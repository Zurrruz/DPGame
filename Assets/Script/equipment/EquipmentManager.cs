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

    public bool slotGloversIsFull;
    public bool slotBootsIsFull;
    public bool slotHelmIsFull;
    public bool slotChestIsFull;
    
    [SerializeField]
    private LayerMask _putOnItem;
    [SerializeField]
    private Transform _rayCast;

    private float _infoAgilityItem;
    private float _infoStrenghtItem;
    private float _infoIntellectItem;

    private Character _character;

    void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        ClickUp.clicUpItem += AddItem;
    }

    private void Update()
    {        
        //RaycastHit2D rayCast = Physics2D.Raycast(_rayCast.position, new Vector2(0, -1));
        //if (rayCast.transform != null)
        //    Debug.Log("Selected object: " + rayCast.transform.name);
        //_infoAgilityItem = rayCast.transform.GetComponent<EquipmentData>().agility;
        //_infoStrenghtItem = rayCast.transform.GetComponent<EquipmentData>().strength;
        //_infoIntellectItem = rayCast.transform.GetComponent<EquipmentData>().intellect;
    }

    public void AddItem(bool glovers, bool boots, bool helm, bool chest, float st, float ag, float intel, GameObject item)
    {
        if (glovers)
        {
            if (!slotGloversIsFull)
            {
                Instantiate(item, slotGlovers.transform);
                _character.AddStats(st, ag, intel);
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
                _character.AddStats(st, ag, intel);
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
                _character.AddStats(st, ag, intel);
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
                _character.AddStats(st, ag, intel);
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
}
    
    

