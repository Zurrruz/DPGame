using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Gameplay/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private string _nameEnemy;
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _speedMove;
    [SerializeField]
    private float _attackDamage;
    [SerializeField]
    private float _armor;
    [SerializeField]
    private float _magicArmor;
    [SerializeField]
    private float _resistFire;
    [SerializeField]
    private float _resistLightning;
    [SerializeField]
    private float _resistCold;

    public string nameEnemy => this._nameEnemy;
    public float health => this._health;
    public float speedMove => this._speedMove;
    public float attackDamage => this._attackDamage;
    public float armor => this._armor;
    public float magicArmor => this._magicArmor;
    public float resistFire => this._resistFire;
    public float resistLighting => this._resistLightning;
    public float resistCold => this._resistCold;
}
