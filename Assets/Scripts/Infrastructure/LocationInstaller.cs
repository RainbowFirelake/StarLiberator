using MFlight;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField]
    private EnemyGeneration _enemyGenerator;
    [SerializeField]
    private PlayerInitializationOnLevel _playerInit;
    [SerializeField]
    private MouseFlightController flightController;
    [SerializeField]
    private RectangleOnUnits _unitTracker;

    public override void InstallBindings()
    {        
        BindPlayerInitializer();
        BindEnemyFactory();
        BindEnemyGeneration();
        BindFlightController();
        BindUnitTracker();
    }

    private void BindFlightController()
    {
        Container.
            Bind<MouseFlightController>().
            FromInstance(flightController).
            AsSingle();
    }

    private void BindEnemyFactory()
    {
        Container
            .Bind<EnemyFactory>()
            .FromNew()
            .AsSingle();
    }

    private void BindPlayerInitializer()
    {
        Container
            .Bind<PlayerInitializationOnLevel>()
            .FromInstance(_playerInit)
            .AsSingle();
    }

    private void BindEnemyGeneration()
    {
        Container
            .Bind<EnemyGeneration>()
            .FromInstance(_enemyGenerator)
            .AsSingle();
    }

    private void BindUnitTracker()
    {
        Container
            .Bind<RectangleOnUnits>()
            .FromInstance(_unitTracker)
            .AsSingle();
    }
}
