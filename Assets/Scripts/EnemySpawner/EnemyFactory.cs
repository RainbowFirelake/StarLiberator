using StarLiberator.Enemies;
using UnityEngine;
using Zenject;

public class EnemyFactory
{
    private DiContainer _container;

    public EnemyFactory(DiContainer container)
    {
        _container = container;
    }

    public GameObject Create(EnemyController enemy, Vector3 position, Quaternion rotation)
    {
        return _container.InstantiatePrefab(enemy, position, rotation, null);
    }
}
