using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVariables : MonoBehaviour
{
    [Tooltip("field: DemoString")]
    public string DemoString;

    [SerializeField, Tooltip("property: DemoBool")]
    private bool _demoBool;

    
    public bool DemoBool
    {
        get { return _demoBool; }
        set { _demoBool = value; }
    }

    [Tooltip("method: GetDemoInt() | SetDemoInt()")]
    [SerializeField] private int _demoInt;
    public int GetDemoInt()
    {
        return _demoInt;
    }

    public void SetDemoInt(int demoInt)
    {
        _demoInt = demoInt;
    }
}
