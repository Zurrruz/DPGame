using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Gameplay/New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _nameWeapon;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _radiusAttack;
    [SerializeField]
    private float _distanceAttack;
    [SerializeField]
    private float _speedAttack;
    [SerializeField]
    private float _fireDamage;
    [SerializeField]
    private float _coldDamage;
    [SerializeField]
    private float _lightingDamage;

    public string nameWeapon => this._nameWeapon;
    public float damage => this._damage;
    public float radiusAttack => this._radiusAttack;
    public float distanceAttack => this._distanceAttack;
    public float speedAttack => this._speedAttack;
    public float fireDamage => this._fireDamage;
    public float coldDamage => this._coldDamage;
    public float lightingDamage => this._lightingDamage;
}
