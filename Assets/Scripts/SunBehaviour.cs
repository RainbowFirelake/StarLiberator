using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SunBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _light;

    private Transform _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player.transform;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player != null && _light != null)
        {
            _light.LookAt(_player.transform.position);
        }
    }

}
