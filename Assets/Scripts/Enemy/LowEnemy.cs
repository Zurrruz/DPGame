using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowEnemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData enemyData;

    public string _nameEnemy;
    public float _health;
    public float _speedMove;
    public float _attackDamage;
    public float _armor;
    public float _magicArmor;
    public float _resistFire;
    public float _resistLightning;
    public float _resistCold;

    private void Awake()
    {
        _nameEnemy = enemyData.nameEnemy;
        _health = enemyData.health;
        _speedMove = enemyData.speedMove;
        _attackDamage = enemyData.attackDamage;
        _armor = enemyData.armor;
        _magicArmor = enemyData.magicArmor;
        _resistFire = enemyData.resistFire;
        _resistLightning = enemyData.resistLighting;
        _resistCold = enemyData.resistCold;
    }

    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }


}
