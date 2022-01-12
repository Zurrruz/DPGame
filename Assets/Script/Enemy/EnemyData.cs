using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Gameplay/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string _nameEnemy;
    [Header("Stats")]
    [SerializeField]
    private float _strength;
    [SerializeField]
    private float _agility;
    [SerializeField]
    private float _intellect;
}
