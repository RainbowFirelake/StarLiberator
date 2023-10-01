using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyVFXSpawner : MonoBehaviour
{
    [SerializeField]
    private Health _health;
    [SerializeField]
    private GameObject _VFX;

    private void OnEnable()
    {
        _health.OnDie += CreateVFX;
    }

    private void CreateVFX()
    {
        Instantiate(_VFX, transform.position, transform.rotation);
    }
}
