using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    [HideInInspector] public Dictionary<string, PersistentData> _data = new Dictionary<string, PersistentData>();

    [HideInInspector] public UnityEvent OnRetrieveValues;



    #region Add/Get data
    public void AddValue(string dataKey, object value, Type type)
    {
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
    public void TriggerRetrieveValues()
    {
        OnRetrieveValues.Invoke();
    }

    [ContextMenu("Clear Data")]
    public void ClearData()
    {
        _data.Clear();
        Debug.Log("Data has been cleared");
    }
}
