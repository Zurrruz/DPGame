using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueAttack : MonoBehaviour
{
    [SerializeField]
    public bool _character;
    [SerializeField]
    public bool _enemy;

    public GameObject icon;

    private Character character;
    private Enemy enemy;

    public float _stepInitiative;
    public float _initiative;

    public float _actualStepInitiative;

    private void Awake()
    {
        if (_character)
            character = GetComponent<Character>();
        if (_enemy)
            enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        ToRenewStepInitiative();
        QueueManager.addStepInitiative += AddInitiayive;
    }

    public void ToRenewStepInitiative()
    {
        if (_character)
        {
            _initiative = character._initiative;
            _stepInitiative = _actualStepInitiative;
        }
        if (_enemy)
        {
            _initiative = enemy.initiative;
            _stepInitiative = _actualStepInitiative;
        }
    }

    public void AddActualInitiative()
    {
        _actualStepInitiative += _initiative;
    }

    private void AddInitiayive()
    {
        _stepInitiative += _initiative;
    }

    public void RemoveQueueStep(float  queueStep)
    {
        _stepInitiative -= queueStep;
    }

    public float Initiative()
    {
        return _initiative;
    }

    public void ResetInitiative()
    {
        _actualStepInitiative = 0;
    }
}
