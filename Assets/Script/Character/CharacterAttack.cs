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


    public void OnPointerClick(PointerEventData eventData)
    {
        if (Character.isActive)
        {
            if (_physicsAttack)
            {
                BattleManager.physicsDamage = true;
                BattleManager.magicDamage = false;
            }
            else if (_magicAttack)
            {
                BattleManager.physicsDamage = false;
                BattleManager.magicDamage = true;
            }
            SpelsManager.EffectActiveFalse();
            SpelsManager.spelIsActive = true;
        }
    }

}
