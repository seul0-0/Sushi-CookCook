using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    private MonsterSpawner _monsterSpawner;

    public TextMeshProUGUI ingredient;
    public TextMeshProUGUI ingredientHp;
    public TextMeshProUGUI price;
    
    private int _stageNum;

    public void OnEnable()
    {
        _monsterSpawner = MonsterSpawner.Instance;

        if (_monsterSpawner.currentStageIndex >= 0)
        {
            _stageNum = _monsterSpawner.currentStageIndex - 1;

            StageData enemiesArray = _monsterSpawner.stageEnemyOrders[_stageNum];

            ingredient.text = null;
            ingredientHp.text = null;
            price.text = null;

            foreach (EnemyData i in enemiesArray.enemies)
            {
                ingredient.text += $"{i.enemyName}\n";
                ingredientHp.text += $"{i.health}\n";
            }

            StatusManager.Instance.ChangeMoneyValue(_monsterSpawner.currentStageIndex * 2 + 10);

            price.text = $" + ({_monsterSpawner.currentStageIndex * 2 + 10}) = {StatusManager.Instance.currentStatus.money}";
        }
    }


}
