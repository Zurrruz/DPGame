using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentWeapon : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public WeaponData weapondata;

    public string _name;

    public int slotInBag;
    private static int _slotbag;    

    [Header("Stats")]
    public float strength;
    public float agility;
    public float intellect;

    public float physicsDamage;
    public float magicDamage;

    public bool putOnWeapon;
    private bool _onPointerClick;

    private Puton putOn;
    private EquipmentManager _em;

    private void Awake()
    {
        putOn = GameObject.Find("EquipmentManager").GetComponent<Puton>();
        Puton.putOnWeapon += PutOnWeapon;
        _em = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>();
        Puton.deleteItem += DeleteItemFromBag;
    }

    void Start()
    {
        _name = weapondata.name;

        strength = weapondata.strength;
        agility = weapondata.agility;
        intellect = weapondata.intellect;
        physicsDamage = weapondata.physicsDamage;
        magicDamage = weapondata.magicDamage;
    }

    void Update()
    {
        if (!putOn._ItemOnPointer && !putOn._tooltiprOnPointer)
        {
            _onPointerClick = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onPointerClick = true;
        putOn.InfoWeapon(name, strength, agility, intellect, physicsDamage, magicDamage);
        putOn.itemWeapon = 2;
        _slotbag = slotInBag;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        putOn._ItemOnPointer = false;        
    }

    public void PutOnWeapon()
    {
        StartCoroutine(PutOnOfWeapon());
    }

    private IEnumerator PutOnOfWeapon()
    {
        if(putOnWeapon)
        {
            _onPointerClick = false;
            transform.position = _em.slots[_slotbag].transform.position;
            transform.SetParent(_em.slots[_slotbag].transform);
            putOnWeapon = false;
        }
        yield return new WaitForSeconds(0.1f);

        if(!putOnWeapon && _onPointerClick)
        {
            transform.position = _em.slotWeapon.transform.position;
            transform.SetParent(_em.slotWeapon.transform);
            _em.AddStatsFromWeapon(strength, agility, intellect, physicsDamage, magicDamage);
            _onPointerClick = false;
        }
    }

    private void DeleteItemFromBag()
    {
        if (_onPointerClick)
        {
            Puton.putOnItem -= PutOnWeapon;
            Puton.deleteItem -= DeleteItemFromBag;
            Destroy(gameObject, 0.1f);
        }
    }

}
