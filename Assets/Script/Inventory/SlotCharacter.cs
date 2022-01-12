using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCharacter : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount > 1)
        {
            if (transform.GetChild(1).CompareTag("Item"))
            {
                transform.GetChild(1).GetComponent<EquipmentItems>().putOn = true;
            }
            else if(transform.GetChild(1).CompareTag("Weapon"))
            {
                transform.GetChild(1).GetComponent<EquipmentWeapon>().putOnWeapon = true;
            }
        }
    }
}
