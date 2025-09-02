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
        Debug.Log("[Installer] InstallBindings 호출됨");
        // ProjectContext에서는 Prefab으로 FromComponentInNewPrefab을 사용해 한 번만 생성
        Container.Bind<IAudioManager>()
                 .To<AudioManager>()
                 .FromComponentInNewPrefab(audioManagerPrefab)
                 .AsSingle()
                 .NonLazy(); // 즉시 생성
        Container.Bind<IGameManager>()
                 .To<GameManager>()
                 .FromComponentInNewPrefab(gameManagerPrefab)
                 .AsSingle()
                 .NonLazy();
        Debug.Log("[Installer] 바인딩 완료");
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