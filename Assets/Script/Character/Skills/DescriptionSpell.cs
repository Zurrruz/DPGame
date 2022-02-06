using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionSpell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _discription;
    [SerializeField]
    private string _discriptionEffect;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _damageEffect;

    public bool magicSpell;
    public bool physicsSpell;
    public bool baseMagic;
    public bool basePhys;

    [SerializeField]
    private float _startTimer;
    private float _timer;
    private bool _isActive;

    public delegate void DescriptionBlockActiveTrue(string name, string description, float damage, string discriptionEffect, float damageEffect);
    public static event DescriptionBlockActiveTrue descriptionBlockActiveTrue;
    public delegate void DescriptionBlockActiveFalse();
    public static event DescriptionBlockActiveFalse descriptionBlockActiveFalse;

    void Update()
    {


        if (_isActive)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                if(magicSpell)
                    descriptionBlockActiveTrue(_name, _discription, _damage + Character.intellectBonusEffectSpell, _discriptionEffect, _damageEffect + Mathf.Floor(Character.intellectBonusEffectSpell / 2));
                else if(physicsSpell)
                    descriptionBlockActiveTrue(_name, _discription, _damage + Character.strenghtBonusEffect, _discriptionEffect, _damageEffect + Mathf.Floor(Character.intellectBonusEffectSpell / 2));
                else if(baseMagic)
                    descriptionBlockActiveTrue(_name, _discription, _damage + Character.intellectBonusEffectSpell + EquipmentManager.infoMagicDamageWeaponDescription, _discriptionEffect, 0);
                else if(basePhys)
                    descriptionBlockActiveTrue(_name, _discription, _damage + Character.strenghtBonusEffect + EquipmentManager.infoPhysicsDamageWeaponDescription, _discriptionEffect, 0);
                _isActive = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isActive = true;
        _timer = _startTimer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isActive = false;
        descriptionBlockActiveFalse();
    }
}
