using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceSpell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void ChoiceSpellColor();
    public static event ChoiceSpellColor choiceSpellColor;

    public bool _isActive;

    private CooldownSpells _cd;
    private SpriteRenderer _sr;

    private void Start()
    {
        _cd = GetComponent<CooldownSpells>();
        _sr = GetComponent<SpriteRenderer>();
        choiceSpellColor += RemoveColor;
    }
    private void OnDestroy()
    {
        choiceSpellColor -= RemoveColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cd.CooldownTimer() <= 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
            choiceSpellColor();
        }
    }

    private void RemoveColor()
    {
        if (_cd.CooldownTimer() <= 0 && !_isActive)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
    public void Active()
    {
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isActive = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isActive = false;
    }

    public void DestroySpell()
    {
        choiceSpellColor -= RemoveColor;
    }
}
