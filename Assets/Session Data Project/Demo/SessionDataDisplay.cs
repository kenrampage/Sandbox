using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SessionDataDisplay : MonoBehaviour
{
    private SessionDataManager _sessionDataManager;

    private List<GameObject> _currentTextObjects = new List<GameObject>();

    private void Awake()
    {
        _sessionDataManager = SessionDataManager.Instance;
    }

    public GameObject TextTemplate;

    private void AddDataEntry(string text)
    {
        GameObject go = Instantiate(TextTemplate, this.transform);
        _currentTextObjects.Add(go);
        TextTemplate.GetComponent<TextMeshProUGUI>().text = text;
    }

    [ContextMenu("Add All Data")]
    public void AddAllDataEntries()
    {
        foreach (var dataEntry in _sessionDataManager.GetDictionary())
        {
            AddDataEntry("Key: " + dataEntry.Key + " | Value: " + dataEntry.Value.Value + " | Type: " + dataEntry.Value.ValueType.ToString());
        }
    }

    [ContextMenu("Clear All")]
    public void ClearAll()
    {
        foreach (var obj in _currentTextObjects)
        {
            DestroyImmediate(obj);
        }

        _currentTextObjects.Clear();
    }
}
