using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSpels : MonoBehaviour
{
    private Enemy enemy;
    private float _shamanDamage;

    [SerializeField, Tooltip("Включить баф на усиление урона")]
    private bool _increasedDamage;
    [SerializeField, Tooltip("Количество урона прибавляемого за баф")]
    private float addDamage;
    private float _addDamage;
    private bool _iDamage;
    [SerializeField, Tooltip("Откат бафа на урон")]
    private int kdAddDamage;
    private int _kdAddDamage;

    [Header("Пассивная способность усиление")]
    [SerializeField]
    private bool _pasiveIncreasedDamage;
    private bool _pasiveIncreasedDamageActive;
    private float _heals;
    [SerializeField, Tooltip("Прибавка урона при срабатывании пассивной способности")]
    private float _addPassiveDamage;

    private Animator _anim;
    [SerializeField]
    private float _animBuffTimer;
    [SerializeField]
    private float _animAttacTimer;
    [SerializeField]
    private ParticleSystem _buffAddDamage;


    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        _kdAddDamage = 0;
        _addDamage = addDamage;
        _heals = enemy._heals;
    }

    public void ActivateSpels()
    {
        _kdAddDamage--;
        if(_increasedDamage)        
            IncreasedDamage();
        
        if (_pasiveIncreasedDamage)
            PasiveIncreasedDamage();
        
        ShamanBufsDamage(_shamanDamage, true);
    }

    public void ParticlStop()
    {
        _buffAddDamage.Stop();
    }
    public float AnimAttackTimer()
    {
        return _animAttacTimer;
    }

    private void IncreasedDamage()
    {
        if (_kdAddDamage <= 0)
        {
            enemy.AnimBuffTimer(_animBuffTimer);
            _anim.SetBool("Buff", true);
            _buffAddDamage.Play(true);
            enemy._pDamage += _addDamage;
            _kdAddDamage = kdAddDamage;
            _iDamage = true;
        }
        else if (_iDamage)
        {
            enemy.AnimBuffTimer(0f);
            if (_addDamage > 1)
            {
                enemy._pDamage -= 2;
                _addDamage -= 2;
                if (_addDamage == 0)
                {
                    _addDamage = addDamage;
                    _iDamage = false;
                }
            }
            else if (_addDamage == 1)
            {
                enemy._pDamage -= 1;
                _addDamage -= 1;
                if (_addDamage == 0)
                {
                    _addDamage = addDamage;
                    _iDamage = false;
                }
            }
        }
    }

    private void PasiveIncreasedDamage()
    {
        if (!_pasiveIncreasedDamageActive)
        {
            float h = _heals / 3;
            if (enemy._heals <= h)
            {
                _sr.color = new Color(1, 0.6f, 0.6f, 1);
                enemy._pDamage += _addPassiveDamage;
                _pasiveIncreasedDamageActive = true;
            }
        }
    }

    public void ShamanBufsDamage(float damage, bool active)
    {
        if (!active)
        {
            enemy._pDamage += damage;
            _shamanDamage = damage;
        }
        else if (damage > 0)
        {
            enemy._pDamage -= 2;
            _shamanDamage -= 2;
        }
    }

    public void OnOffParticleBuff()
    {
        StartCoroutine(OnOff());
    }
    IEnumerator OnOff()
    {
        _buffAddDamage.Play();
        yield return new WaitForSeconds(_animBuffTimer);
        _buffAddDamage.Stop();
    }
}
