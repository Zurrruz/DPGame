using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _openBox;
    [SerializeField]
    private GameObject _chooseItem;
    [SerializeField]
    private GameObject _fightInTheArena;
    

    private void Start()
    {
        PathCell.playerOnTheChest += OpenBox;
        PathCell.fightInTheArena += FightInTheArena;
        _chooseItem.SetActive(false);
        _fightInTheArena.SetActive(false);
    }

    private void OpenBox()
    {
        StartCoroutine(OpenBoxL());
    }
    IEnumerator OpenBoxL()
    {
        yield return new WaitForSeconds(0.5f);

        if (Dice.result <= 0)
        {
            _openBox.SetActive(true);
        }
    }

    private void FightInTheArena()
    {
        StartCoroutine(Fight());
    }
    IEnumerator Fight()
    {
        yield return new WaitForSeconds(0.5f);
        if (Dice.result <= 0)
            _fightInTheArena.SetActive(true);
    }
    public void SetActiveFightInTheArens(bool set)
    {
        if (set)
            _fightInTheArena.SetActive(true);
        if (!set)
            _fightInTheArena.SetActive(false);
    }

    public void SetActiveChooseItem(bool set)
    {
        if(set)
            _chooseItem.SetActive(true);
        if (!set)
            _chooseItem.SetActive(false);
    }

    public void  FalseActiveOpenBoxButton()
    {
        _openBox.SetActive(false);
    }
    public void FalseActiveFightButton()
    {
        _fightInTheArena.SetActive(false);
    }    
    
}
