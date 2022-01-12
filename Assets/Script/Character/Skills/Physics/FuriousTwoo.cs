using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FuriousTwoo : MonoBehaviour, IPointerClickHandler
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
            Character.spellsDamage = _damage + SpelsManager.combo;
            StartCoroutine(Spels());
            BattleManager.physicsDamage = true;
            BattleManager.magicDamage = false;            
        }
    }


    IEnumerator Spels()
    {
        yield return new WaitForSeconds(0.1f);
        if (Character.isActive)
        {
            SpelsManager.spelIsActive = true;
            SpelsManager.furiousTwo = true;
        }
    }
}
