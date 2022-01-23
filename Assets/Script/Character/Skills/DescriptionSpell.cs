using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionSpell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _discription;

    [SerializeField]
    private float _startTimer;
    private float _timer;
    private bool _isActive;

    public delegate void DescriptionBlockActiveTrue(string name, string description);
    public static event DescriptionBlockActiveTrue descriptionBlockActiveTrue;
    public delegate void DescriptionBlockActiveFalse();
    public static event DescriptionBlockActiveFalse descriptionBlockActiveFalse;

    void Update()
    {
        if (_isActive)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                descriptionBlockActiveTrue(_name, _discription);
                _isActive = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isActive = true;
        _timer = _startTimer;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isActive = false;
        descriptionBlockActiveFalse();
    }
}
