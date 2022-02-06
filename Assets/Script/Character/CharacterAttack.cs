using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAttack : MonoBehaviour, IPointerClickHandler
{
    public BattleManager battleManager;

    [SerializeField]
    private bool _physicsAttack;
    [SerializeField]
    private bool _magicAttack;

    private ChoiceSpell _cs;

    private void Start()
    {
        Character.cooldownTimer += Active;
        _cs = GetComponent<ChoiceSpell>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Character.isActive)
        {
            if (_physicsAttack)
            {
                BattleManager.physicsDamage = true;
                BattleManager.magicDamage = false;
                Character.spellsDamage = 0;
            }
            else if (_magicAttack)
            {
                BattleManager.physicsDamage = false;
                BattleManager.magicDamage = true;
                Character.spellsDamage = 0;
            }
            SpelsManager.EffectActiveFalse();
            SpelsManager.spelIsActive = true;
            Character.spell = false;
        }
    }

    private void Active()
    {
        _cs.Active();
    }

    private void OnDestroy()
    {
        Character.cooldownTimer -= Active;
    }
}
