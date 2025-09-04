using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawner : MonoBehaviour
{
    [Header("스크립터블 오브젝트 연결 (원본)")]
    public List<EnemyData> originalEnemyDatas = new List<EnemyData>();

    [Header("클론된 몬스터 데이터 (런타임 전용)")]
    public List<EnemyData> clonedEnemyDatas = new List<EnemyData>();

    [Header("UI 연결")]
    public TextMeshProUGUI enemyNameText;
    public Image enemySpriteImage;
    public Image healthBarImage; // Slider 대신 Image 사용

    [Header("스테이지 구성 (원본 참조)")]
    public List<StageData> stageEnemyOrders = new List<StageData>();


    private int currentStageIndex = 0;
    private int currentEnemyIndex = -1;
    private int currentHealth;
    private int maxHealth;

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


        NextEnemy();
    }

    // 다음 적 등장
    private void NextEnemy()
    {
        currentEnemyIndex++;

        if (currentEnemyIndex >= clonedEnemyDatas.Count)
        {
            Debug.Log("스테이지 클리어!");
            enemyNameText.text = "";
            enemySpriteImage.sprite = null;
            healthBarImage.fillAmount = 0f;
            return;
        }

        EnemyData enemy = clonedEnemyDatas[currentEnemyIndex];
        maxHealth = enemy.health;
        currentHealth = maxHealth;

        // UI 세팅
        //enemyNameText.text = enemy.enemyName;
        SpriteRenderer sr = enemy.enemyPrefab.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            enemySpriteImage.sprite = sr.sprite;
        }        UpdateHealthBar();

        Debug.Log(enemy.enemyName + " 등장! (체력 " + enemy.health + ")");
    }

    // 체력 감소
    public void DamageEnemy(int damage)
    {
        Debug.Log("DamageEnemy 호출됨! damage = " + damage);
        if (currentEnemyIndex < 0 || currentEnemyIndex >= clonedEnemyDatas.Count) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log(clonedEnemyDatas[currentEnemyIndex].enemyName + " 처치!");
            NextEnemy();
        }
    }

    // 체력바 갱신 (Image.fillAmount)
    private void UpdateHealthBar()
    {
        if (maxHealth > 0 && healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }
    
}
