using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SunBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _light;

    private Transform _player;
    private PlayerInitializationOnLevel _playerInit;

    [Inject]
    private void Construct(PlayerInitializationOnLevel playerInit)
    {
        _playerInit = playerInit;
        _playerInit.OnPlayerUpdate += Init;
    }

    private void OnDestroy()
    {
        _playerInit.OnPlayerUpdate -= Init;
    }

    private void Init(Player player)
    {
        _player = player.transform;
    }   

    private void Update()
    {
        if (_player != null && _light != null)
        {
            _light.LookAt(_player.position);
        }
    }

}
