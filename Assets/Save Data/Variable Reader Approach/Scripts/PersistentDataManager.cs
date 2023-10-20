using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData
{
    public object Value;
    public Type ValueType;

    public PersistentData(object value, Type valueType)
    {
        Value = value;
        ValueType = valueType;
    }

}
public class PersistentDataManager : MonoBehaviour
{
    #region Singleton
    private static PersistentDataManager instance;
    public static PersistentDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PersistentDataManager();
            }
            return instance;
        }
    }

    // Private constructor to prevent instantiation from other classes
    private PersistentDataManager()
    {
        // Initialize Singleton here
    }

    #endregion

    private Dictionary<string, PersistentData> _data = new Dictionary<string, PersistentData>();
    //private Dictionary<string, object> _values = new Dictionary<string, object>();
    //private Dictionary<string, Type> _types = new Dictionary<string, Type>();

    public void AddValue(string dataKey, object value, Type type)
    {
        //_values.Add(key, value);
        //_types.Add(key, type);

        PersistentData data = new PersistentData(value, type);

        _data[dataKey] = data;
    }

    public void AddValue(string dataKey, PersistentData persistentData)
    {
        _data.Add(dataKey, persistentData);
    }

    public PersistentData GetValue(string dataKey)
    {
        if (_data.ContainsKey(dataKey))
        {
            return _data[dataKey];
        }
        else
        {
            Debug.LogWarning("Key not found: " + dataKey);
            return null;
        }
    }

    [ContextMenu("Print All Data")]
    public void PrintAllData()
    {
        foreach (var data in _data)
        {
            Debug.Log("Key: " + data.Key + " | Value:" + data.Value.Value + " | Type:" + data.Value.ValueType);
        }
    }
}
