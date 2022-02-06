using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireSpells : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _scorch;
    [SerializeField]
    private bool _fireBall;
    [SerializeField]
    private bool _fireJet;

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
            SpelsManager.scorch = _scorch + Mathf.Floor(Character.intellectBonusEffectSpell / 2);
            StartCoroutine(Spell());

            Character.spell = true;
        }
    }

    IEnumerator Spell()
    {
        yield return new WaitForSeconds(0.1f); 
        if (Character.isActive)
        {
            SpelsManager.spelIsActive = true;
            SpelsManager.scorchIsActive = true;
            _cooldownSpells._isActive = true;
            if (_fireBall)
                SpelsManager.fireBall = true;
            else if (_fireJet)
                SpelsManager.fireJet = true;
        }
    }
    
}
