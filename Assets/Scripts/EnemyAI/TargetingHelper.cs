using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingHelper : MonoBehaviour
{
    private Transform _transform;
    private Transform _target;
    private Transform _parent = null;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    public void Initialize(Transform parent)
    {
        _parent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            _transform.LookAt(_target);
        }
    }

    public Vector3 GetDirection()
    {
        return _transform.forward;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }
}
