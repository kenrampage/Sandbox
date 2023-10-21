using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SessionDataManager : MonoBehaviour
{
    #region Singleton
    private static SessionDataManager instance;
    public static SessionDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SessionDataManager();
            }
            return instance;
        }
    }

    // Private constructor to prevent instantiation from other classes
    private SessionDataManager()
    {
        // Initialize Singleton here
    }

    #endregion

    private Dictionary<string, SessionDataValue> _data = new Dictionary<string, SessionDataValue>();

    [HideInInspector] public UnityEvent TriggerRetrieveValues;
    [HideInInspector] public UnityEvent TriggerSetValues;



    #region Add/Get data
    public void AddValue(string dataKey, object value, Type type)
    {
        if (!_data.ContainsKey(dataKey))
        {
            _data[dataKey] = new SessionDataValue(value, type);
        }
        else
        {
            Debug.LogWarning("Key already exists: " + dataKey);
        }
    }

    public void AddValue(string dataKey, SessionDataValue persistentData)
    {
        _data.Add(dataKey, persistentData);
    }

    public SessionDataValue GetValue(string dataKey)
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

    public Dictionary<string, SessionDataValue> GetDictionary()
    {
        return _data;
    }
    #endregion

    [ContextMenu("Print All Data")]
    public void PrintAllData()
    {
        foreach (var data in _data)
        {
            Debug.Log("Key: " + data.Key + " | Value:" + data.Value.Value + " | Type:" + data.Value.ValueType);
        }
    }

    [ContextMenu("Retrieve Values")]
    public void RetrieveValues()
    {
        TriggerRetrieveValues.Invoke();
        Debug.Log("Telling Value Handlers to retrieve data");
    }

    [ContextMenu("Set Values")]
    public void SetValues()
    {
        TriggerSetValues.Invoke();
        Debug.Log("Telling Value Handlers to set data");
    }

    [ContextMenu("Clear Data")]
    public void ClearData()
    {
        _data.Clear();
        Debug.Log("Data has been cleared");
    }
}
