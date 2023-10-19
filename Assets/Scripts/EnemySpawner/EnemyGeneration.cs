using StarLiberator.Enemies;
using System.Collections;
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

    private List<EnemyController> _enemies = new();

    [Inject]
    private void Construct(SunSystemGeneration _sunGeneration)
    {
        _sunGeneration.OnPlanetGenerate += GenerateStarBase;
    }

    private void GenerateStarBase(Vector3 position)
    {
        int x = Random.Range(-500, 500);
        int z = Random.Range(1500, 2500);
        int y = Random.Range(-700, 700);

        var port = Instantiate(_starPort, 
            new Vector3(position.x + x, position.y + y, position.z + z), Random.rotation);

        var list = _levelList.GetInfoByLevel(currentLevel);

        foreach (var obj in list)
        {
            var count = Random.Range(obj.MinCount, obj.MaxCount + 1);
            for (int i = 0; i < count; i++)
            {
                x = Random.Range(500, 1000);
                y = Random.Range(-500, 500);
                z = Random.Range(-250, 750);

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
