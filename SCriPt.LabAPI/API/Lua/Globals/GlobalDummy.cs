using System;
using System.Collections.Generic;
using LabApi.Features.Wrappers;
using MEC;
using MoonSharp.Interpreter;
using NetworkManagerUtils.Dummies;
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
                    return time;
               }
               else if (action.StartsWith("WaitFrames"))
               {
                    // Extract the number of frames from the action string
                    var frames = int.Parse(action.Substring("WaitFrames_".Length));
                    return 0.01f * frames;
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

          List<DummyAction> actions = DummyActionCollector.ServerGetActions(dummy.ReferenceHub);
          foreach (var dummyAction in actions)
          {
               if (dummyAction.Name == action)
               {
                    // Execute the action
                    dummyAction.Action();
                    return Timing.WaitForOneFrame; // Return a small delay to allow the action to complete
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
                    foreach (var action2 in actionTable.Values)
                    {
                         Action(dummy, action2.String);
                    }
                    yield return Timing.WaitForOneFrame;
               } 
               else
               {
                    // Handle other types if necessary
                    yield return Timing.WaitForOneFrame;
               }
               
          }
     }
     
}