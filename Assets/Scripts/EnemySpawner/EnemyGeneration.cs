using Sirenix.OdinInspector;
using StarLiberator.Enemies;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyGeneration : SerializedMonoBehaviour
{
    public int currentLevel = 1;

    [SerializeField]
    private EnemyLevelList _levelList;
    [SerializeField]
    private Transform _port;

    private List<EnemyController> _enemies = new();
    private Player _player;
    
    private PlayerInitializationOnLevel _playerInit;
    private RectangleOnUnits _unitTracker;
    private EnemyFactory _enemyFactory;

    [Inject]
    private void Construct(PlayerInitializationOnLevel playerInit, EnemyFactory enemyFactory)
    {
        _playerInit = playerInit;
        _playerInit.OnPlayerUpdate += Init;
        _enemyFactory = enemyFactory;
    }

    private void Init(Player player)
    {
        _player = player;
        GenerateStarterEnemies();
    }

    private void OnDestroy()
    {
        _playerInit.OnPlayerUpdate -= Init;
    }

    private void GenerateStarterEnemies()
    {
        var enemyLevelList = _levelList.GetInfoByLevel(currentLevel);

        foreach (var enemyLevelInfo in enemyLevelList)
        {
            var count = Random.Range(enemyLevelInfo.MinCount, enemyLevelInfo.MaxCount + 1);
            for (int i = 0; i < count; i++)
            {
                CreateShipAtRandomPoint(enemyLevelInfo.Enemy);  
            }
        }
    }

    [Button("Instantiate Random enemy (works only in runtime)")]
    private void CreateRandomShip()
    {
        CreateShipAtRandomPoint(_levelList.GetRandomEnemyAtLevel(currentLevel));
    }

    private void CreateShipAtRandomPoint(EnemyController enemy)
    {
        float x = Random.Range(500, 1000);
        float y = Random.Range(-500, 500);
        float z = Random.Range(-250, 750);

        var enemyShip = _enemyFactory.Create(enemy, new Vector3(
            _port.position.x + x, _port.position.y + y, _port.position.z + z), Quaternion.identity);

        enemyShip.GetComponent<EnemyController>().Init(_player);
        enemyShip.GetComponent<EnemyController>().OnKilled += RemoveDestroyedShip;

        _enemies.Add(enemyShip.GetComponent<EnemyController>());
    }

    private void RemoveDestroyedShip(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }
}
