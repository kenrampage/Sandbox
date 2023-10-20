using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SessionDataManager : MonoBehaviour
{
    [SerializeField] private Dictionary<string, SessionDataValue> _sessionData = new Dictionary<string, SessionDataValue>();

    public SessionDataValue TestDataValue;

    //[Header("Debug")]
    //public string TestKeyToPrint;

    public void AddDataValue(string key, SessionDataValueType valueType, object value)
    {
        if (_sessionData.ContainsKey(key))
        {
            _sessionData[key] = new SessionDataValue(valueType, value);
        }
        else
        {
            _sessionData.Add(key, new SessionDataValue(valueType, value));
        }
    }

    //// Get a value from the collection using a string key
    //public T GetDataValue<T>(string key)
    //{
    //    if (_sessionData.ContainsKey(key) && _sessionData[key] is T)
    //    {
    //        T value = (T)_sessionData[key].Value;
    //        return value;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Data with key " + key + " does not exist or is of a different type.");
    //        return default(T);
    //    }
    //}

    [ContextMenu("Test Add Value 1")]
    public void TestAddValue()
    {
        AddDataValue("Test Key 1", SessionDataValueType.STRING, "Test Value 1 (String)");
        print("Session Data Count " + _sessionData.Count);
    }

    [ContextMenu("Test Add Value 2")]
    public void TestAddValue2()
    {
        AddDataValue("Test Key 2", SessionDataValueType.INT, 2);
        print("Session Data Count " + _sessionData.Count);
    }

    [ContextMenu("Test Add Value 3")]
    public void TestAddValue3()
    {
        AddDataValue("Test Key 3", SessionDataValueType.BOOL, true);
        print("Session Data Count " + _sessionData.Count);
    }

    //[ContextMenu("Test Print Value")]
    //public void GetDataValue()
    //{
    //    if (_sessionData.ContainsKey(TestKeyToPrint))
    //    {
    //        SessionDataValue dataValue = _sessionData[TestKeyToPrint];

    //        switch (dataValue.Type)
    //        {
    //            case SessionDataValueType.STRING:
    //                string stringValue = (string)dataValue.Value;
    //                print("String value of : " + stringValue);
    //                break;
    //            case SessionDataValueType.INT:
    //                int intValue = (int)dataValue.Value;
    //                print("int value of : " + intValue);
    //                break;

    //            case SessionDataValueType.BOOL:
    //                bool boolValue = (bool)dataValue.Value;
    //                print("bool value of : " + boolValue);
    //                break;

    //            default:
    //                Debug.LogWarning("Unsupported data type for " + TestKeyToPrint );
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Data with key " + TestKeyToPrint + " does not exist.");
    //    }
    //}

    public SessionDataValue GetDataValue(string valueKey)
    {
        return _sessionData[valueKey];
    }

}
