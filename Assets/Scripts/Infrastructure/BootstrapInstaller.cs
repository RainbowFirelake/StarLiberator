using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerEntity _playerEntity;

    public override void InstallBindings()
    {
        BindPlayerEntity();
    }

    private void BindPlayerEntity()
    {
        Container
            .Bind<PlayerEntity>()
            .FromInstance(_playerEntity)
            .AsSingle();
    }
}
