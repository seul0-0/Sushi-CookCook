using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // IAudioService �������̽��� AudioManager ����, �̱������� ����
        Container.Bind<IAudioManager>().To<AudioManager>().AsSingle();

        // GameManager�� �̱������� ���
        Container.Bind<IGameManager>().To<GameManager>().AsSingle();
    }
}