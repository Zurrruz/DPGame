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
            SpelsManager.scorch = _scorch;
            StartCoroutine(Spell());

            
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
        }
    }
    
}
