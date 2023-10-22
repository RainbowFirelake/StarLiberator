using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public partial class PlayerShipsSO : SerializedScriptableObject
{
    [DictionaryDrawerSettings(KeyLabel = "Ship Type", ValueLabel = "Ship Prefab")]
    public Dictionary<PlayerPlaneTypes, Player> PlayerPlanes = new();
}
