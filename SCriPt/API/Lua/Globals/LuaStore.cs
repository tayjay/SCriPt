using System;
using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using MEC;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization;
using MoonSharp.Interpreter.Serialization.Json;
using SCriPt.API.Lua.Objects;
using UnityEngine;
using Utf8Json;
using Coroutine = MoonSharp.Interpreter.Coroutine;

namespace SCriPt.API.Lua.Globals;

public class LuaStore
{
    public static Dictionary<string,Table> Tables { get; set; } = new Dictionary<string, Table>();
    
    public static Table Load(string key, DynValue defaultTable = null)
    {
        if (!Tables.TryGetValue(key, out var storage))
        {
            if(defaultTable.Type != DataType.Table)
                throw new Exception("Default table must be a table.");
            storage = defaultTable.Table ?? DynValue.NewPrimeTable().Table;
            Tables[key] = storage;
            SaveStores();
        }
        return storage;
    }
    
    public static void Save(string key, Table table)
    {
        Tables[key] = table;
        SaveStores();
    }
    
    
    [MoonSharpHidden]
    public static void LoadStores()
    {
        try
        {
            if(!Directory.Exists("Scripts/Data"))
                Directory.CreateDirectory("Scripts/Data");
            if (!File.Exists("Scripts/Data/store.json"))
            {
                string json = JsonSerializer.ToJsonString(new Dictionary<string,string>());
                File.WriteAllText("Scripts/Data/store.json", JsonSerializer.PrettyPrint(json));
            }
        
            string data = File.ReadAllText("Scripts/Data/store.json");
            Dictionary<string,string> temp = JsonSerializer.Deserialize<Dictionary<string, string>>(data);
            foreach (KeyValuePair<string,string> pair in temp)
            {
                Tables[pair.Key] = JsonTableConverter.JsonToTable(pair.Value);
            }
            Log.Debug("Lua Store loaded.");
        } catch (Exception e)
        {
            Log.Error("Error loading Lua Store: "+e);
        }
    }
    
    [MoonSharpHidden]
    public static void SaveStores()
    {
        try
        {
            Dictionary<string,string> output = new Dictionary<string, string>();
            foreach (KeyValuePair<string,Table> pair in Tables)
            {
                output[pair.Key] = JsonTableConverter.TableToJson(pair.Value);
            }
            string json = JsonSerializer.ToJsonString(output);
            File.WriteAllText("Scripts/Data/store.json", JsonSerializer.PrettyPrint(json));
            Log.Debug("Lua Store saved.");
        } catch (Exception e)
        {
            Log.Error("Error saving Lua Store: "+e);
        }
        Log.Debug("Lua Store saved.");
    }

    [MoonSharpHidden]
    static CoroutineHandle _saveCoroutine;
    
    [MoonSharpHidden]
    public static void Register()
    {
        if(!SCriPt.Instance.Config.EnableStorage) return;
        //_saveCoroutine = Timing.RunCoroutine(SaveCoroutine());
    }
    
    [MoonSharpHidden]
    public static IEnumerator<float> SaveCoroutine()
    {
        while (true)
        {
            try
            {
                SaveStores();
            } catch (Exception e)
            {
                Log.Error("Error auto-saving Lua Store: "+e);
            }
            yield return Timing.WaitForSeconds(SCriPt.Instance.Config.AutoSaveInterval);
        }
    }
    
    [MoonSharpHidden]
    public static void Unregister()
    {
        Timing.KillCoroutines(_saveCoroutine);
        SaveStores();
        Tables.Clear();
    }

    [MoonSharpHidden]
    private static DynValue ExternalGetDynValue(string table, string key)
    {
        if(!Tables.TryGetValue(table, out var storage))
            return DynValue.Nil;
        DynValue value = storage.Get(key);
        return value;
    }
    
    [MoonSharpHidden]
    private static void ExternalSetDynValue(string table, string key, DynValue value)
    {
        if(!Tables.TryGetValue(table, out var storage))
            return;
        storage.Set(key, value);
    }
    
    [MoonSharpHidden]
    public static double ExternalGetNumber(string table, string key)
    {
        DynValue value = ExternalGetDynValue(table, key);
        if(value.Type != DataType.Number)
            throw new InvalidCastException("Value is not a number.");
        return value.Number;
    }
    
    [MoonSharpHidden]
    public static void ExternalSetNumber(string table, string key, double value)
    {
        ExternalSetDynValue(table, key, DynValue.NewNumber(value));
    }
    
    [MoonSharpHidden]
    public static string ExternalGetString(string table, string key)
    {
        DynValue value = ExternalGetDynValue(table, key);
        if(value.Type != DataType.String)
            throw new InvalidCastException("Value is not a string.");
        return value.String;
    }
    
    [MoonSharpHidden]
    public static void ExternalSetString(string table, string key, string value)
    {
        ExternalSetDynValue(table, key, DynValue.NewString(value));
    }
    
    [MoonSharpHidden]
    public static bool ExternalGetBool(string table, string key)
    {
        DynValue value = ExternalGetDynValue(table, key);
        if(value.Type != DataType.Boolean)
            throw new InvalidCastException("Value is not a boolean.");
        return value.Boolean;
    }
    
    [MoonSharpHidden]
    public static void ExternalSetBool(string table, string key, bool value)
    {
        ExternalSetDynValue(table, key, DynValue.NewBoolean(value));
    }
    
    [MoonSharpHidden]
    public static Dictionary<string,object> ExternalGetTable(string table, string key)
    {
        DynValue value = ExternalGetDynValue(table, key);
        if(value.Type != DataType.Table)
            throw new InvalidCastException("Value is not a table.");
        Dictionary<string,object> output = new Dictionary<string, object>();
        foreach (var pair in value.Table.Pairs)
        {
            output[pair.Key.String] = pair.Value.ToObject();
        }
        return output;
    }
    
    [MoonSharpHidden]
    public static void ExternalSetTable(string table, string key, Dictionary<string,object> value)
    {
        Table newTable = DynValue.NewPrimeTable().Table;
        foreach (KeyValuePair<string,object> pair in value)
        {
            newTable.Set(pair.Key, DynValue.FromObject(newTable.OwnerScript, pair.Value));
        }
        ExternalSetDynValue(table, key, DynValue.NewTable(newTable));
    }
    
    
}