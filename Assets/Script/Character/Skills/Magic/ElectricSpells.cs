using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElectricSpells : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _damageEffect;
    [SerializeField]
    private float _staticElectricity;
    [SerializeField]
    private bool _lightningBolt;
    [SerializeField]
    private bool _electricalDischarge;


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
            Character.spellsDamage = _damage;
            BattleManager.physicsDamage = false;
            BattleManager.magicDamage = true;
            StartCoroutine(Spell());
            SpelsManager.staticElectricity = _staticElectricity + Mathf.Floor(Character.intellectBonusEffectSpell / 2);
            SpelsManager.damageEffect = _damageEffect;
            Character.spell = true;
        }
    }
    IEnumerator Spell()
    {
        yield return new WaitForSeconds(0.1f);
        if (Character.isActive)
        {
            SpelsManager.spelIsActive = true;
            _cooldownSpells._isActive = true;
            SpelsManager.electricSpell = true;
            if (_lightningBolt)
                SpelsManager.lightningBolt = true;
            else if (_electricalDischarge)
                SpelsManager.electricalDischarge = true;
        }
    }
}
