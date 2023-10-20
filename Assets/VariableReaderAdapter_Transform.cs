using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableReaderAdapter_Transform : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    public Vector3 Position
    {
        get { return _transform.position; }
        set { _transform.position = value; }
    }

    [HideInInspector]
    public Quaternion Rotation
    {
        get { return _transform.rotation; }
        set { _transform.rotation = value; }
    }

    [HideInInspector]
    public Vector3 Scale
    {
        get { return _transform.localScale; }
        set { _transform.localScale = value; }
    }

    public bool ActiveSelf
    {
        get { return _transform.gameObject.activeSelf; }
        set { _transform.gameObject.SetActive(value); }
    }

    private void Awake()
    {
        if (transform == null)
        {
            _transform = GetComponent<Transform>();
        }

    }

    public Vector3 GetPosition()
    {
        return Position;
    }
}
