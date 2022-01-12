using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBox : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _item;
    [SerializeField]
    private List<GameObject> _weapon;
    [SerializeField]
    private List<GameObject> _spells;

    private OpenBox openBox;

    private void Awake()
    {
        openBox = GameObject.FindGameObjectWithTag("OpenChest").GetComponent<OpenBox>();
    }

    private void Start()
    {
        OpenBox.addItemWeaponInBox += AddItemWeaponInChest;
    }

    public void AddItemWeaponInChest()
    {
        if (transform.parent.gameObject.GetComponent<PathCell>().playerHere)
        {
            for (int i = 0; i < _item.Count; i++)
            {
                openBox.AddItem(_item[i]);
            }
            for (int w = 0; w < _weapon.Count; w++)
            {
                openBox.AddWeapon(_weapon[w]);
            }
            for (int s = 0; s < _spells.Count; s++)
            {
                openBox.AddSpell(_spells[s]);
            }
        }
    }
}
