using System;
using System.Collections.Generic;
using Exiled.API.Enums;
using MoonSharp.Interpreter;
using SCriPt.API.Lua.Objects;

namespace SCriPt.API.Lua.Globals;
[MoonSharpUserData]
public class LuaCustomItems
{
    public static List<CustomScriptItem> Items { get; } = new List<CustomScriptItem>();
    public static List<CustomScriptFirearm> Firearms { get; } = new List<CustomScriptFirearm>();
    public static uint NextId { get; private set; } = 5283;

    public void AddScript(Script script)
    {
        foreach (DynValue dynValue in script.Globals.Values)
        {
            if (dynValue.Type == DataType.Table)
            {
                Table table = dynValue.Table;
                // Register table as an item
                if (table.Get("item_type").Type == DataType.String &&
                    table.Get("name").Type == DataType.String && table.Get("description").Type == DataType.String &&
                    table.Get("weight").Type == DataType.Number)
                {
                    // Create the item
                    Enum.TryParse(table.Get("item_type").String, out ItemType type);
                    uint id = NextId++;
                    string name = table.Get("name").String;
                    string description = table.Get("description").String;
                    float weight = (float) table.Get("weight").Number;

                    
                    CustomScriptItem item = new CustomScriptItem(script, type, id, name, description, weight);
                    // Register the events it will handle
                    foreach(CustomScriptItem.EventType eventType in Enum.GetValues(typeof(CustomScriptItem.EventType)))
                    {
                        if (table.Get(type.ToString()).Type == DataType.Function)
                        {
                            item.AddEvent(eventType, table.Get(type.ToString()));
                        }
                    }
                    Items.Add(item);
                }

                // Register table as a firearm
                if (table.Get("firearm_type").Type == DataType.String &&
                    table.Get("name").Type == DataType.String && table.Get("description").Type == DataType.String &&
                    table.Get("weight").Type == DataType.Number)
                {
                    // Create the item
                    Enum.TryParse(table.Get("firearm_type").String, out FirearmType type);
                    uint id = NextId++;
                    string name = table.Get("name").String;
                    string description = table.Get("description").String;
                    float weight = (float) table.Get("weight").Number;
                    float damage = (float) table.Get("damage").Number;

                    CustomScriptFirearm item = new CustomScriptFirearm(script, type, id, name, description, weight, damage);
                    // Register the events it will handle
                    foreach(CustomScriptFirearm.EventType eventType in Enum.GetValues(typeof(CustomScriptFirearm.EventType)))
                    {
                        if (table.Get(type.ToString()).Type == DataType.Function)
                        {
                            item.AddEvent(eventType, table.Get(type.ToString()));
                        }
                    }
                    Firearms.Add(item);
                }
            }
        }
    }
    
    
    public void AddItem(ItemType type, uint id, string name, string description, float weight)
    {
        
    }
    
    public void RemoveItem(uint id)
    {
        
    }
    
    public static void RegisterAll()
    {
        foreach (var item in Items)
        {
            item.ScriptRegister();
        }
        foreach (var firearm in Firearms)
        {
            firearm.ScriptRegister();
        }
    }
    
    
}