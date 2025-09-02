using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening.Core;       // 내부 Tween 관련

public class UI_BlurFade : MonoBehaviour
{
    [Header("Fade Panel (검은색)")]
    public Image fadePanel;

    [Header("Blur Effect Panel (원본 이미지 위에 살짝 퍼진 이미지)")]
    public Image blurPanel;

    [Header("설정")]
    public float fadeDuration = 0.5f;
    public float blurScale = 1.05f; // 퍼짐 정도
    public float blurAlpha = 0.3f;  // 블러 알파


    private void Start()
    {
         LoadSceneWithBlurFade(GameScene.Level1).Forget();
    }

    /// <summary>
    /// 씬 전환 호출
    /// </summary>
    public async UniTaskVoid LoadSceneWithBlurFade(GameScene scene)
    {
        //  블러 + 페이드 인
        blurPanel.gameObject.SetActive(true);
        blurPanel.color = new Color(1f, 1f, 1f, 0f);
        blurPanel.transform.localScale = Vector3.one;

        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0f, 0f, 0f, 0f);

        // DOTween으로 동시에 블러와 페이드 인
        var fadeTween = fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad);
        var blurTween = DOTween.Sequence()
            .Join(blurPanel.DOFade(blurAlpha, fadeDuration).SetEase(Ease.InOutQuad))
            .Join(blurPanel.transform.DOScale(blurScale, fadeDuration).SetEase(Ease.OutQuad));

        // Tween을 UniTask로 래핑
        await UniTask.WhenAll(
            TweenToUniTask(fadePanel.DOFade(1f, fadeDuration)),
            TweenToUniTask(blurPanel.DOFade(blurAlpha, fadeDuration)),
            TweenToUniTask(blurPanel.transform.DOScale(blurScale, fadeDuration))
        );

        //  씬 로드
        await SceneManager.LoadSceneAsync((int)scene).ToUniTask();

        //  페이드 + 블러 아웃
        await UniTask.WhenAll(
            TweenToUniTask(fadePanel.DOFade(0f, fadeDuration)),
            TweenToUniTask(blurPanel.DOFade(0f, fadeDuration)),
            TweenToUniTask(blurPanel.transform.DOScale(1f, fadeDuration))
        );

        // 최종 비활성화
        blurPanel.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// DOTween Tween을 UniTask로 변환
    /// </summary>
    private UniTask TweenToUniTask(Tween tween)
    {
        var tcs = new UniTaskCompletionSource();
        tween.OnComplete(() => tcs.TrySetResult());
        return tcs.Task;
    }
}
