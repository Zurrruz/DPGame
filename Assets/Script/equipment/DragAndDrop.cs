using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _dragging;
    private Transform r;
    private EquipmentManager em;

    private void Start()
    {
        em = GameObject.Find("EquipmentManager").GetComponent<EquipmentManager>();
    }

    private void Update()
    {
        if (_dragging)
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
        transform.position = em.slotChest.transform.position;
    }

}
