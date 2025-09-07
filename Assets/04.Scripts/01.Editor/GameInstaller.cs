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
        // ProjectContext������ Prefab���� FromComponentInNewPrefab�� ����� �� ���� ����
        Container.Bind<IAudioManager>()
                 .To<AudioManager>()
                 .FromComponentInHierarchy()  // ���� ������Ʈ ����
                 .AsSingle()
                 .NonLazy(); // ��� ����
        Container.Bind<IGameManager>()
                 .To<GameManager>()
                 .FromComponentInHierarchy()
                 .AsSingle()
                 .NonLazy();
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