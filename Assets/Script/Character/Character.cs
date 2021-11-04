using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public float agility;
    public float strength;
    public float intellect;

    private float _damage;
    private float _heals;

    private float _initiative;
    private float _critical;
    private float _dodge;

    private float _mana;
    private float _resist;

    private void Update()
    {
        Stats();
    }

    private void Stats()
    {
        _damage = strength;

        _initiative = agility;
        _critical = agility;
        _dodge = agility;

        _mana = intellect;
        _resist = intellect;
    }

    public void AddStats(float st, float ag, float intel)
    {
        strength += st;
        agility += ag;
        intellect += intel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spot"))
            Dice.result--;
    }


}
