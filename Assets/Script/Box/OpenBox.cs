using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenBox : MonoBehaviour, IPointerClickHandler
{
    
    public List<GameObject> _items;
    public List<GameObject> _weapons;
    public List<GameObject> _spells;

    [SerializeField]
    private GameObject[] _spawn;
    public bool[] _isFull;

    
    public UIPlayerButton uiPlayerButton;

    public delegate void Take();
    public static event Take take;

    public delegate void AddItemWeaponInBox();
    public static event AddItemWeaponInBox addItemWeaponInBox;
    public delegate void CameraPlayer();
    public static event CameraPlayer cameraPlayer;

    public void AddItem(GameObject item)
    {
        _items.Add(item);
    }
    public void AddWeapon(GameObject weapon)
    {
        _weapons.Add(weapon);
    }
    public void AddSpell(GameObject spell)
    {
        _spells.Add(spell);
    }

    private void ClearList()
    {
        if(_items != null)
            _items.Clear();
        if (_weapons != null)
            _weapons.Clear();
        if (_spells != null)
            _spells.Clear();

        for (int i = 0; i < _isFull.Length; i++)
        {
            _isFull[i] = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Spawn());
        uiPlayerButton.SetActiveChooseItem(true);
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < _spawn.Length; i++)
        {
            if(!_isFull[i])
            {
                int r = Random.Range(0, 3);        
                switch (r)
                {
                    case 0:
                        int it = Random.Range(0, _items.Count);
                        Instantiate(_items[it], _spawn[i].transform);
                        break;
                    case 1:
                        int w = Random.Range(0, _weapons.Count);
                        Instantiate(_weapons[w], _spawn[i].transform);
                        break;
                    case 2:
                        int s = Random.Range(0, _spells.Count);
                        Instantiate(_spells[s], _spawn[i].transform);
                        break;
                }
                _isFull[i] = true;
            }

            yield return new WaitForSeconds(0.2f);
        }
        ClearList();
    }

    public void TakeItemVeapon()
    {
        take();
            //uiPlayerButton.SetActiveChooseItem(false);
        //cameraPlayer();
        //Dice.thereIsAMove = false;
        MainCameraController.inBoxRoom = false;
        MainCameraController.inArenaRoom = false;
    }

    public void NextLvl()
    {
        
    }

    public void AddItemWeapon()
    {
        addItemWeaponInBox();
    }
}
