using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    public float _strength;
    private float _agility;
    private float _intellect;

    public delegate void AddStats(float st, float ag, float intel);

    public static event AddStats addStatsFromChest;
    public static event AddStats addStatsFromBoots;
    public static event AddStats addStatsFromGlovers;
    public static event AddStats addStatsFromHelm;

    public LayerMask _item;

    private void Start()
    {
       
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Item"))
    //    {
    //        if (collision.transform.GetComponent<EquipmentItems>().chest  && !_putOn)
    //        {
    //            _putOn = true;
    //            _strength = collision.transform.GetComponent<EquipmentItems>().strength;
    //            _agility = collision.transform.GetComponent<EquipmentItems>().agility;
    //            _intellect = collision.transform.GetComponent<EquipmentItems>().intellect;

    //            addStatsFromChest(_strength, _agility, _intellect);
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Item"))
    //    {

    //    }
    //    _putOn = false;
    //}

    private void Update()
    {
        AddStatsFromChest();
        AddStatsFromBoots();
        AddStatsFromGlovers();
        AddStatsFromHelm();
    }

    private void AddStatsFromChest()
    {
        var Hit2D = Physics2D.Raycast(transform.position, new Vector3(0, 0, -15), 30, _item);
        if (Hit2D.transform != null)
        {
            if (Hit2D.transform.GetComponent<EquipmentItems>().chest)
            {
                _strength = Hit2D.transform.GetComponent<EquipmentItems>().strength;
                _agility = Hit2D.transform.GetComponent<EquipmentItems>().agility;
                _intellect = Hit2D.transform.GetComponent<EquipmentItems>().intellect;

                addStatsFromChest(_strength, _agility, _intellect);
            }
        }
        else
        {
            addStatsFromChest(0, 0, 0);
        }
    }

    private void AddStatsFromGlovers()
    {
        var Hit2D = Physics2D.Raycast(transform.position, new Vector3(0, 0, -15), 30, _item);
        if (Hit2D.transform != null)
        {
            if (Hit2D.transform.GetComponent<EquipmentItems>().glovers)
            {
                _strength = Hit2D.transform.GetComponent<EquipmentItems>().strength;
                _agility = Hit2D.transform.GetComponent<EquipmentItems>().agility;
                _intellect = Hit2D.transform.GetComponent<EquipmentItems>().intellect;

                addStatsFromGlovers(_strength, _agility, _intellect);
            }
        }
        else
        {
            addStatsFromGlovers(0, 0, 0);
        }
    }

    private void AddStatsFromBoots()
    {
        var Hit2D = Physics2D.Raycast(transform.position, new Vector3(0, 0, -15), 30, _item);
        if (Hit2D.transform != null)
        {
            if (Hit2D.transform.GetComponent<EquipmentItems>().boots)
            {
                _strength = Hit2D.transform.GetComponent<EquipmentItems>().strength;
                _agility = Hit2D.transform.GetComponent<EquipmentItems>().agility;
                _intellect = Hit2D.transform.GetComponent<EquipmentItems>().intellect;

                addStatsFromBoots(_strength, _agility, _intellect);
            }
        }
        else
        {
            addStatsFromBoots(0, 0, 0);
        }
    }

    private void AddStatsFromHelm()
    {
        var Hit2D = Physics2D.Raycast(transform.position, new Vector3(0, 0, -15), 30, _item);
        if (Hit2D.transform != null)
        {
            if (Hit2D.transform.GetComponent<EquipmentItems>().helm)
            {
                _strength = Hit2D.transform.GetComponent<EquipmentItems>().strength;
                _agility = Hit2D.transform.GetComponent<EquipmentItems>().agility;
                _intellect = Hit2D.transform.GetComponent<EquipmentItems>().intellect;

                addStatsFromHelm(_strength, _agility, _intellect);
            }
        }
        else
        {
            addStatsFromHelm(0, 0, 0);
        }
    }
}
