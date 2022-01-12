using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Puton po;

    void Start()
    {
        po = GameObject.Find("EquipmentManager").GetComponent<Puton>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        po._tooltiprOnPointer = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        po._tooltiprOnPointer = false;
    }
}
