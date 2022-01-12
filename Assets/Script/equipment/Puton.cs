using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puton : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuPutOn;

    [SerializeField]
    private Text _nameItem;
    [SerializeField]
    private Text _strengthItem;
    [SerializeField]
    private Text _agilityItem;
    [SerializeField]
    private Text _intellectItem;
    [SerializeField]
    private Text _physicsDamage;
    [SerializeField]
    private Text _magicDamage;

    public bool _tooltiprOnPointer;
    public bool _ItemOnPointer;

    public delegate void PutOn();
    public static event PutOn putOnItem;
    public static event PutOn putOnWeapon;
    public static event PutOn deleteItem;
    public int itemWeapon;

    void Start()
    {
        _tooltiprOnPointer = true;
        _ItemOnPointer = true;
        _menuPutOn.SetActive(false);        
    }

    private void Update()
    {
        if (!_tooltiprOnPointer && !_ItemOnPointer)
            _menuPutOn.SetActive(false);
    }

    public void InfoItem(string name, float st, float ag, float intel)
    {
        _ItemOnPointer = true;
        _menuPutOn.SetActive(true);
        _menuPutOn.transform.position = Input.mousePosition;
        _nameItem.text = name;
        _strengthItem.text = "Сила " + st;
        _agilityItem.text = "Ловкость " + ag;
        _intellectItem.text = "Интеллект " + intel;
    }

    public void InfoWeapon(string name, float st, float ag, float intel, float pD, float mD)
    {
        _ItemOnPointer = true;
        _menuPutOn.SetActive(true);
        _menuPutOn.transform.position = Input.mousePosition;
        _nameItem.text = name;
        _strengthItem.text = "Сила " + st;
        _agilityItem.text = "Ловкость " + ag;
        _intellectItem.text = "Интеллект " + intel;
        _physicsDamage.text = "Физический урон " + pD;
        _magicDamage.text = "Магический урон " + mD;
    }

    public void PutOnButton()
    {
        if (itemWeapon == 1)
            putOnItem();
        else if (itemWeapon == 2)
            putOnWeapon();
        _menuPutOn.SetActive(false);
    }

    public void DeleteItemFromBag()
    {
        deleteItem();
        _menuPutOn.SetActive(false);
    }
}
