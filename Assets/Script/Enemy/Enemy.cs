using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    private SpelsManager _spelsManager;
    private BattleManager battleManager;
    private SpriteRenderer _sr;
    private Debuffs _debuffs;
    private Character character;
    [Header("Характеристики")]
    [SerializeField]
    public float heals;
    [HideInInspector]
    public float _heals;
    [SerializeField]
    public float initiative;
    private float _frostInitiative;
    public bool _frostbite;

    [Header("Специализация")]
    [SerializeField]
    public bool _mage;
    [SerializeField]
    public bool _warior;

    [Header("Атака")]
    [SerializeField]
    private bool _physicsD;
    [SerializeField]
    public float _pDamage;
    [SerializeField]
    private bool _magicD;
    [SerializeField]
    public float _mDamage;

    [HideInInspector]
    public float _magicShield;

    private PhysicsSpels _physicsSpels;
    private MagicSpels _magicSpels;
    private QueueAttack _queueAttack;
    private QueueManager _queueManager;

    public bool isTarget;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _debuffs = GetComponent<Debuffs>();
        _spelsManager = GameObject.FindGameObjectWithTag("SpelsManager").GetComponent<SpelsManager>();
        _queueManager = GameObject.FindGameObjectWithTag("QueueManager").GetComponent<QueueManager>();
        _queueAttack = GetComponent<QueueAttack>();
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        if (_physicsD)
            _physicsSpels = GetComponent<PhysicsSpels>();
        if (_magicD)
            _magicSpels = GetComponent<MagicSpels>();
    }

    void Start()
    {
        _frostInitiative = initiative;
        _heals = heals;
        battleManager.listEnemy.Add(gameObject);
        battleManager.actualQueueAttack.Add(_queueAttack);
        _queueManager._queueAttacks.Add(_queueAttack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Character.isActive && SpelsManager.spelIsActive)
        {
            isTarget = true;
            TakingDamage();            
        }
    }
    private void TakingDamage()
    {
        if (_magicShield > 0)
        {
            _magicShield -= character.DealtDamageEnemy();
            if (_magicShield < 0)
            {
                _heals += _magicShield;
                _magicShield = 0;
                StartCoroutine(TakingDamageVisualization());
            }
        }
        else
        {
            _heals -= character.DealtDamageEnemy();
            StartCoroutine(TakingDamageVisualization());
        }
        _spelsManager.Masseffect();
        isTarget = false;
        Character.isActive = false;
        StartCoroutine(SpelIsNotActive());
        _queueManager.QueueAttack();
        battleManager.QueueAttack();
        Dead();
    }
    IEnumerator SpelIsNotActive()
    {
        yield return new WaitForSeconds(0.1f);
        SpelsManager.spelIsActive = false;
        if (SpelsManager.electricSpell)
            _debuffs.Electricity();
        yield return new WaitForSeconds(0.1f);
        SpelsManager.EffectActiveFalse();
    }

    public void TakingDebufsDamage(float damage)
    {
        if (_magicShield > 0)
        {
            _magicShield -= damage;
            if (_magicShield > 0) return;
            {
                _heals += _magicShield;
                _magicShield = 0;
            }
        }
        else
        {
            _heals -= damage;
            if(damage > 0)
                StartCoroutine(TakingDamageVisualization());
        }
        Dead();
    }
    IEnumerator TakingDamageVisualization()
    {
        _sr.color = new Color(0.8f, 0f, 0f);
        yield return new WaitForSeconds(0.1f);
        _sr.color = new Color(1f, 1f, 1f);
    }
    IEnumerator DamageVisualization()
    {
        _sr.color = new Color(0.3f, 1f, 0.5f);
        yield return new WaitForSeconds(0.1f);
        _sr.color = new Color(1f, 1f, 1f);
    }

    public void Frostbite(float f)
    {
        initiative -= f;
    }
    public void UnFrostbite()
    {
        initiative = _frostInitiative;
    }

    public void ActionEnemy()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        _queueAttack._actualStepInitiative -= battleManager._queueStep;
        yield return new WaitForSeconds(0.5f);
        if (!_frostbite)
        {
            if (_physicsD)
            {
                _physicsSpels.ActivateSpels();
                character.TakingPhysicsDamage(_pDamage);
            }

            if (_magicD)
            {
                _magicSpels.ActivateSpels();
                character.TakingPhysicsDamage(_mDamage);
            }
        }
        StartCoroutine(DamageVisualization());
        yield return new WaitForSeconds(1f);
        _debuffs.DebufEffect();
        _frostbite = false;
        yield return new WaitForSeconds(0.5f);
        _queueManager.QueueAttack();
    }   

   

    private void Dead()
    {
        if(_heals <= 0)
        {
            battleManager.DeadEnemy();
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnDestroy()
    {
        battleManager.listEnemy.Remove(gameObject);
        battleManager.actualQueueAttack.Remove(_queueAttack);
        _queueManager._queueAttacks.Remove(_queueAttack);
    }
}
