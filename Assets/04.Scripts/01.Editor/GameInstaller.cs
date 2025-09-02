using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //���� ȣ��� �ڵ����� / NonLazy=��ý���
        Container.Bind<IAudioManager>().To<AudioManager>().AsSingle().NonLazy();
        Container.Bind<IGameManager>().To<GameManager>().AsSingle().NonLazy();
        //�����Ұ��ִٸ� ������ġ�� ���ε�
        //Container.Bind<IAudioService>().FromInstance(audioManagerInScene).AsSingle();
        //Container.Bind<IGameManager>().FromInstance(gameManagerInScene).AsSingle();
    }
}

//��뿹��
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
        //_audio.PlaySound("����");
        //_gameManager.AddScore(10);
    }
}