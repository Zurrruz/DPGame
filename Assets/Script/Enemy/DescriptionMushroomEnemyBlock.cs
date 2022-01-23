using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionMushroomEnemyBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool _warior;
    [SerializeField]
    private bool _mage;
    [SerializeField]
    private bool _shaman;

    [SerializeField]
    private float _startTimer;
    private float _timer;
    private bool _isActive;

    public delegate void DescriptionMushroomEnemyBlockActiveTrue(bool warior, bool mage, bool shaman);
    public static event DescriptionMushroomEnemyBlockActiveTrue descriptionBlockTrue;

    public delegate void DescriptionMushroomEnemyBlockActiveFalse();
    public static event DescriptionMushroomEnemyBlockActiveFalse descriptionBlockFalse;

    private void Update()
    {
        if (_isActive)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                descriptionBlockTrue(_warior, _mage, _shaman);
                _isActive = false;
            }            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _timer = _startTimer;
        _isActive = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isActive = false;
        descriptionBlockFalse();
    }
}
