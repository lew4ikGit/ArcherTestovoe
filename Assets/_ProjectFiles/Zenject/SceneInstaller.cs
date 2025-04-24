using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private InputController inputController;
    [SerializeField] private TrajectoryRenderer trajectoryRenderer;
    [SerializeField] private ArcherAnimations archerAnimatons;


    public override void InstallBindings()
    {
        Container.Bind<InputController>()
             .FromInstance(inputController)
             .AsSingle();

        Container.Bind<TrajectoryRenderer>()
            .FromInstance(trajectoryRenderer)
            .AsSingle();

        Container.Bind<ArcherAnimations>()
           .FromInstance(archerAnimatons)
           .AsSingle();


    }
}
