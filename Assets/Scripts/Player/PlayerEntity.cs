using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerEntity
{
    public Player PlayerInstance = null;

    [SerializeField]
    private PlayerShipsSO _playerShipsData;

    public Player GetPlayerShip(PlayerPlaneTypes type)
    {
        return _playerShipsData.PlayerPlanes[type];
    }
}
