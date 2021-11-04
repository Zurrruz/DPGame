using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Gameplay/New equipment")]
public class EquipmentData : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private float _strength;
    [SerializeField]
    private float _agility;
    [SerializeField]
    private float _intellect;

    [SerializeField]
    private bool _platemail;
    [SerializeField]
    private bool _leather;
    [SerializeField]
    private bool _cloth;

    [SerializeField]
    private bool _glovers;
    [SerializeField]
    private bool _boots;
    [SerializeField]
    private bool _helm;
    [SerializeField]
    private bool _chest;

    public float strength => this._strength;
    public float agility => this._agility;
    public float intellect => this._intellect;

    public bool platemail => this._platemail;
    public bool leather => this._leather;
    public bool cloth => this._cloth;

    public bool glovers => this._glovers;
    public bool boots => this._boots;
    public bool helm => this._helm;
    public bool chest => this._chest;
}
