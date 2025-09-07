using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    [Header("스크립터블 오브젝트 연결 (원본)")]
    public List<EnemyData> originalEnemyDatas = new List<EnemyData>();

    [Header("클론된 몬스터 데이터 (런타임 전용)")]
    public List<EnemyData> clonedEnemyDatas = new List<EnemyData>();

    [Header("UI 연결")]
    public TextMeshProUGUI enemyNameText;
    public GameObject enemySpriteImage;
    public Image healthBarImage; 
    private SpriteRenderer enemySpriteRenderer;   // SpriteRenderer 사용

    public TextMeshProUGUI stageTitleText;  // 스테이지 이름
    public TextMeshProUGUI stageCountText;  // 스테이지 카운트

    // === 클리어시 영수증 ===
    public GameObject reciptPanel;

    [Header("스테이지 구성 (원본 참조)")]
    public List<StageData> stageEnemyOrders = new List<StageData>();

    private int currentStageIndex = 0;
    [SerializeField]
    private int currentEnemyIndex = -1;
    private float currentHealth;
    private int maxHealth;

    protected override void Awake()
    {
        base.Awake();

        enemySpriteRenderer = enemySpriteImage.GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        StartStage(0); // 0번 스테이지부터 시작
    }
    
    // 스테이지 시작
    public void StartStage(int stageIndex)
    {
        currentStageIndex = stageIndex;
        currentEnemyIndex = -1;

        // 스테이지용 클론 생성
        clonedEnemyDatas.Clear();
        foreach (EnemyData enemy in stageEnemyOrders[stageIndex].enemies)
        {
            EnemyData clone = Instantiate(enemy); // 런타임 클론 생성
            clonedEnemyDatas.Add(clone);
        }
        
        stageTitleText.text = $"{stageEnemyOrders[stageIndex].stageName} (0/{clonedEnemyDatas.Count})";

        // 적 소환 시작
        NextEnemy();
    }

    // 다음 적 등장
    private void NextEnemy()
    {
        currentEnemyIndex++;

        if (currentEnemyIndex < clonedEnemyDatas.Count)
        {
            EnemyData enemy = clonedEnemyDatas[currentEnemyIndex];
            maxHealth = enemy.health;
            currentHealth = maxHealth;
            enemyNameText.text = enemy.enemyName;

            if (enemy.image != null && enemySpriteRenderer != null)
            {
                enemySpriteRenderer.sprite = enemy.image;
            }
            UpdateHealthBar();
            
            stageTitleText.text = $"{stageEnemyOrders[currentStageIndex].stageName} ({currentEnemyIndex}/{clonedEnemyDatas.Count})";
        }
        else
        {
            enemyNameText.text = "";
            if (enemySpriteRenderer != null) enemySpriteRenderer.sprite = null;
            healthBarImage.fillAmount = 0f;

            // 마지막 스테이지 체크
            if (currentStageIndex + 1 < stageEnemyOrders.Count)
            {
                StartStage(currentStageIndex + 1); // 다음 스테이지 시작
                StageClear();
            }
            else
            {
                // TODO: 엔딩 UI 표시
                // endingCanvas.SetActive(true);

                // 에디터 테스트용 종료
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit(); // 빌드 실행 시 종료
#endif
            }
        }
    }

    // 체력 감소
    public void DamageEnemy(float damage)
    {
        if (currentEnemyIndex < 0 || currentEnemyIndex >= clonedEnemyDatas.Count) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            NextEnemy();
        }
    }

    // 체력바 갱신 (Image.fillAmount)
    private void UpdateHealthBar()
    {
        if (maxHealth > 0 && healthBarImage != null)
        {
            float ratio = (float)currentHealth / maxHealth;
            healthBarImage.fillAmount = ratio;
        }
    }

    private void StageClear()
    {
        reciptPanel.SetActive(true);
    }
}