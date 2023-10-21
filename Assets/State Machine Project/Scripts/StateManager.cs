using UnityEngine;
using UnityEngine.Events;

//Tracks current state and previous state and invokes OnStateChange event when state changes
public class StateManager : MonoBehaviour
{
    [Header("Data (Read Only)")]
    [SerializeField] private string _previousStateName;
    [SerializeField] private string _currentStateName;

    [Header("Events")]
    [HideInInspector] public UnityEvent<string, string> OnTryStateChange;

    public void TryChangeState(string nextStateName)
    {
        OnTryStateChange?.Invoke(_currentStateName, nextStateName);
    }

    public void ConfirmStateChange(string previousStateName, string nextStateName)
    {
        _previousStateName = previousStateName;
        _currentStateName = nextStateName;

        print(gameObject.name + " | Confirmed State Change from " + _previousStateName + " to " + _currentStateName);
    }

    public void RejectStateChange(string previousStateName, string nextStateName)
    {
        //A place for logic later
    }

}
