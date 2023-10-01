using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarAlign : MonoBehaviour
{
    private Camera _mainCam;
    private Transform _transform;

    void Start()
    {
        _mainCam = Camera.main;
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainCam == null) return;

        _transform.rotation = _mainCam.transform.rotation;
    }
}
