using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSword : MonoBehaviour
{
    [SerializeField]
    private WeaponData _weaponData;

    [SerializeField]
    private GameObject _waveAttack;
    [SerializeField]
    private GameObject _fireWaveAttack;

    private float  _damage;
    private float _speedAttack;
    private float _timerAttack;
    private float _fireDamage;

    private bool _wave;
    private bool _fireAttack;
    private bool _dopAttack30;
    private bool _dopAttack60;

    private void Awake()
    {
        _damage = _weaponData.damage;
        _fireDamage = _weaponData.fireDamage;
    }

    void Start()
    {
        
        _speedAttack = 0f;
        _wave = false;
        _fireAttack = false;
        _dopAttack30 = false;
    }
    
    void Update()
    {
        Stats();
        Attack();
        SpeedAttack();
    }

    private void Stats()
    {
        if (Character.strength <= 9)
        {
            _damage = _weaponData.damage;

            if (Character.strength >= 10)
            {
                _damage += 5;

                if (Character.strength >= 30)
                {                 
                    _wave = true;
                }
                else
                {
                    _wave = false;
                }
            }
        }                    

        if (Character.agility <= 9)
        {
            _speedAttack = _weaponData.speedAttack;

            if (Character.agility >= 10)
            {
                _speedAttack += 0.05f;

                if (Character.agility >= 30)
                {
                    _dopAttack30 = true;
                }
                else
                {
                    _dopAttack30 = false;
                }
            }                
        }      
        
        if(Character.intellect >= 10)
        {
            _fireAttack = true;
        }
        else
        {
            _fireAttack = false;
        }
    }

    private void SpeedAttack()
    {
        _timerAttack += Time.deltaTime;
    }


    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _timerAttack >= 1.0f)
        {
            _timerAttack = _speedAttack;
            //атака по врагу
            if (_dopAttack30)
            {
                int ran = Random.Range(0, 101);
                if (ran > 30)
                {
                    // вторая атака
                }
            }

            if(_wave)
            {
                if(_fireAttack)
                {
                    //запускает огненую волну
                }
                else
                {
                    //запускает волну
                }
            }            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            if (_fireAttack)
            {
                collision.collider.GetComponent<FireRaising>().Arson(_fireDamage);
            }
            
        }
    }
}
