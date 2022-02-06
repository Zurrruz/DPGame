using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FuriousBlow : MonoBehaviour, IPointerClickHandler
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
            BattleManager.physicsDamage = true;
            BattleManager.magicDamage = false;
            StartCoroutine(Spels());
            Character.spell = true;
        }
    }

    IEnumerator Spels()
    {
        yield return new WaitForSeconds(0.1f);
        if (Character.isActive)
        {
            SpelsManager.spelIsActive = true;
            _cooldownSpells._isActive = true;
            SpelsManager.furiousBlow = true;
        }
    }

}
