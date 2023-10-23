using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVariables : MonoBehaviour
{
    [Tooltip("field: Name")]
    public string Name;

    [SerializeField, Tooltip("property: IsAlive")]
    private bool _isAlive;
    public bool IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    [Tooltip("method: GetLevel() | SetLevel()")]
    [SerializeField] private int _level;
    public int GetLevel()
    {
        return _level;
    }
    public void SetLevel(int level)
    {
        _level = level;
    }
}
