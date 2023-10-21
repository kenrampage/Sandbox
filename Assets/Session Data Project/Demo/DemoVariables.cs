using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoVariables : MonoBehaviour
{
    public string DemoString = "Demo String Value";
    public bool DemoBool = false;


    [SerializeField] private int _demoInt = 58;
    public int GetDemoInt()
    {
        return _demoInt;
    }

    public void SetDemoInt(int demoInt)
    {
        _demoInt = demoInt;
    }
}
