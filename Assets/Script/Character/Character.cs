using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public BattleManager battleManager;

    [Header("Основные характеристики")]
    public float agility;
    public float strength;
    public float intellect;
    public static float intellectSpellDamage;
    public float baseHeals;
    public float _heals;
    [Header("Начальный урон")]
    public float basePDamage;
    public float baseMDamage;
    public float physicsDamage;
    public float magicDamage;
    public static float magicEffectDamage;
    public static float spellsDamage;

    private float _damageDealt;
    [Header("Начальное значение зависимых параметров")]
    [SerializeField]
    public float _initiative;
    public float basicinitiative;
    [SerializeField]
    private float _critical;
    [SerializeField]
    private float _increasedCriticalDamage;
    [SerializeField]
    private float _dodge;
    [SerializeField]
    private float _mana;
    [SerializeField]
    private float _resist;

    private QueueAttack queueAttack;
    private QueueManager _queueManager;

    public delegate void CooldownTimer();
    public static event CooldownTimer cooldownTimer;

    public static bool isActive;

    private void Awake()
    {
        _queueManager = GameObject.FindGameObjectWithTag("QueueManager").GetComponent<QueueManager>();
        queueAttack = GetComponent<QueueAttack>();
    }
    private void Start()
    {
        _queueManager._queueAttacks.Add(queueAttack);
        battleManager.actualQueueAttack.Add(queueAttack);
        _initiative = basicinitiative;
        _heals = baseHeals;
        physicsDamage = basePDamage;
        magicDamage = baseMDamage;
        magicEffectDamage = baseMDamage;
    }

    private void Update()
    {
        
    }

    public void Stats()
    {
        _heals = strength * 10 + baseHeals;
        _initiative = basicinitiative + agility;
        _critical = agility;
        _dodge = agility;

        _mana = intellect;
        _resist = intellect;
        magicEffectDamage = magicDamage;
    }
        

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spot"))
            Dice.result--;
    }

    public void TakingPhysicsDamage(float pDamage)
    {
        float d = Random.Range(0, 101);
        if(d > _dodge)
        {
            _heals -= pDamage;
        }
        else
        {
            Debug.Log("Вы улонились от атаки");
        }
    }
    public void TakingMagicDamage(float mDamage)
    {
        _heals -= mDamage;
    }

    public float DealtDamageEnemy()
    {
        if (BattleManager.physicsDamage)
        {
            float c = Random.Range(0, 101);
            if(c <= _critical)
            {
                _damageDealt = (physicsDamage + spellsDamage) * _increasedCriticalDamage;
            }
            else
                _damageDealt = physicsDamage + spellsDamage;
        }
        else if (BattleManager.magicDamage)
        {
            float c = Random.Range(0, 101);
            if (c <= _critical)
            {
                _damageDealt = (magicDamage + spellsDamage) * _increasedCriticalDamage;
            }
            else
                _damageDealt = magicDamage + spellsDamage;
        }
        queueAttack._actualStepInitiative -= battleManager._queueStep;
        //_queueManager.QueueAttack();
        //battleManager.QueueAttack();
        cooldownTimer();
        return _damageDealt;
    }
}
