using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AimedStrike : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _weakening;

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
            SpelsManager.weakeningPhysicsDamage = _weakening + Mathf.Floor( Character.intellectBonusEffectSpell / 2);
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
            SpelsManager.spelIsActive = true;
            SpelsManager.aimedStrike = true;
            _cooldownSpells._isActive = true;
        }
    }
}
