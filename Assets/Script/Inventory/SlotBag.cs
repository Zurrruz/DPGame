using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBag : MonoBehaviour
{
    private EquipmentManager eq;
    public int i;

    void Start()
    {
        eq = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 0)
        {
            eq.isFull[i] = false;
           
        }
        if (transform.childCount > 0)
        {
            if (transform.GetChild(0).CompareTag("Item"))
            {
                transform.GetChild(0).GetComponent<EquipmentItems>().slotInBag = i;
                transform.GetChild(0).GetComponent<EquipmentItems>().putOn = false;
                if (transform.GetChild(0).GetComponent<EquipmentItems>().helm == true)
                {
                    transform.GetChild(0).GetComponent<EquipmentItems>().koi = 1;
                }
                else if (transform.GetChild(0).GetComponent<EquipmentItems>().chest == true)
                {
                    transform.GetChild(0).GetComponent<EquipmentItems>().koi = 2;
                }
                else if (transform.GetChild(0).GetComponent<EquipmentItems>().glovers == true)
                {
                    transform.GetChild(0).GetComponent<EquipmentItems>().koi = 3;
                }
                else if (transform.GetChild(0).GetComponent<EquipmentItems>().boots == true)
                {
                    transform.GetChild(0).GetComponent<EquipmentItems>().koi = 4;
                }
            }
            else if(transform.GetChild(0).CompareTag("Weapon"))
            {
                transform.GetChild(0).GetComponent<EquipmentWeapon>().slotInBag = i;
                transform.GetChild(0).GetComponent<EquipmentWeapon>().putOnWeapon = false;
            }
        }
    }
}
