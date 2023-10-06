using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MainMenuShip : MonoBehaviour
{
    [SerializeField]
    private float _flySpeed = 100f;
    [SerializeField]
    private float _forceMultiplier = 100f;
    [SerializeField]
    private float _flightDelay = 2f;
    [SerializeField]
    private float _timeOfReturningToStart = 15f;

    private Rigidbody _rb;
    private bool _isFlying = false;
    private float _timeOfFlying = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_flightDelay > 0)
            StartCoroutine(StartFlight());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isFlying) return;

        _rb.AddRelativeForce(Vector3.forward * _flySpeed * _forceMultiplier);
    }

    private IEnumerator StartFlight()
    {
        yield return new WaitForSeconds(_flightDelay);
        _isFlying = true;
    }
}
