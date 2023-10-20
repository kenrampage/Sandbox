using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDataReader : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SessionDataManager _sessionDataManager;

    [Header("Settings")]
    [SerializeField] private string _dataKey;
    [SerializeField] private SessionDataValueType _dataValueType;

    [ContextMenu("Test Print Value")]
    public void GetDataValue()
    {
        SessionDataValue dataValue = _sessionDataManager.GetDataValue(_dataKey);

        if (dataValue != null)
        {
            
            switch (dataValue.Type)
            {
                case SessionDataValueType.STRING:
                    string stringValue = (string)dataValue.Value;
                    print("String value of : " + stringValue);
                    break;
                case SessionDataValueType.INT:
                    int intValue = (int)dataValue.Value;
                    print("int value of : " + intValue);
                    break;

                case SessionDataValueType.BOOL:
                    bool boolValue = (bool)dataValue.Value;
                    print("bool value of : " + boolValue);
                    break;

                default:
                    Debug.LogWarning("Unsupported data type for " + _dataKey);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Data with key " + _dataKey + " does not exist.");
        }
    }

    public void GetDataValue(string valueKey)
    {
        SessionDataValue dataValue = _sessionDataManager.GetDataValue(valueKey);

        if (dataValue != null)
        {

            switch (dataValue.Type)
            {
                case SessionDataValueType.STRING:
                    string stringValue = (string)dataValue.Value;
                    print("String value of : " + stringValue);
                    break;
                case SessionDataValueType.INT:
                    int intValue = (int)dataValue.Value;
                    print("int value of : " + intValue);
                    break;

                case SessionDataValueType.BOOL:
                    bool boolValue = (bool)dataValue.Value;
                    print("bool value of : " + boolValue);
                    break;

                default:
                    Debug.LogWarning("Unsupported data type for " + _dataKey);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Data with key " + _dataKey + " does not exist.");
        }
    }
}
