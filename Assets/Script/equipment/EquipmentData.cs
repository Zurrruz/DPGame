using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Gameplay/New equipment")]
public class EquipmentData : ScriptableObject
{
    [SerializeField]
    private string _nameItem;
    [Header("Stats")]
    [SerializeField]
    private float _minStrength;
    [SerializeField]
    private float _maxStrength;
    [SerializeField]
    private float _minAgility;
    [SerializeField]
    private float _maxAgility;
    [SerializeField]
    private float _maxIntellect;
    [SerializeField]
    private float _minIntellect;

    [Header("Материал")]
    [SerializeField]
    private bool _platemail;
    [SerializeField]
    private bool _leather;
    [SerializeField]
    private bool _cloth;

    [Header("Вид доспеха")]
    [SerializeField]
    private bool _glovers;
    [SerializeField]
    private bool _boots;
    [SerializeField]
    private bool _helm;
    [SerializeField]
    private bool _chest;

    public float minStrength => this._minStrength;
    public float maxStrength => this._maxStrength;

    public float minAgility => this._minAgility;
    public float maxAgility => this._maxAgility;

    public float minIntellect => this._maxIntellect;
    public float maxIntellect => this._minIntellect;

    public bool platemail => this._platemail;
    public bool leather => this._leather;
    public bool cloth => this._cloth;

    public bool glovers => this._glovers;
    public bool boots => this._boots;
    public bool helm => this._helm;
    public bool chest => this._chest;

    public string nameItem => this._nameItem;
}
