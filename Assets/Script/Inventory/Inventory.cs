using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    private bool _on;

    private void Start()
    {
        inventory.SetActive(false);
        _on = true;
    }

    public void OnOff()
    {
        if (_on)
        {
            inventory.SetActive(true);
            _on = false;
        }
        else if (!_on)
        {
            inventory.SetActive(false);
            _on = true;
        }
    }
}
