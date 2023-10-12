using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateTransition
{
    [Header("Settings")]
    [SerializeField] private string _currentStateName;
    [SerializeField] private string _nextStateName;
    private string _wildcardCharacter = "*";

    [Header("Events")]
    public UnityEvent _OnTransition;

    public bool HandleTransition(string currentStateName, string nextStateName)
    {
        if (_currentStateName == currentStateName || _currentStateName == _wildcardCharacter)
        {
            if (_nextStateName == nextStateName || _nextStateName == _wildcardCharacter)
            {
                _OnTransition?.Invoke();

                Debug.Log("Performed Transition from " + currentStateName + " to " + nextStateName);

                return true;

            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }


    }

}
