using System.Collections.Generic;
using System.IO;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;
using SCriPt.LabAPI.API.Lua.Objects;
using SCriPt.LabAPI.Handlers;
using Utf8Json;

namespace SCriPt.LabAPI.API.Lua.Globals;

// Persistence
[MoonSharpUserData]
public class GlobalData
{
    [MoonSharpHidden]
    public static Dictionary<string, LuaCustomData> CustomData { get; set; } = new Dictionary<string, LuaCustomData>();

    
    // my_mod.Data = Data.Get("my_mod.Data")
    
    public static LuaCustomData Get(string name)
    {
        if (CustomData.TryGetValue(name, out LuaCustomData data))
        {
            return data;
        }
        else
        {
            // If the data does not exist, create a new one
            LuaCustomData newData = new LuaCustomData();
            CustomData[name] = newData;
            SaveDataToDisk(newData);
            return newData;
        }
    }
    
    [MoonSharpHidden]
    public static void LoadFromDisk()
    {
        // Load tables from disk
        //Iterate through all json files in the Data folder
        DirectoryInfo dataDir = ScriptLoader.ScriptPathParent.CreateSubdirectory("SCriPt/Data");
        foreach (FileInfo file in dataDir.GetFiles("*.json", SearchOption.AllDirectories))
        {
            string data = File.ReadAllText(file.FullName);
            Table table = JsonTableConverter.JsonToTable(data);
            if (table != null)
            {
                LuaCustomData customData = new LuaCustomData();
                foreach (TablePair pair in table.Pairs)
                {
                    // We drill down to the table variable as to not trigger Save to Disk every time we access a value, losing data
                    customData.table.Table[pair.Key.String] = pair.Value;
                }
                CustomData[file.Name.Replace(".json", "")] = customData;
            }
        }
    }

    [MoonSharpHidden]
    public static void SaveDataToDisk(string name)
    {
        FileInfo file = new FileInfo(Path.Combine(ScriptLoader.ScriptPathParent.FullName, "SCriPt/Data", name + ".json"));
        if (!file.Directory.Exists)
        {
            file.Directory.Create();
        }
        LuaCustomData customData = CustomData[name];
        string json = JsonTableConverter.TableToJson(customData.table.Table);
        File.WriteAllText(file.FullName, JsonSerializer.PrettyPrint(json));
    }

    [MoonSharpHidden]
    public static void SaveDataToDisk(LuaCustomData data)
    {
        foreach (KeyValuePair<string, LuaCustomData> pair in CustomData)
        {
            if (pair.Value == data)
            {
                SaveDataToDisk(pair.Key);
                return;
            }
        }
    }
}