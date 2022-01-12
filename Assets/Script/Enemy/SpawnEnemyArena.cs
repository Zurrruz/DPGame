using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyArena : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemy;
    public bool _thereIsCharacter;
    [SerializeField, Range(1, 5)]
    private int _countEnemy;
    [SerializeField]
    private bool _rewardLow;
    [SerializeField]
    private bool _rewardMedium;
    [SerializeField]
    private bool _rewardHard;

    BattleManager battleManager;

    private void Awake()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
    }

    private void Start()
    {
        BattleManager.addEnemy += SpawnEnemy;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _thereIsCharacter = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _thereIsCharacter = false;
    }

    private void SpawnEnemy()
    {
        if (_thereIsCharacter) 
        {
            battleManager.CountEnemy(_countEnemy);
            battleManager.LevelReward(_rewardLow, _rewardMedium, _rewardHard);
            for (int i = 0; i < _enemy.Count; i++)
            {
                battleManager.AddEnemyList(_enemy[i]);
                //BattleManager.enemy.Add(_enemy[i]);
            }
        }
    }
}
