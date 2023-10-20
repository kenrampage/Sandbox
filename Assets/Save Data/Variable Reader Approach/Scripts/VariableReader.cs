using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

public class VariableReader : MonoBehaviour
{
    [SerializeField] private PersistentDataManager _persistentDataManager;

    public string DataKey;
    public string SourceName;
    public GameObject TargetGameObject;

    #region Init

    private void OnEnable()
    {
        _persistentDataManager.OnRetrieveValues.AddListener(RetrieveValue);
    }

    private void OnDestroy()
    {
        _persistentDataManager.OnRetrieveValues.RemoveListener(RetrieveValue);
    }

    #endregion

    [ContextMenu("Get Value")]
    public void RetrieveValue()
    {
        if (string.IsNullOrEmpty(SourceName))
        {
            Debug.LogWarning("SourceName is empty. Cannot retrieve data.");
            return;
        }

        var components = TargetGameObject.GetComponents<Component>();

        foreach (var component in components)
        {
            FieldInfo field = component.GetType().GetField(SourceName);
            MethodInfo method = component.GetType().GetMethod(SourceName);

            if (field != null || method != null)
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

                if (value != null)
                {
                    _persistentDataManager.AddValue(DataKey, new PersistentData(value, valueType));
                    Debug.Log($"Retrieved value from component {(field != null ? "field" : "method")} {SourceName}: {value} {valueType}");
                    return;
                }
            }
        }

        Debug.LogWarning($"No field or method found with the name {SourceName} in the script or components on the GameObject.");
    }

}
