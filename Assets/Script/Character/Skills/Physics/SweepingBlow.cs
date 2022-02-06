using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SweepingBlow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _damage;

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
            StartCoroutine(Spels());
            BattleManager.physicsDamage = true;
            BattleManager.magicDamage = false;
            Character.spell = true;
        }
    }

    IEnumerator Spels()
    {
        yield return new WaitForSeconds(0.1f);
        if (Character.isActive)
        {
            SpelsManager.damageEffect = _damage + Mathf.Floor(Character.intellectBonusEffectSpell / 2);
            SpelsManager.spelIsActive = true;
            SpelsManager.sweepingBlow = true;
            _cooldownSpells._isActive = true;
        }
    }
}
