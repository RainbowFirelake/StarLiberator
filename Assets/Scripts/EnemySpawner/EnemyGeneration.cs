using StarLiberator.Enemies;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyGeneration : MonoBehaviour
{
    public int currentLevel = 1;

    [SerializeField]
    private EnemyLevelList _levelList;
    [SerializeField]
    private GameObject _starPort;
    [SerializeField]
    private Transform _spawnTransformPoint;

    private List<EnemyController> _enemies = new();

    [Inject]
    private void Construct()
    {
        
    }

    private void Start()
    {
        GenerateStarBase(_spawnTransformPoint.position);
    }

    private void GenerateStarBase(Vector3 position)
    {
        var port = Instantiate(_starPort, 
            _spawnTransformPoint.position, Random.rotation);

        var list = _levelList.GetInfoByLevel(currentLevel);

        foreach (var obj in list)
        {
            var count = Random.Range(obj.MinCount, obj.MaxCount + 1);
            for (int i = 0; i < count; i++)
            {
                float x = Random.Range(500, 1000);
                float y = Random.Range(-500, 500);
                float z = Random.Range(-250, 750);

                var enemyShip = Instantiate(obj.Enemy, new Vector3(
                    port.transform.position.x + x, port.transform.position.y + y, port.transform.position.z + z), Quaternion.identity);
                _enemies.Add(enemyShip);
                enemyShip.OnKilled += RemoveDestroyedShip;
            }
        }
    }

    private void RemoveDestroyedShip(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }
}
