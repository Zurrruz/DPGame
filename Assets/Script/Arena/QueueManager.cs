using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public BattleManager battleManager;
    public List<QueueAttack> _queueAttacks;
    [SerializeField]
    private List<Transform> _attackQueue;

    public List<GameObject> icon;

    public delegate void AddStepInitiative();
    public static event AddStepInitiative addStepInitiative;

    public float queue;

    public void QueueAttack()
    {
        foreach (var item in _queueAttacks)
        {
            item.ToRenewStepInitiative();
        }
        DeleteIcon();
        StartCoroutine(AttackEnemy());
    }

    IEnumerator AttackEnemy()
    {
        for (int i = 0; i < _attackQueue.Count; i++)
        {
            while (true)
            {
                _queueAttacks.Sort(delegate (QueueAttack left, QueueAttack right)
                {
                    if (left._stepInitiative == right._stepInitiative) return 0;
                    else if (left._stepInitiative > right._stepInitiative) return -1;
                    else if (right._stepInitiative > left._stepInitiative) return 1;
                    else return left._stepInitiative.CompareTo(right._stepInitiative);
                });

                if (_queueAttacks[0]._stepInitiative >= battleManager.QueueStep())
                {                    
                    Instantiate(_queueAttacks[0].icon, _attackQueue[i]);
                    _queueAttacks[0].RemoveQueueStep(battleManager.QueueStep());
                    break;
                }
                else
                {
                    addStepInitiative();
                }
            }
        }
        yield return null;
    }

    public void DeleteIcon()
    {
        foreach (var item in icon)
        {
            item.GetComponent<Icon>().Deleteicon();
        }
        icon.Clear();
    }
}
