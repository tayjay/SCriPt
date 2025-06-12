using System;
using System.Collections.Generic;
using UnityEngine;
using Utf8Json;

namespace SCriPt.EXILED.API.Lua.Objects;

public class CustomConfig : IJsonSerializable
{
    public Dictionary<string,object> Config { get; set; }
    
    public CustomConfig()
    {
        Config = new Dictionary<string, object>();
    }
    
    /// <summary>
    /// Get an integer value from the config.
    /// </summary>
    /// <param name="key">Unique name of the config item</param>
    /// <param name="defaultValue">Default value to set if the item has not been added yet.</param>
    /// <param name="value">Value that is stored in the config</param>
    /// <returns>Whether the entry is new and needs to be saved to disk.</returns>
    public bool GetInt(string key, int defaultValue, out int value)
    {
        if (Config.TryGetValue(key, out var value1))
        {
            value = Convert.ToInt32(value1);
            return false;
        }
        Config[key] = defaultValue;
        value = defaultValue;
        return true;
    }
    
    /// <summary>
    /// Get a float value from the config.
    /// </summary>
    /// <param name="key">Unique name of the config item</param>
    /// <param name="defaultValue">Default value to set if the item has not been added yet.</param>
    /// <param name="value">Value that is stored in the config</param>
    /// <returns>Whether the entry is new and needs to be saved to disk.</returns>
    public bool GetFloat(string key, float defaultValue, out float value)
    {
        if (Config.TryGetValue(key, out var value1))
        {
            value = Convert.ToSingle(value1);
            return false;
        }
        Config[key] = defaultValue;
        value = defaultValue;
        return true;
    }
    
    /// <summary>
    /// Get a float value from the config.
    /// </summary>
    /// <param name="key">Unique name of the config item</param>
    /// <param name="defaultValue">Default value to set if the item has not been added yet.</param>
    /// <param name="value">Value that is stored in the config</param>
    /// <returns>Whether the entry is new and needs to be saved to disk.</returns>
    public bool GetString(string key, string defaultValue, out string value)
    {
        if (Config.TryGetValue(key, out var value1))
        {
            value = Convert.ToString(value1);
            return false;
        }
        Config[key] = defaultValue;
        value = defaultValue;
        return true;
    }
    
    /// <summary>
    /// Get a float value from the config.
    /// </summary>
    /// <param name="key">Unique name of the config item</param>
    /// <param name="defaultValue">Default value to set if the item has not been added yet.</param>
    /// <param name="value">Value that is stored in the config</param>
    /// <returns>Whether the entry is new and needs to be saved to disk.</returns>
    public bool GetBool(string key, bool defaultValue, out bool value)
    {
        if (Config.TryGetValue(key, out var value1))
        {
            value = Convert.ToBoolean(value1);
            return false;
        }
        Config[key] = defaultValue;
        value = defaultValue;
        return true;
    }
    
    /// <summary>
    /// Get a float value from the config.
    /// </summary>
    /// <param name="key">Unique name of the config item</param>
    /// <param name="defaultValue">Default value to set if the item has not been added yet.</param>
    /// <param name="value">Value that is stored in the config</param>
    /// <returns>Whether the entry is new and needs to be saved to disk.</returns>
    public bool GetVector3(string key, UnityEngine.Vector3 defaultValue, out UnityEngine.Vector3 value)
    {
        if (Config.TryGetValue(key, out var value1))
        {
            string str = Convert.ToString(value1);
            value = JsonSerializer.Deserialize<Vector3>(str);
            return false;
        }
        Config[key] = defaultValue;
        value = defaultValue;
        return true;
    }
    
    public int SetInt(string key, int value)
    {
        Config[key] = value;
        return value;
    }
    
    public float SetFloat(string key, float value)
    {
        Config[key] = value;
        return value;
    }
    
    public string SetString(string key, string value)
    {
        Config[key] = value;
        return value;
    }
    
    public bool SetBool(string key, bool value)
    {
        Config[key] = value;
        return value;
    }
    
    public Vector3 SetVector3(string key, Vector3 value)
    {
        Config[key] = JsonSerializer.Serialize(value);
        return value;
    }
    
    
    public bool Contains(string key)
    {
        return Config.ContainsKey(key);
    }
    
}