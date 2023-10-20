using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersistentDataDisplay : MonoBehaviour
{
    [SerializeField] private PersistentDataManager _persistentDataManager;

    private List<GameObject> _currentTextObjects = new List<GameObject>();

    private void Awake()
    {
        if (_persistentDataManager == null)
        {
            _persistentDataManager = PersistentDataManager.Instance;
        }
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
        foreach (var dataEntry in _persistentDataManager._data)
        {
            AddDataEntry("Key: " + dataEntry.Key + " | Type: " + dataEntry.Value.ValueType.ToString() + " | Value: " + dataEntry.Value.Value);
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
