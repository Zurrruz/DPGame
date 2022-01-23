using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public GameObject[] slots;
    public bool[] isFull;

    public GameObject slotGlovers;
    public GameObject slotBoots;
    public GameObject slotHelm;
    public GameObject slotChest;
    public GameObject slotWeapon;

    public bool slotGloversIsFull;
    public bool slotBootsIsFull;
    public bool slotHelmIsFull;
    public bool slotChestIsFull;
    public bool slotWeaponIsFull;

    private float _infoAgilityChest;
    private float _infoStrenghtChest;
    private float _infoIntellectChest;

    private float _infoAgilityGlovers;
    private float _infoStrenghtGlovers;
    private float _infoIntellectGlovers;

    private float _infoAgilityBoots;
    private float _infoStrenghtBoots;
    private float _infoIntellectBoots;

    private float _infoAgilityHelm;
    private float _infoStrenghtHelm;
    private float _infoIntellectHelm;

    private float _infoAgilityWeapon;
    private float _infoStrengthWeapon;
    private float _infoIntellectWeapon;
    private float _infoPhysicsDamageWeapon;
    private float _infoMagicDamageWeapon;

    private Character _character;


    void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        ClickUp.clicUpItem += AddItem;
        ClickUp.clicUpWeapon += AddWeapon;
    }
    private void OnDestroy()
    {
        ClickUp.clicUpItem -= AddItem;
        ClickUp.clicUpWeapon -= AddWeapon;
    }

    private void Update()
    {
        
    }

    private void AddStatsCharacter()
    {
        _character.strength = _infoStrenghtChest + _infoStrenghtBoots + _infoStrenghtGlovers + _infoStrenghtHelm + _infoStrengthWeapon;
        _character.agility = _infoAgilityChest + _infoAgilityBoots + _infoAgilityGlovers + _infoAgilityHelm + _infoAgilityWeapon;
        _character.intellect = _infoIntellectChest + _infoIntellectBoots + _infoIntellectGlovers + _infoIntellectHelm + _infoIntellectWeapon;
        _character.physicsDamage = _character.basePDamage + _infoPhysicsDamageWeapon + _character.strength;
        _character.magicDamage = _character.baseMDamage + _infoMagicDamageWeapon + _character.intellect;        
        _character.Stats();
    }

    public void AddStatsFromChest(float st, float ag, float intel)
    {
        _infoAgilityChest = ag;
        _infoIntellectChest = intel;
        _infoStrenghtChest = st;
        AddStatsCharacter();
    }
    public void AddStatsFromGlovers(float st, float ag, float intel)
    {
        _infoAgilityGlovers = ag;
        _infoIntellectGlovers = intel;
        _infoStrenghtGlovers = st;
        AddStatsCharacter();
    }
    public void AddStatsFromBoots(float st, float ag, float intel)
    {
        _infoAgilityBoots = ag;
        _infoIntellectBoots = intel;
        _infoStrenghtBoots = st;
        AddStatsCharacter();
    }
    public void AddStatsFromHelm(float st, float ag, float intel)
    {
        _infoAgilityHelm = ag;
        _infoIntellectHelm = intel;
        _infoStrenghtHelm = st;
        AddStatsCharacter();
    }
    public void AddStatsFromWeapon(float st, float ag, float intel, float pDamage, float mDamage)
    {
        _infoAgilityWeapon = ag;
        _infoIntellectWeapon = intel;
        _infoStrengthWeapon = st;
        _infoPhysicsDamageWeapon = pDamage;
        _infoMagicDamageWeapon = mDamage;
        AddStatsCharacter();
    }

    public void AddItem(bool glovers, bool boots, bool helm, bool chest, float st, float ag, float intel, GameObject item)
    {
        if (glovers)
        {
            if (!slotGloversIsFull)
            {
                item.GetComponent<EquipmentItems>().strength = st;
                Instantiate(item, slotGlovers.transform);
                AddStatsFromGlovers(st, ag, intel);
                slotGloversIsFull = true;
            }
            else
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!isFull[i])
                    {
                        isFull[i] = true;
                        Instantiate(item, slots[i].transform);
                        break;
                    }
                }
            }
        }
        else if (boots)
        {
            if (!slotBootsIsFull)
            {
                Instantiate(item, slotBoots.transform);
                AddStatsFromBoots(st, ag, intel);
                slotBootsIsFull = true;
            }
            else
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!isFull[i])
                    {
                        isFull[i] = true;
                        Instantiate(item, slots[i].transform);
                        break;
                    }
                }
            }
        }
        else if (helm)
        {
            if (!slotHelmIsFull)
            {
                Instantiate(item, slotHelm.transform);
                AddStatsFromHelm(st, ag, intel);
                slotHelmIsFull = true;
            }
            else
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!isFull[i])
                    {
                        isFull[i] = true;
                        Instantiate(item, slots[i].transform);
                        break;
                    }
                }
            }
        }
        else if (chest)
        {
            if (!slotChestIsFull)
            {
                Instantiate(item, slotChest.transform);               
                AddStatsFromChest(st, ag, intel);                
                slotChestIsFull = true;
            }
            else
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (!isFull[i])
                    {
                        isFull[i] = true;
                        Instantiate(item, slots[i].transform);
                        break;
                    }
                }
            }
        }
    }

    public void AddWeapon(float physicsDamage, float magicDamage, float st, float ag, float intel, GameObject weapon)
    {
        if(!slotWeaponIsFull)
        {
            Instantiate(weapon, slotWeapon.transform);
            AddStatsFromWeapon(st, ag, intel, physicsDamage, magicDamage);
            slotWeaponIsFull = true;
        }

        else
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!isFull[i])
                {
                    isFull[i] = true;
                    Instantiate(weapon, slots[i].transform);
                    break;
                }
            }
        }
    }
   
   
}
    
    

