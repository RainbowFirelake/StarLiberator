using MFlight;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    //[SerializeField]
    //private SunSystemGeneration _sunGenerator;
    [SerializeField]
    private EnemyGeneration _enemyGenerator;
    [SerializeField]
    private PlayerInitializationOnLevel _playerInit;
    [SerializeField]
    private MouseFlightController flightController;

    //public Transform StartPoint;

    //public Player playerPlane;

    public override void InstallBindings()
    {
        //CreatePlane();
        //BindSunSystemGenerator();
        
        BindPlayerInitializer();
        BindEnemyGeneration();
        BindFlightController();
    }

    private void BindFlightController()
    {
        Container.
            Bind<MouseFlightController>().
            FromInstance(flightController).
            AsSingle();
    }

    //private void CreatePlane()
    //{
    //    Player plane =
    //                Container.InstantiatePrefabForComponent<Player>(playerPlane, StartPoint.position, Quaternion.identity, null);
    //    Container.
    //        Bind<Player>().
    //        FromInstance(plane).
    //        AsSingle();
    //}

    //private void BindSunSystemGenerator()
    //{
    //    Container
    //        .Bind<SunSystemGeneration>()
    //        .FromInstance(_sunGenerator)
    //        .AsSingle();
    //}

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
}
