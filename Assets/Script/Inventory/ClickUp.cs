using UnityEngine;
using UnityEngine.EventSystems;

public class ClickUp : MonoBehaviour, IPointerClickHandler
{
    public EquipmentData eq;
    [SerializeField]
    private GameObject _item;

    public delegate void SomeAction(bool glovers, bool boots, bool helm, bool chest, float strenght, float agility, float intellect, GameObject item);

    public static event SomeAction clicUpItem;
  
    public void OnPointerClick(PointerEventData eventData)
    {
        clicUpItem(eq.glovers, eq.boots, eq.helm, eq.chest, eq.strength, eq.agility, eq.intellect, _item);
        Destroy(gameObject, 0.2f);
    }
}
