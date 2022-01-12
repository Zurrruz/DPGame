using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IceSpells : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _frostbite;

    [SerializeField]
    private bool _frostStorm;

    CooldownSpells _cooldownSpells;

    private void Start()
    {
        _cooldownSpells = GetComponent<CooldownSpells>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cooldownSpells.CooldownTimer() <= 0)
        {
            SpelsManager.EffectActiveFalse();
            if(_frostStorm)
            {
                Character.spellsDamage -= Character.magicEffectDamage;
                SpelsManager.damageEffect = Character.intellectSpellDamage;
            }
            else
                Character.spellsDamage = _damage;

            BattleManager.physicsDamage = false;
            BattleManager.magicDamage = true;
            SpelsManager.frostbite = _frostbite;
            StartCoroutine(Spell());
        }

    }

    IEnumerator Spell()
    {
        yield return new WaitForSeconds(0.1f);
        SpelsManager.spelIsActive = true;
        _cooldownSpells._isActive = true;
        if (_frostStorm)
            SpelsManager.frostStorm = true;
        else
        {
            SpelsManager.frostbiteIsActive = true;
        }
       
    }
}
