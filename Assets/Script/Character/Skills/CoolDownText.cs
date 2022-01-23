using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownText : MonoBehaviour
{
    [SerializeField]
    private Text _cd;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
            if (transform.GetChild(0).GetComponent<CooldownSpells>().CooldownTimer() > 0)
                _cd.text = "" + transform.GetChild(0).GetComponent<CooldownSpells>().CooldownTimer();
            else
                _cd.text = "";
        }
    }
}
