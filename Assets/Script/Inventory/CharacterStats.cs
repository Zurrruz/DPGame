using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public Character _character;

    [SerializeField]
    private Text _strength;
    [SerializeField]
    private Text _agility;
    [SerializeField]
    private Text _intellect;
    [SerializeField]
    private Text _physicsDamage;
    [SerializeField]
    private Text _magicDamage;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StatsText();
    }

    private void StatsText()
    {
        _strength.text = "" + _character.strength;
        _agility.text = "" + _character.agility;
        _intellect.text = "" + _character.intellect;
        _physicsDamage.text = "���������� ���� " + _character.physicsDamage;
        _magicDamage.text = "���������� ���� " + _character.magicDamage;
    }
}
