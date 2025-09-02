using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening.Core;       // ���� Tween ����

public class UI_BlurFade : MonoBehaviour
{
    [Header("Fade Panel (������)")]
    public Image fadePanel;

    [Header("Blur Effect Panel (���� �̹��� ���� ��¦ ���� �̹���)")]
    public Image blurPanel;

    [Header("����")]
    public float fadeDuration = 0.5f;
    public float blurScale = 1.05f; // ���� ����
    public float blurAlpha = 0.3f;  // �� ����


    private void Start()
    {
         LoadSceneWithBlurFade(GameScene.Level1).Forget();
    }

    /// <summary>
    /// �� ��ȯ ȣ��
    /// </summary>
    public async UniTaskVoid LoadSceneWithBlurFade(GameScene scene)
    {
        //  �� + ���̵� ��
        blurPanel.gameObject.SetActive(true);
        blurPanel.color = new Color(1f, 1f, 1f, 0f);
        blurPanel.transform.localScale = Vector3.one;

        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0f, 0f, 0f, 0f);

        // DOTween���� ���ÿ� ���� ���̵� ��
        var fadeTween = fadePanel.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad);
        var blurTween = DOTween.Sequence()
            .Join(blurPanel.DOFade(blurAlpha, fadeDuration).SetEase(Ease.InOutQuad))
            .Join(blurPanel.transform.DOScale(blurScale, fadeDuration).SetEase(Ease.OutQuad));

        // Tween�� UniTask�� ����
        await UniTask.WhenAll(
            TweenToUniTask(fadePanel.DOFade(1f, fadeDuration)),
            TweenToUniTask(blurPanel.DOFade(blurAlpha, fadeDuration)),
            TweenToUniTask(blurPanel.transform.DOScale(blurScale, fadeDuration))
        );

        //  �� �ε�
        await SceneManager.LoadSceneAsync((int)scene).ToUniTask();

        //  ���̵� + �� �ƿ�
        await UniTask.WhenAll(
            TweenToUniTask(fadePanel.DOFade(0f, fadeDuration)),
            TweenToUniTask(blurPanel.DOFade(0f, fadeDuration)),
            TweenToUniTask(blurPanel.transform.DOScale(1f, fadeDuration))
        );

        // ���� ��Ȱ��ȭ
        blurPanel.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// DOTween Tween�� UniTask�� ��ȯ
    /// </summary>
    private UniTask TweenToUniTask(Tween tween)
    {
        var tcs = new UniTaskCompletionSource();
        tween.OnComplete(() => tcs.TrySetResult());
        return tcs.Task;
    }
}
