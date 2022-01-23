using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClicUpSpells : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject _spell;

    private bool _onPointer;
    private bool _onEnter;

    public delegate void UpSpell(GameObject spell);
    public static event UpSpell upSpell;

    private void Start()
    {
        ChooseNotChoose.choose += NotChoose;
        OpenBox.take += AddSpell;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Ñhoose();
        _onPointer = true;
        ChooseNotChoose.NotChoose();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _onEnter = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _onEnter = false;
    }

    private void Ñhoose()
    {
        transform.GetChild(0).GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }
    private void NotChoose()
    {
        if (!_onEnter)
        {
            _onPointer = false;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void AddSpell()
    {
        if(_onPointer)
            upSpell(_spell);
        DestroySpell();
    }

    private void DestroySpell()
    {
        //transform.GetChild(1).GetComponent<ChoiceSpell>().DestroySpell();
        ChooseNotChoose.choose -= NotChoose;
        OpenBox.take -= AddSpell;
        Destroy(gameObject, 0.2f);
    }
}
