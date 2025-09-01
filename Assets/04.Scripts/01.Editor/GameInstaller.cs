using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // IAudioService 인터페이스에 AudioManager 연결, 싱글톤으로 관리
        Container.Bind<IAudioManager>().To<AudioManager>().AsSingle();

        // GameManager도 싱글톤으로 등록
        Container.Bind<IGameManager>().To<GameManager>().AsSingle();
    }
}