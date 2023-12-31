using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SessionDataManager : MonoBehaviour
{
    #region Singleton Pattern
    private static SessionDataManager _instance;
    public static SessionDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SessionDataManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    #endregion

    private Dictionary<string, SessionDataValue> _data = new Dictionary<string, SessionDataValue>();

    [HideInInspector] public UnityEvent TriggerReadValues;
    [HideInInspector] public UnityEvent TriggerWriteValues;

    #region Init
    private void Awake()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Dictionary Management
    public void AddValue(string dataKey, object value, Type type)
    {
        if (!_data.ContainsKey(dataKey))
        {
            _data[dataKey] = new SessionDataValue(value, type);
        }
        else
        {
            _data[dataKey] = new SessionDataValue(value, type);
            Debug.Log("Key already exists: " + dataKey + ". Updating Value");
        }
    }

    public void AddValue(string dataKey, SessionDataValue sessionDataValue)
    {
        if (!_data.ContainsKey(dataKey))
        {
            _data[dataKey] = sessionDataValue;
        }
        else
        {
            _data[dataKey] = sessionDataValue;
            Debug.Log("Key already exists: " + dataKey + ". Updating Value");
        }
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

    #region System Control

    public void PrintAllData()
    {
        foreach (var data in _data)
        {
            Debug.Log("Key: " + data.Key + " | Value:" + data.Value.Value + " | Type:" + data.Value.ValueType);
        }
    }

    public void ReadValuesFromObjects()
    {
        TriggerReadValues?.Invoke();
        Debug.Log("Reading values from Source Objects");
    }

    public void WriteValuesToObjects()
    {
        TriggerWriteValues.Invoke();
        Debug.Log("Writing values to Target Objects");
    }

    public void ClearData()
    {
        _data.Clear();
        Debug.Log("Data has been cleared");
    }

    #endregion
}
