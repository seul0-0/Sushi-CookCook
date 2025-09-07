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
        // ProjectContext에서는 Prefab으로 FromComponentInNewPrefab을 사용해 한 번만 생성
        Container.Bind<IAudioManager>()
                 .To<AudioManager>()
                 .FromComponentInHierarchy()  // 기존 오브젝트 참조
                 .AsSingle()
                 .NonLazy(); // 즉시 생성
        Container.Bind<IGameManager>()
                 .To<GameManager>()
                 .FromComponentInHierarchy()
                 .AsSingle()
                 .NonLazy();
    }
}

//projectcontext 실행 안될떄 Test
//public class ProjectContextDebugger : MonoBehaviour
//{
//    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//    static void CheckProjectContext()
//    {
//        var prefab = Resources.Load("ProjectContext");
//        Debug.Log("[Debug] ProjectContext found: " + (prefab != null));
//    }
//}