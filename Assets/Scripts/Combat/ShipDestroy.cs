using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroy : MonoBehaviour
{
    [SerializeField]
    private List<Rigidbody> _rigidbodies = new List<Rigidbody>();

    private void OnDestroy()
    {
        foreach (var rigidbody in _rigidbodies)
        {
            
        }
    }
}
