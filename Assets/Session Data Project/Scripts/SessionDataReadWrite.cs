using System;
using System.Data;
using System.Reflection;
using UnityEngine;

public class SessionDataReadWrite : MonoBehaviour
{
    private SessionDataManager _sessionDataManager;

    public GameObject TargetGameObject;
    [Space(10)]
    public string ValueSourceName;
    public string ValueTargetName;
    [Space(10)]
    public string DataKey;

    #region Init
    private void Awake()
    {
        GetReferences();
    }

    private void OnEnable()
    {
        _sessionDataManager.TriggerReadValues.AddListener(ReadValue);
        _sessionDataManager.TriggerWriteValues.AddListener(WriteValue);
    }

    private void OnDisable()
    {
        _sessionDataManager.TriggerReadValues.RemoveListener(ReadValue);
        _sessionDataManager.TriggerWriteValues.RemoveListener(WriteValue);
    }

    private void GetReferences()
    {
        if (_sessionDataManager == null)
        {
            _sessionDataManager = SessionDataManager.Instance;
        }
    }

    #endregion

    public void ReadValue()
    {
        if (string.IsNullOrEmpty(ValueSourceName))
        {
            Debug.LogWarning("ValueSourceName is empty. Cannot read data.");
            return;
        }

        Component[] components = TargetGameObject.GetComponents<Component>();

        bool sourceFound = false;

        foreach (Component component in components)
        {
            FieldInfo field = component.GetType().GetField(ValueSourceName);
            MethodInfo method = component.GetType().GetMethod(ValueSourceName);
            PropertyInfo property = component.GetType().GetProperty(ValueSourceName);

            if (field != null || method != null || property != null)
            {
                sourceFound = true;

                object value = null;
                Type valueType = null;

                if (field != null)
                {
                    value = field.GetValue(component);
                    valueType = field.FieldType;
                }
                else if (method != null)
                {
                    value = method.Invoke(component, null);
                    valueType = method.ReturnType;
                }
                else if (property != null)
                {
                    value = property.GetValue(component);
                    valueType = property.PropertyType;
                }

                if (value != null)
                {
                    _sessionDataManager.AddValue(DataKey, new SessionDataValue(value, valueType));
                    //Debug.Log($"Read value from component {(field != null ? "field" : method != null ? "method" : "property")} {ValueSourceName}: {value} {valueType}");
                    return;
                }

            }
        }

        if (!sourceFound)
        {
            Debug.LogWarning("No Source called " + ValueSourceName + " was found");
        }
    }

    public void WriteValue()
    {
        if (string.IsNullOrEmpty(ValueTargetName))
        {
            Debug.LogWarning("ValueTargetName is empty. Cannot write data.");
            return;
        }

        Component[] components = TargetGameObject.GetComponents<Component>();

        bool targetFound = false;

        foreach (Component component in components)
        {
            FieldInfo field = component.GetType().GetField(ValueTargetName);
            PropertyInfo property = component.GetType().GetProperty(ValueTargetName);
            MethodInfo method = component.GetType().GetMethod(ValueTargetName);

            if (field != null || method != null || property != null)
            {
                targetFound = true;
                object value = _sessionDataManager.GetValue(DataKey)?.Value;

                if (value == null)
                {
                    return;
                }

                Type sourceValueType = _sessionDataManager.GetValue(DataKey)?.ValueType;
                Type targetValueType;

                if (field != null)
                {
                    targetValueType = field.FieldType;

                    if (targetValueType == sourceValueType)
                    {
                        field.SetValue(component, value);
                    }
                    else
                    {
                        Debug.LogWarning("Type Mismatch between source:" + sourceValueType + " & target:" + targetValueType);
                    }
                }
                else if (property != null)
                {
                    targetValueType = property.PropertyType;

                    if (targetValueType == sourceValueType)
                    {
                        property.SetValue(component, value);
                    }
                    else
                    {
                        Debug.LogWarning("Type Mismatch between source:" + sourceValueType + " & target:" + targetValueType);
                    }
                }
                else if (method != null)
                {
                    ParameterInfo[] parametersInfo = method.GetParameters();

                    if (parametersInfo.Length == 1)
                    {
                        targetValueType = parametersInfo[0].ParameterType;

                        if (targetValueType == sourceValueType)
                        {
                            object[] parameters = new object[1];
                            parameters[0] = value;
                            method.Invoke(component, parameters);
                        }
                        else
                        {
                            Debug.LogWarning("Type Mismatch between source:" + sourceValueType + " & target:" + targetValueType);
                        }
                    }
                    else
                    {
                        Debug.LogError("Method has the wrong number of parameters.");
                    }
                }
            }
        }
        if (!targetFound)
        {
            Debug.LogWarning("No Target called " + ValueTargetName + " was found");
        }
    }

}
