using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Gameplay/New weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _weaponName;    
    [Header("Stats")]
    [SerializeField]
    private float _strength;
    [SerializeField]
    private float _agility;
    [SerializeField]
    private float _intellect;

    [Header("От чего получает бонус")]
    [SerializeField]
    private bool _strengthBonus;
    [SerializeField]
    private bool _agilityBonus;
    [SerializeField]
    private bool _intellectBonus;

    [Header("Тип урона")]
    [SerializeField]
    private float _physicsDamage;
    [SerializeField]
    private float _magicDamage;


    public float strength => this._strength;
    public float agility => this._agility;
    public float intellect => this._intellect;

    public bool strengthBonus => this._strengthBonus;
    public bool agilityBonus => this._agilityBonus;
    public bool intellectBonus => this._intellectBonus;

    public float physicsDamage => this._physicsDamage;
    public float magicDamage => this._magicDamage;

    public string weaponName => this._weaponName;
}
