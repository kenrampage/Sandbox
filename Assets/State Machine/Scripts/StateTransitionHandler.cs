using UnityEngine;

// keeps array of transitions and iterates through them in response to state changes
public class StateTransitionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StateManager _stateManager;

    [Header("Settings")]
    [SerializeField] private StateTransition[] _transitionArray;

    #region System Functions

    private void OnEnable()
    {
        _stateManager?.OnTryStateChange.AddListener(HandleTryStateChange);
    }

    private void OnDisable()
    {
        _stateManager?.OnTryStateChange.RemoveListener(HandleTryStateChange);
    }

    #endregion

    public void HandleTryStateChange(string currentStateName, string nextStateName)
    {
        bool stateChanged = false;

        foreach (var transition in _transitionArray)
        {
            if(transition.HandleTransition(currentStateName, nextStateName))
            {
                _stateManager.ConfirmStateChange(currentStateName, nextStateName);
                stateChanged = true;
            }
        }

        if (!stateChanged)
        {
            _stateManager.RejectStateChange(currentStateName, nextStateName);
        }
    }

}
