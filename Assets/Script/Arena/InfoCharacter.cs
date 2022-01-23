using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCharacter : MonoBehaviour
{
    public Character character;

    [SerializeField]
    private Text _heals;
    

    void Start()
    {
    }

    void Update()
    {
        _heals.text = character._heals + "";
    }
}
