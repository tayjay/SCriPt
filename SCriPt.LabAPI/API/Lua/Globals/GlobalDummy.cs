using System;
using System.Collections.Generic;
using LabApi.Features.Wrappers;
using MEC;
using MoonSharp.Interpreter;
using NetworkManagerUtils.Dummies;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Thirdperson;
using PlayerRoles.FirstPersonControl.Thirdperson.Subcontrollers;
using SCriPt.LabAPI.API.Lua.Objects;

namespace SCriPt.LabAPI.API.Lua.Globals;

[MoonSharpUserData]
public class GlobalDummy
{
     
     public ScriptHandler Owner { get; }
     
     public List<Player> Dummies { get; } = new List<Player>();
     
     public GlobalDummy(ScriptHandler owner)
     {
          Owner = owner;
     }
     
     public Player Spawn(string name)
     {
          Player p = Player.Get(DummyUtils.SpawnDummy(name));
          if (p != null)
          {
               Dummies.Add(p);
               return p;
          }
          return null;
     }
     
     public Player Remove(Player dummy)
     {
          if (dummy == null || !Dummies.Contains(dummy))
               return null;
          
          Dummies.Remove(dummy);
          dummy.Kick(Player.Host, "Dummy removed by script.");
          return dummy;
     }
     
     public Table GetDummyActions(Player dummy)
     {
          List<DummyAction> actions = DummyActionCollector.ServerGetActions(dummy.ReferenceHub);
          DynValue table = DynValue.NewPrimeTable();
          
          foreach (var action in actions)
          {
               table.Table[action.Name] = action.Name;
          }
          
          return table.Table;
     }

     public float Action(Player dummy, string action)
     {
          if (action.StartsWith("Wait"))
          {
               if(action.StartsWith("WaitSeconds"))
               {
                    // Extract the time in seconds from the action string
                    var time = float.Parse(action.Substring("WaitSeconds_".Length));
                    return Timing.WaitForSeconds(time);
               }
               else if (action.StartsWith("WaitFrames"))
               {
                    // Extract the number of frames from the action string
                    var frames = int.Parse(action.Substring("WaitFrames_".Length));
                    return frames;
               }
          }

          if (action.StartsWith("Hold"))
          {
               var itemStr = action.Substring("Hold_".Length);
               if (Enum.TryParse(itemStr, out ItemType itemType))
               {
                    foreach(var item in dummy.Items)
                    {
                         if (item != null && item.Type == itemType)
                         {
                              // Hold the item
                              dummy.CurrentItem = item;
                              return Timing.WaitForOneFrame; // Return a small delay to allow the action to complete
                         }
                    }
               } else if (itemStr == "None")
               {
                    dummy.CurrentItem = null;
               }
          }

          if (action.StartsWith("Drop"))
          {
               if(dummy.CurrentItem != null)
                    dummy.DropItem(dummy.CurrentItem);
               return Timing.WaitForOneFrame; // Return a small delay to allow the action to complete
          }

          if (action.StartsWith("Emotion"))
          {
               var emotionStr = action.Substring("Emotion_".Length);
               if (Enum.TryParse(emotionStr, out EmotionPresetType emotionType))
               {
                    // Execute the emotion
                    dummy.ReferenceHub.ServerSetEmotionPreset(emotionType);
                    return Timing.WaitForOneFrame; // Return a small delay to allow the action to complete
               }
          }

          List<DummyAction> actions = DummyActionCollector.ServerGetActions(dummy.ReferenceHub);
          foreach (var dummyAction in actions)
          {
               if (dummyAction.Name == action)
               {
                    // Execute the action
                    dummyAction.Action();
                    //return Timing.WaitForOneFrame; // Return a small delay to allow the action to complete
                    // We don't return a delay here since there could be multiple actions with the same name in different Controllers
               }
          }
          
          
          return Timing.WaitForOneFrame;
     }
     
     public void ActionSequence(Player dummy, Table sequence)
     {
          Owner.Coroutines.Handles.Add(Timing.RunCoroutine(ActionCoroutine(dummy, sequence)));
     }

     private IEnumerator<float> ActionCoroutine(Player dummy, Table sequence)
     {
          foreach (var action in sequence.Values)
          {
               if (action.Type == DataType.String)
               {
                    yield return Action(dummy, action.String);
               }
               else if (action.Type == DataType.Table)
               {
                    var actionTable = action.Table;
                    float waitTime = 0f;
                    foreach (var action2 in actionTable.Values)
                    {
                         float newtime = Action(dummy, action2.String);
                         if (newtime > waitTime)
                         {
                              waitTime = newtime;
                         }
                    }
                    yield return waitTime;
               } 
               else
               {
                    // Handle other types if necessary
                    yield return Timing.WaitForOneFrame;
               }
               
          }
     }
     
}