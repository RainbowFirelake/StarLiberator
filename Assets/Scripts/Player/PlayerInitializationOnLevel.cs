using MFlight;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInitializationOnLevel : MonoBehaviour
{
    public event Action<Player> OnPlayerUpdate;

    [SerializeField]
    private Transform _playerSpawnPoint;
    [SerializeField]
    private MouseFlightController _flightController;

    private PlayerEntity _playerEntity;

    [Inject]
    private void Construct(PlayerEntity playerEntity)
    {
        _playerEntity = playerEntity;
    }

    private void Start()
    {
        if (_playerEntity.PlayerInstance == null)
        {
            var player = Instantiate(_playerEntity.GetPlayerShip(PlayerPlaneTypes.Light), _playerSpawnPoint.position, _playerSpawnPoint.rotation);
            _playerEntity.PlayerInstance = player;
            _flightController.Init(player);
            OnPlayerUpdate?.Invoke(player);
        }
        else
        {
            _flightController.Init(_playerEntity.PlayerInstance);
            OnPlayerUpdate?.Invoke(_playerEntity.PlayerInstance);
        }
    }
}
