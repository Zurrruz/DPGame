using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FuriousTwoo : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private float _damage;
    Character character;

    CooldownSpells _cooldownSpells;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        _cooldownSpells = GetComponent<CooldownSpells>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cooldownSpells.CooldownTimer() <= 0)
        {
            SpelsManager.EffectActiveFalse();
            Character.spellsDamage = character.strength + SpelsManager.combo;
            BattleManager.physicsDamage = true;
            BattleManager.magicDamage = false;
            StartCoroutine(Spels());

        }
    }


    IEnumerator Spels()
    {
        yield return new WaitForSeconds(0.1f);
        if (Character.isActive)
        {            
            SpelsManager.spelIsActive = true;
            SpelsManager.furiousTwo = true;
            _cooldownSpells._isActive = true;
        }
    }
}
