using System;
using System.Reflection;
using UnityEngine;

public class SessionDataReadWrite : MonoBehaviour
{
    [Header("Data Manager Settings")]
    [SerializeField] private SessionDataManager _persistentDataManager;
    public string DataKey;

    [Header("Object Settings")]
    public GameObject TargetGameObject;
    public string ValueSourceName;
    public string ValueTargetName;

    private void OnEnable()
    {
        _persistentDataManager.TriggerRetrieveValues.AddListener(RetrieveValue);
        _persistentDataManager.TriggerSetValues.AddListener(SetValue);
    }

    private void OnDisable()
    {
        _persistentDataManager.TriggerRetrieveValues.RemoveListener(RetrieveValue);
        _persistentDataManager.TriggerSetValues.RemoveListener(SetValue);
    }

    [ContextMenu("Get Value")]
    public void RetrieveValue()
    {
        if (string.IsNullOrEmpty(ValueSourceName))
        {
            Debug.LogWarning("ValueSourceName is empty. Cannot retrieve data.");
            return;
        }

        Component[] components = TargetGameObject.GetComponents<Component>();

        foreach (Component component in components)
        {
            FieldInfo field = component.GetType().GetField(ValueSourceName);
            MethodInfo method = component.GetType().GetMethod(ValueSourceName);
            PropertyInfo property = component.GetType().GetProperty(ValueSourceName);

            if (field != null || method != null || property != null)
            {
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
                    _persistentDataManager.AddValue(DataKey, new SessionDataValue(value, valueType));
                    // Debug.Log($"Retrieved value from component {(field != null ? "field" : method != null ? "method" : "property")} {ValueSourceName}: {value} {valueType}");
                    return;
                }
            }
        }
    }

    [ContextMenu("Set Value")]
    public void SetValue()
    {
        if (string.IsNullOrEmpty(ValueTargetName))
        {
            Debug.LogWarning("ValueTargetName is empty. Cannot set data.");
            return;
        }

        Component[] components = TargetGameObject.GetComponents<Component>();

        foreach (Component component in components)
        {
            FieldInfo field = component.GetType().GetField(ValueTargetName);
            PropertyInfo property = component.GetType().GetProperty(ValueTargetName);
            MethodInfo method = component.GetType().GetMethod(ValueTargetName);

            if (field != null || method != null || property != null)
            {
                object value = _persistentDataManager.GetValue(DataKey)?.Value;

                if (value == null)
                {
                    Debug.Log("No data available with that name");
                    return;
                }

                Type sourceValueType = _persistentDataManager.GetValue(DataKey)?.ValueType;
                Type targetValueType;

                if (field != null)
                {
                    targetValueType = field.FieldType;

                    if (targetValueType == sourceValueType)
                    {
                        field.SetValue(component, value);
                    }
                }
                else if (property != null)
                {
                    targetValueType = property.PropertyType;

                    if (targetValueType == sourceValueType)
                    {
                        property.SetValue(component, value);
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
                            Debug.LogError("Method parameters do not match your requirements.");
                        }
                    }
                    else
                    {
                        Debug.LogError("Method has the wrong number of parameters.");
                    }
                }
            }
        }
    }
}
