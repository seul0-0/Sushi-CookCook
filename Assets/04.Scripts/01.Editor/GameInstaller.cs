using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //씬에 호출시 자동생성 / NonLazy=즉시실행
        Container.Bind<IAudioManager>().To<AudioManager>().AsSingle().NonLazy();
        Container.Bind<IGameManager>().To<GameManager>().AsSingle().NonLazy();
        //참조할게있다면 씬에배치후 바인딩
        //Container.Bind<IAudioService>().FromInstance(audioManagerInScene).AsSingle();
        //Container.Bind<IGameManager>().FromInstance(gameManagerInScene).AsSingle();
    }
}

//사용예시
public class ZZZ : MonoBehaviour
{
    private IAudioManager _audio;
    private IGameManager _gameManager;

    [Inject]
    public void Construct(IAudioManager audio, IGameManager gameManager)
    {
        _audio = audio;
        _gameManager = gameManager;
    }

    void Start()
    {
        //_audio.PlaySound("공격");
        //_gameManager.AddScore(10);
    }
}