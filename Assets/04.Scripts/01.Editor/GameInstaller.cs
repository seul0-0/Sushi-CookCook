using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private AudioManager audioManagerPrefab;
    [SerializeField] private GameManager gameManagerPrefab;
    public override void InstallBindings()
    {
        Debug.Log("[Installer] InstallBindings ȣ���");
        // ProjectContext������ Prefab���� FromComponentInNewPrefab�� ����� �� ���� ����
        Container.Bind<IAudioManager>()
                 .To<AudioManager>()
                 .FromComponentInNewPrefab(audioManagerPrefab)
                 .AsSingle()
                 .NonLazy(); // ��� ����
        Container.Bind<IGameManager>()
                 .To<GameManager>()
                 .FromComponentInNewPrefab(gameManagerPrefab)
                 .AsSingle()
                 .NonLazy();
        Debug.Log("[Installer] ���ε� �Ϸ�");
    }
}

//projectcontext ���� �ȵɋ� Test
//public class ProjectContextDebugger : MonoBehaviour
//{
//    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//    static void CheckProjectContext()
//    {
//        var prefab = Resources.Load("ProjectContext");
//        Debug.Log("[Debug] ProjectContext found: " + (prefab != null));
//    }
//}