using System.Collections.Generic;
using Exiled.API.Features;
using MoonSharp.Interpreter;
using SCriPt.EXILED.API.Lua.Objects;
using UnityEngine;
using UserSettings.ServerSpecific;

namespace SCriPt.EXILED.API.Lua.Globals;

[MoonSharpUserData]
public class LuaSSMenu : IGlobal
{
    public Script Owner { get; }
    
    public LuaSSMenu(Script owner)
    {
        Owner = owner;
    }
    
    /*
     *
     * mod = {}
     * mod.menu = {
     *      Menu.Header("Header"),
     *      Menu.TextArea("Text"),
     *      Menu.Button("Label", "ButtonText", function(setting, hub) end)
     *     Menu.KeyBind("Label", function(setting, hub) end, 0)
     * }
     * 
     */
    
    public void AddScript(Script script)
    {
        Log.Debug("Attempting to add Menu for script.");
        foreach (DynValue dynValue in script.Globals.Values)
        {
            if (dynValue.Type == DataType.Table)
            {
                foreach (TablePair entry in dynValue.Table.Pairs)
                {
                    if (entry.Key.Type == DataType.String && entry.Key.String == "menu")
                    {
                        Log.Debug("Found a menu table.");
                        //Found a menu table to add
                        CustomSSMenu customSSMenu = new CustomSSMenu(script);
                        Table menu = entry.Value.Table;
                        foreach(TablePair menuEntry in menu.Pairs)
                        {
                            if (menuEntry.Value.Type == DataType.Table)
                            {
                                Table menuEntryTable = menuEntry.Value.Table;
                                if (menuEntryTable.Get("type").Type == DataType.String)
                                {
                                    Log.Debug("Found a menu entry "+menuEntryTable.Get("type").String);
                                    switch (menuEntryTable.Get("type").String)
                                    {
                                        case "header":
                                            customSSMenu.AddHeader(menuEntryTable.Get("text").String);
                                            break;
                                        case "textarea":
                                            customSSMenu.AddTextArea(menuEntryTable.Get("text").String);
                                            break;
                                        case "button":
                                            customSSMenu.AddButton(menuEntryTable.Get("label").String, menuEntryTable.Get("buttonText").String, menuEntryTable.Get("callback").Function);
                                            break;
                                        case "keybind":
                                            customSSMenu.AddKeyBind(menuEntryTable.Get("label").String, menuEntryTable.Get("callback").Function, (int)menuEntryTable.Get("code").Number);
                                            break;
                                    }
                                }
                            }
                        }
                        customSSMenu.GenerateMenu();
                    }
                }
            }
        }
    }

    public Table Header(string text)
    {
        Dictionary<string,DynValue> dict = new Dictionary<string, DynValue>();
        dict.Add("type",DynValue.NewString("header"));
        dict.Add("text",DynValue.NewString(text));
        return DynValue.FromObject(Owner, dict).Table;
    }
    
    public Table TextArea(string text)
    {
        Dictionary<string,DynValue> dict = new Dictionary<string, DynValue>();
        dict.Add("type",DynValue.NewString("textarea"));
        dict.Add("text",DynValue.NewString(text));
        return DynValue.FromObject(Owner, dict).Table;
    }
    
    public Table Button(string label, string buttonText, Closure callback)
    {
        Dictionary<string,DynValue> dict = new Dictionary<string, DynValue>();
        dict.Add("type",DynValue.NewString("button"));
        dict.Add("label",DynValue.NewString(label));
        dict.Add("buttonText",DynValue.NewString(buttonText));
        dict.Add("callback",DynValue.NewClosure(callback));
        return DynValue.FromObject(Owner, dict).Table;
    }
    
    public Table KeyBind(string label, Closure callback, int defaultcode=0)
    {
        Dictionary<string,DynValue> dict = new Dictionary<string, DynValue>();
        dict.Add("type",DynValue.NewString("keybind"));
        dict.Add("label",DynValue.NewString(label));
        dict.Add("code",DynValue.NewNumber(defaultcode));
        dict.Add("callback",DynValue.NewClosure(callback));
        return DynValue.FromObject(Owner, dict).Table;
    }
    
    

    
}