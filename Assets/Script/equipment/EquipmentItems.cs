using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentItems : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{    
    public string _name;

    public int slotInBag;
    private static int _slotbag;
    public int koi;
    private static int kindOfItem;

    [Header ("Stats")]
    public float strength;
    public float agility;
    public float intellect;

    [Header ("Вид доспеха")]
    public bool chest;
    public bool glovers;
    public bool boots;
    public bool helm;

    private Puton po;
    private EquipmentManager _em;
    private bool _onPointerClick;

    public bool putOn;    

    private void Awake()
    {
        po = GameObject.Find("EquipmentManager").GetComponent<Puton>();
        _em = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>();
        Puton.putOnItem += PutOnItem;
        Puton.deleteItem += DeleteItemFromBag;
    }

    private void OnDestroy()
    {
        Puton.putOnItem -= PutOnItem;
        Puton.deleteItem -= DeleteItemFromBag;
    }

    void Start()
    {
        putOn = false;
        _onPointerClick = false;
    }

    private void Update()
    {
        if(!po._ItemOnPointer && !po._tooltiprOnPointer)
        {
            _onPointerClick = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        po.itemWeapon = 1;
        po.InfoItem(_name, strength, agility, intellect, putOn);
        _onPointerClick = true;
        _slotbag = slotInBag;
        kindOfItem = koi;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        po._ItemOnPointer = false;
        
    }

    private void PutOnItem()
    {
        StartCoroutine(PutOnOfItem());
    }

    private IEnumerator PutOnOfItem()
    {
        if (putOn)
        {
            _onPointerClick = false;
            switch (kindOfItem)
            {
                case 1:
                    if(helm)
                    {
                        transform.position = _em.slots[_slotbag].transform.position;
                        transform.SetParent(_em.slots[_slotbag].transform);
                        putOn = false;
                    }
                    break;
                case 2:
                    if (chest)
                    {
                        transform.position = _em.slots[_slotbag].transform.position;
                        transform.SetParent(_em.slots[_slotbag].transform);
                        putOn = false;
                    }
                    break;
                case 3:
                    if (glovers)
                    {
                        transform.position = _em.slots[_slotbag].transform.position;
                        transform.SetParent(_em.slots[_slotbag].transform);
                        putOn = false;
                    }
                    break;
                case 4:
                    if (boots)
                    {
                        transform.position = _em.slots[_slotbag].transform.position;
                        transform.SetParent(_em.slots[_slotbag].transform);
                        putOn = false;
                    }
                    break;
            }           
        }

        yield return new WaitForSeconds(0.1f);

        if (!putOn)
        {
            if (_onPointerClick)
            {
                if (glovers)
                {
                    transform.position = _em.slotGlovers.transform.position;
                    transform.SetParent(_em.slotGlovers.transform);
                    _em.AddStatsFromGlovers(strength, agility, intellect);
                }
                else if (boots)
                {
                    transform.position = _em.slotBoots.transform.position;
                    transform.SetParent(_em.slotBoots.transform);
                    _em.AddStatsFromBoots(strength, agility, intellect);
                }
                else if (helm)
                {
                    transform.position = _em.slotHelm.transform.position;
                    transform.SetParent(_em.slotHelm.transform);
                    _em.AddStatsFromHelm(strength, agility, intellect);
                }
                else if (chest)
                {
                    transform.position = _em.slotChest.transform.position;
                    transform.SetParent(_em.slotChest.transform);
                    _em.AddStatsFromChest(strength, agility, intellect);
                }
                _onPointerClick = false;
            }
        }
    }

    private void DeleteItemFromBag()
    {
        if (_onPointerClick)
        {
            Puton.putOnItem -= PutOnItem;
            Puton.deleteItem -= DeleteItemFromBag;
            Destroy(gameObject, 0.1f);
        }       
    }
}
