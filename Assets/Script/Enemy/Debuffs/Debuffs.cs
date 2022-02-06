using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Debuffs : MonoBehaviour, IPointerClickHandler
{
    private Enemy _enemy;
    private float _weakening;
    private float _weakeningValue;

    private float _scorch;

    private float _frostbite;
    [SerializeField]
    private float _frostbiteMax;
    [SerializeField]
    private float _frostbiteMin;
    private bool _frostbiteAuditMin;
    private bool _frostbiteAuditMax;

    private float _staticElectricity;
    // public bool electrified;
    [Header("Анимация дебафов")]
    [SerializeField]
    GameObject _scorchAnimation;
    [SerializeField]
    GameObject _staticElectricityAnimation;
    public bool _staticAnimTrue;
    [SerializeField]
    GameObject _frostbiteAnimationMage;
    [SerializeField]
    GameObject _frostbiteAnimationWarior;
    public bool frostbiteAnimTrue;
    [SerializeField]
    GameObject _iceChainsWarior;
    [SerializeField]
    GameObject _iceChainsMage;


    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_enemy._isDead)
        {
            Combo();
            if (SpelsManager.aimedStrike)
            {
                WeakeningPhysicsDamage(SpelsManager.weakeningPhysicsDamage);
                _weakening += SpelsManager.weakeningPhysicsDamage;
                _weakeningValue = _weakening;
            }

            if (SpelsManager.scorchIsActive)
            {
                _scorch += SpelsManager.scorch;
                _enemy.AnimDebuffTimer(0.5f);
            }

            if (SpelsManager.frostbiteIsActive)
            {
                Frostbite();
            }
        }
    }

    public void DebufEffect()
    {
        if (_staticElectricity > 0)
            _staticElectricity--;

        Weakening();
        Scorch();
    }

    private void WeakeningPhysicsDamage(float weakening)
    {
        if (_enemy._warior)
        {
            if (_enemy._pDamage < weakening)
            {                
                _weakening = _enemy._pDamage;
                _weakeningValue = _enemy._pDamage;
                _enemy._pDamage = 0;
            }

            else
                _enemy._pDamage -= weakening;
        }

        if (_enemy._mage)
        {
            if (_enemy._mDamage < weakening)
            {
                _weakening = _enemy._mDamage;
                _weakeningValue = _enemy._mDamage;
                _enemy._mDamage = 0;
            }
            else
                _enemy._mDamage -= weakening;
        }
    }

    private void Weakening()
    {
        if (_enemy._warior)
        {
            if (_weakeningValue > 2)
            {
                _weakening = Mathf.Ceil(_weakening / 2);
                _enemy._pDamage += _weakening;
                _weakeningValue -= _weakening;
            }
            else if (_weakeningValue == 2)
            {
                _enemy._pDamage += _weakening;
                _weakeningValue -= _weakening;
                _weakening = 0;
            }
            else if (_weakeningValue == 1)
            {
                _enemy._pDamage++;
                _weakeningValue = 0;
                _weakening = 0;
            }
        }

        else if (_enemy._mage)
        {
            if (_weakeningValue > 2)
            {
                _weakening = Mathf.Ceil(_weakening / 2);
                _enemy._mDamage += _weakening;
                _weakeningValue -= _weakening;
            }
            else if (_weakeningValue == 2)
            {
                _enemy._mDamage += _weakening;
                _weakeningValue -= _weakening;
                _weakening = 0;
            }
            else if (_weakeningValue == 1)
            {
                _enemy._mDamage++;
                _weakeningValue = 0;
                _weakening = 0;
            }
        }
    }
    public float WeakeningDebuf()
    {
        return _weakeningValue;
    }

    private void Scorch()
    {
        _enemy.TakingDebufsDamage(_scorch);
        if (_scorch > 0)
            Instantiate(_scorchAnimation, transform);

        if (_scorch >= 2)
            _scorch -= 2;
        else
        {
            _scorch = 0;
            _enemy.AnimDebuffTimer(0);
        }        
    }
    public float ScorchDebuf()
    {
        return _scorch;
    }
    
    public void Frostbite()
    {
        _frostbite += SpelsManager.frostbite;
        if(!frostbiteAnimTrue)
        {
            if(_enemy._mage)
            {
                Instantiate(_frostbiteAnimationMage, transform);
            }
            else
                Instantiate(_frostbiteAnimationWarior, transform);

            frostbiteAnimTrue = true;
        }

        if(_frostbite <= _frostbiteMin && !_frostbiteAuditMin)
        {
            _enemy.Frostbite(10f);
            _frostbiteAuditMin = true;
        }
        else if(_frostbite > _frostbiteMin && _frostbite < _frostbiteMax && !_frostbiteAuditMax)
        {
            _enemy.Frostbite(5f);
            _frostbiteAuditMax = true;
        }
        else if(_frostbite >= _frostbiteMax)
        {
            if (_enemy._mage && !_enemy._isDead)
            {
                Instantiate(_iceChainsMage, transform);
                _enemy.AnimIdleStop();
            }
            else if(!_enemy._isDead)
            {
                Instantiate(_iceChainsWarior, transform);
                _enemy.AnimIdleStop();
            }

            _enemy._frostbite = true;
            _frostbite = 0;
            _frostbiteAuditMin = false;
            _frostbiteAuditMax = false;
            _enemy.UnFrostbite();
        }
    }
    public float FrostbiteDebuf()
    {
        return _frostbite;
    }

    
    public void Electricity()
    {
        _staticElectricity += SpelsManager.staticElectricity;
        if (!_staticAnimTrue)
        {
            Instantiate(_staticElectricityAnimation, transform);
            _staticAnimTrue = true;
        }
    }
    public float StaticElectricity()
    {
        return _staticElectricity;
    }  

    private void Combo()
    {
        if (SpelsManager.furiousBlow)
            SpelsManager.combo += 3;
        else if (SpelsManager.furiousTwo)
            SpelsManager.combo += 3;
        else
            SpelsManager.combo = 0;
    }
}
