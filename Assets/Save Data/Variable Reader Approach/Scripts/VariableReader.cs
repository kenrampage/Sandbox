using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class VariableReader : MonoBehaviour
{
    [SerializeField] private PersistentDataManager _persistentDataManager;

    public string DataKey;
    public string SourceName; 
    public GameObject TargetGameObject;

    //private Dictionary<string, object> retrievedValues = new Dictionary<string, object>();

    [ContextMenu("Get Value")]
    // Dynamically retrieve values based on the TargetDataSource string
    public void RetrieveValue()
    {
        // Check if the TargetDataSource is valid
        if (string.IsNullOrEmpty(SourceName))
        {
            Debug.LogWarning("TargetDataSource is empty. Cannot retrieve data.");
            return;
        }

        // First, try to find a field with the given name in this script's class
        //FieldInfo field = TargetGameObject.GetType().GetField(SourceName);
        //field = TargetComponent.GetType().GetField(TargetDataSource);

        //if (field != null)
        //{
        //    object value = field.GetValue(TargetComponent);
        //    retrievedValues[TargetDataSource] = value;
        //    Debug.Log($"Retrieved value from component field {TargetDataSource}: {value}");
        //    return;
        //}


        // If the field is not found, try to find a method with the given name in this script's class
        //MethodInfo method = TargetGameObject.GetType().GetMethod(SourceName);
        //method = TargetComponent.GetType().GetMethod(TargetDataSource);

        //if (method != null)
        //{
        //    object value = method.Invoke(TargetComponent, null);
        //    retrievedValues[TargetDataSource] = value;
        //    Debug.Log($"Retrieved value from component method {TargetDataSource}: {value}");
        //    return;
        //}

        //If neither field nor method is found in this script, check components on the same GameObject
        var components = TargetGameObject.GetComponents<Component>();

        foreach (var component in components)
        {
            FieldInfo field = component.GetType().GetField(SourceName);

            if (field != null)
            {
                object value = field.GetValue(component);
                _persistentDataManager.AddValue(DataKey, new PersistentData(value, field.FieldType));
                //retrievedValues[SourceName] = value;
                Debug.Log($"Retrieved value from component field {SourceName}: {value} {field.FieldType}");
                return;
            }

            MethodInfo method = component.GetType().GetMethod(SourceName);

            if (method != null)
            {
                object value = method.Invoke(component, null);
                _persistentDataManager.AddValue(DataKey, new PersistentData(value, method.ReturnType));
                //retrievedValues[SourceName] = value;
                Debug.Log($"Retrieved value from component method {SourceName}: {value} {method.ReturnType}");
                return;
            }
        }

        Debug.LogWarning($"No field or method found with the name {SourceName} in the script or components on the GameObject.");
    }

}
