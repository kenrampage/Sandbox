using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionDataReadWriteAdapter_Transform : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    [HideInInspector] public Vector3 Position
    {
        get { return _transform.position; }
        set { _transform.position = value; }
    }
    [HideInInspector] public Quaternion Rotation
    {
        get { return _transform.rotation; }
        set { _transform.rotation = value; }
    }
    [HideInInspector] public Vector3 LocalScale
    {
        get { return _transform.localScale; }
        set { _transform.localScale = value; }
    }
    [HideInInspector] public bool ActiveSelf
    {
        get { return _transform.gameObject.activeSelf; }
        set { _transform.gameObject.SetActive(value); }
    }

    #region Init
    private void Awake()
    {
        if (transform == null)
        {
            _transform = GetComponent<Transform>();
        }

    }

    #endregion

}
