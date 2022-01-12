using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Debuffs : MonoBehaviour, IPointerClickHandler
{
    private Enemy _enemy;
    private float _weakening;

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

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Combo();
        if (SpelsManager.aimedStrike)
        {
            WeakeningPhysicsDamage(SpelsManager.weakeningPhysicsDamage);
            _weakening += SpelsManager.weakeningPhysicsDamage;
        }

        if(SpelsManager.scorchIsActive)
        {
            _scorch += SpelsManager.scorch;
        }

        if(SpelsManager.frostbiteIsActive)
        {
            Frostbite();
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
        if (_enemy._pDamage <= weakening)
        {
            _enemy._pDamage = 0;
        }
        else
        {
            _enemy._pDamage -= weakening;
        }
    }

    private void Weakening()
    {
        _weakening = Mathf.Floor(_weakening / 2);
        _enemy._pDamage += _weakening;
    }

    private void Scorch()
    {
        _enemy.TakingDebufsDamage(_scorch);
        if (_scorch >= 2)
            _scorch -= 2;
        else
            _scorch = 0;
    }
    
    public void Frostbite()
    {
        _frostbite += SpelsManager.frostbite;
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
            _enemy._frostbite = true;
            _frostbite = 0;
            _frostbiteAuditMin = false;
            _frostbiteAuditMax = false;
            _enemy.UnFrostbite();
        }
    }

    public void Electricity()
    {
        _staticElectricity += SpelsManager.staticElectricity;
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
