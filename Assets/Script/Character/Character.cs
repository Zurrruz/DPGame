using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public BattleManager battleManager;

    [Header("Основные характеристики")]
    public float agility;
    public float strength;
    public static float strenghtBonusEffect;
    public float intellect;
    public static float intellectBonusEffectSpell;
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

    [SerializeField]
    private GameObject _gameOverBox;

    public GameObject _player;
    private Animator _anim;
    [SerializeField]
    private float _animAttackTimer;
    [SerializeField]
    private float _animTakingDamage;

    private SpriteRenderer _sr;
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
        _anim = _player.GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _queueManager._queueAttacks.Add(queueAttack);
        battleManager.actualQueueAttack.Add(queueAttack);
        _initiative = basicinitiative;
        _heals = baseHeals;
        physicsDamage = basePDamage;
        magicDamage = baseMDamage;
        magicEffectDamage = baseMDamage;
        _gameOverBox.SetActive(false);

        intellectBonusEffectSpell = intellect;
    }

    public void Stats()
    {
        _heals = strength * 3 + baseHeals;
        _initiative = basicinitiative + agility;
        //_critical = agility;
        _dodge = agility * 2 + 20;
        intellectBonusEffectSpell = intellect;
        strenghtBonusEffect = strength;

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
            StartCoroutine(TakingDamageAnimation());
        }
        else
        {
            StartCoroutine(BlockAnimanion());
            Debug.Log("Вы улонились от атаки");
        }
        if(_heals <= 0)
        {
            _gameOverBox.SetActive(true);
            _anim.SetBool("Dying", true);
        }
    }
    IEnumerator BlockAnimanion()
    {
        _anim.SetBool("Block", true);
        yield return new WaitForSeconds(1.3f);
        _anim.SetBool("Block", false);
    }
    public void TakingMagicDamage(float mDamage)
    {
        _heals -= mDamage;
        StartCoroutine(TakingDamageAnimation());
        if (_heals <= 0)
        {
            _gameOverBox.SetActive(true);
            _anim.SetBool("Dying", true);
        }
    }

    IEnumerator TakingDamageAnimation()
    {
        _anim.SetBool("TakingDamage", true);
        yield return new WaitForSeconds(_animTakingDamage);
        _anim.SetBool("TakingDamage", false);
    }

    public static bool spell;
    public float DealtDamageEnemy()
    {
        if (BattleManager.physicsDamage)
        {
            float c = Random.Range(0, 101);
            if (c <= _critical)
            {
                _damageDealt = (physicsDamage + spellsDamage) * _increasedCriticalDamage;
            }
            else
            {
                if (!spell)
                    _damageDealt = physicsDamage;
                else
                    _damageDealt = spellsDamage + strength;
            }
        }
        else if (BattleManager.magicDamage)
        {
            float c = Random.Range(0, 101);
            if (c <= _critical)
            {
                _damageDealt = (magicDamage + spellsDamage) * _increasedCriticalDamage;
            }
            else
            {
                if (!spell)
                    _damageDealt = magicDamage;
                else
                    _damageDealt = spellsDamage + intellect;
            }
        }
        
        queueAttack._actualStepInitiative -= battleManager._queueStep;
        //_queueManager.QueueAttack();
        //battleManager.QueueAttack();
        cooldownTimer();
        return _damageDealt;
    
    }

    public void AnimationAttackOn()
    {
        StartCoroutine(AnimationAttack());
    }

    IEnumerator AnimationAttack()
    {
        _anim.SetBool("Attack", true);
        yield return new WaitForSeconds(_animAttackTimer);
        _anim.SetBool("Attack", false);
    }
    
}
