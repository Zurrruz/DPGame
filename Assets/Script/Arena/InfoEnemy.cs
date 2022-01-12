using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoEnemy : MonoBehaviour
{
    [SerializeField]
    private Text _healsEnemy;
    [SerializeField]
    private Text _damageEnemy;
    [SerializeField]
    private Text _magicShield;

    void Update()
    {
        if (transform.childCount > 0)
        {
            _healsEnemy.text = "HP " + transform.GetChild(0).GetComponent<Enemy>()._heals;

            if (transform.GetChild(0).GetComponent<Enemy>()._mage)
            {
                _magicShield.text = "" + transform.GetChild(0).GetComponent<Enemy>()._magicShield;
                _damageEnemy.text = "" + transform.GetChild(0).GetComponent<Enemy>()._mDamage;
            }
            else if(transform.GetChild(0).GetComponent<Enemy>()._warior)
            {
                _damageEnemy.text = "" + transform.GetChild(0).GetComponent<Enemy>()._pDamage;

            }
        }
    }
}
