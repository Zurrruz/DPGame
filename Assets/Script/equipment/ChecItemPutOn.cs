using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecItemPutOn : MonoBehaviour
{
    public LayerMask _item;

    private void Start()
    {     
    }

    void Update()
    {
        var Hit2D = Physics2D.Raycast(transform.position, new Vector3(0, 0, -15), 30, _item);
        if (Hit2D.transform != null)
        {
            Hit2D.transform.GetComponent<EquipmentItems>().putOn = true;
        }
    }
}
