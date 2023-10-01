using MFlight;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform StartPoint;
    public MouseFlightController flightController;
    public Player playerPlane;

    public override void InstallBindings()
    {
        CreatePlane();
        CreateControls();
    }

    private void CreateControls()
    {
        MouseFlightController mouseFlightController =
                    Container.InstantiatePrefabForComponent<MouseFlightController>(flightController, StartPoint.position, Quaternion.identity, null);
        Container.
            Bind<MouseFlightController>().
            FromInstance(mouseFlightController).
            AsSingle();
    }

    private void CreatePlane()
    {
        Player plane =
                    Container.InstantiatePrefabForComponent<Player>(playerPlane, StartPoint.position, Quaternion.identity, null);
        Container.
            Bind<Player>().
            FromInstance(plane).
            AsSingle();
    }
}
