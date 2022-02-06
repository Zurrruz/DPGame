using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Character character;
    public QueueManager queueManager;
    [SerializeField]
    public float _queueStep;

    [SerializeField]
    private GameObject _canvasInfoArena;

    private RewardForVictory _rewardForVictory;
    [SerializeField]
    private GameObject _takeItemButton;

    public bool _step;

    public float _timer;

    private bool _rewardLow;
    private bool _rewardMedium;
    private bool _rewardHard;

    public static bool physicsDamage;
    public static bool magicDamage;

    public List<GameObject> enemy;
    [SerializeField]
    private List<Transform> _spawnEnemy;
    public int _countEnemy;

    public List<GameObject> listEnemy;
    public List<QueueAttack> actualQueueAttack;

    public delegate void AddEnemy();
    public static event AddEnemy addEnemy;

    public delegate void DestroyEnemy();
    public static event DestroyEnemy destroyEnemy;

    public delegate void ResetCooldownSpell();
    public static event ResetCooldownSpell resetCooldownSpell;

    private void Start()
    {
        _rewardForVictory = GetComponent<RewardForVictory>();
        //_canvasInfoArena.SetActive(false);
        SpawnEnemyToTheArena();
        PlayerPrefs.SetInt("Lvl", _countEnemy);
        OpenBox.take += NextLvl;
    }

    private void OnDestroy()
    {
        OpenBox.take -= NextLvl;
    }

    public void QueueAttack()
    {
        StartCoroutine(AttackEnemy());
    }  

    private void AddInitiayive()
    {
        foreach (var a in actualQueueAttack)
        {
            a.AddActualInitiative();
        }
    }

    IEnumerator AttackEnemy()
    {
        while (true)
        {
            actualQueueAttack.Sort(delegate ( QueueAttack left, QueueAttack right)
            {
                if (left._actualStepInitiative == right._actualStepInitiative) return 0;
                else if (left._actualStepInitiative > right._actualStepInitiative) return -1;
                else if (right._actualStepInitiative > left._actualStepInitiative) return 1;
                else return left._actualStepInitiative.CompareTo(right._actualStepInitiative);
            });

            if (actualQueueAttack[0]._actualStepInitiative >= _queueStep)
            {
                if (actualQueueAttack[0]._character)
                {
                    Character.isActive = true;
                    break;
                }
                else if(actualQueueAttack[0]._enemy)
                {
                    actualQueueAttack[0].GetComponent<Enemy>().ActionEnemy();
                    //yield return new WaitForSeconds(actualQueueAttack[0].GetComponent<Enemy>().DelayBetweenEnemies());
                    //queueManager.QueueAttack();
                    break;
                }
            }
            else
            {
                AddInitiayive();
            }
        }        yield return null;
    }


    public float QueueStep()
    {
        return _queueStep;
    }

    public void SpawnEnemyToTheArena()
    {
        if (enemy.Count > 0)
        {
            // enemy.Clear();
            for (int i = 0; i < actualQueueAttack.Count; i++)
            {
                if (actualQueueAttack[i]._enemy)
                    actualQueueAttack.RemoveAt(i);
            }
            for (int i = 0; i < queueManager._queueAttacks.Count; i++)
            {
                if (queueManager._queueAttacks[i]._enemy)
                    queueManager._queueAttacks.RemoveAt(i);
            }
        }
        //addEnemy();
        StartCoroutine(SpawnEnemy());
    }

    public void AddEnemyList(GameObject e)
    {
        enemy.Add(e);
    }
    public void CountEnemy(int c)
    {
        _countEnemy = c;
    }

    bool _shamanIsOn;
    int r;
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        //_canvasInfoArena.SetActive(true);
        for (int i = 0; i < _countEnemy; i++)
        {
            if(_countEnemy == 1 || _shamanIsOn)
                r = Random.Range(0, enemy.Count - 1);
            else
                r = Random.Range(0, enemy.Count);

            Instantiate(enemy[r], _spawnEnemy[i]);
            if (enemy[r].GetComponent<MagicSpels>().IsShaman())
            {
                //enemy.RemoveAt(r);
                _shamanIsOn = true;
            }
        }
    }

    public void LevelReward(bool low, bool medium, bool hard)
    {
        _rewardLow = low;
        _rewardMedium = medium;
        _rewardHard = hard;
    }

    public void DeadEnemy()
    {
        StartCoroutine(DeaEnemyCor());
    }
    public GameObject win; // удалить после приема ДП
    IEnumerator DeaEnemyCor()
    {
        _countEnemy--;
        if (_countEnemy <= 0)
        {
            if (PlayerPrefs.GetInt("Lvl") == 6)
            {
                win.SetActive(true);
            }
            else
            {
                yield return new WaitForSeconds(3f);
                _rewardForVictory.ShowRewardLow(true, false, false);
                _takeItemButton.SetActive(true);
                character.Stats();
                //_canvasInfoArena.SetActive(false);
                destroyEnemy();
            }
        }
        else
        {
            QueueAttack();
            queueManager.QueueAttack();
        }
    }

    public GameObject takeItemButton; // Удалить просле приема диплома.
    public static  bool _nextLel;
    private void NextLvl()
    {
        StartCoroutine(NextLvlCor());
    }
    IEnumerator NextLvlCor()
    {        
        if (_nextLel)
        {
            _countEnemy = PlayerPrefs.GetInt("Lvl") + 1;
            SpawnEnemyToTheArena();
            PlayerPrefs.SetInt("Lvl", _countEnemy);
            _nextLel = false;
            takeItemButton.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            _rewardForVictory.ShowRewardLow(false, true, false);
            _nextLel = true;
        }
    }

    public void ResetCooldown()
    {
        resetCooldownSpell();
    }
}
