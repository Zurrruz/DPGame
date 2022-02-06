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
    public List<GameObject> _debufsAnimation;

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

    private Animator _anim;
    private float _animBufftimer;
    private float _animAttackTimer;
    private float _animDebuffTimer;

    public bool isTarget;
    public bool _isDead;

    public delegate void AnimationSpells();
    public static event AnimationSpells animationSpells;

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
        _anim = GetComponent<Animator>();
        _frostInitiative = initiative;
        _heals = heals;
        battleManager.listEnemy.Add(gameObject);
        battleManager.actualQueueAttack.Add(_queueAttack);
        _queueManager._queueAttacks.Add(_queueAttack);
        if (_warior)
            _animAttackTimer = _physicsSpels.AnimAttackTimer();
        else if(_mage)
            _animAttackTimer = _magicSpels.AnimAttackTimer();

        BattleManager.destroyEnemy += Destroy;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Character.isActive && SpelsManager.spelIsActive && !_isDead)
        {           
            isTarget = true;
            animationSpells();
            StartCoroutine(TakingDamage());
            character.AnimationAttackOn();
        }
    }
    IEnumerator TakingDamage()
    {
        yield return new WaitForSeconds(_spelsManager.TimFlySpell());

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
        Dead();
        _spelsManager.Masseffect();        
        isTarget = false;
        Character.isActive = false;
        StartCoroutine(SpelIsNotActive());
        yield return new WaitForSeconds(0.2f);
        if (_heals > 0)
        {
            _queueManager.QueueAttack();
            battleManager.QueueAttack();
        }
        
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
        if(!_isDead)
            Dead();
    }
     IEnumerator TakingDamageVisualization()
    {
        _anim.SetBool("TakingDamage", true);
        yield return new WaitForSeconds(0.9f);
        _anim.SetBool("TakingDamage", false);
    }
     IEnumerator DamageVisualization()
    {
        if (_physicsD)
        {
            _anim.SetBool("Attack", true);
            yield return new WaitForSeconds(_animAttackTimer);
            _anim.SetBool("Attack", false);
        }
        else if(!_magicSpels.IsShaman())
        {
            _anim.SetBool("Attack", true);
            yield return new WaitForSeconds(_animAttackTimer);
            _anim.SetBool("Attack", false);
        }
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
        yield return new WaitForSeconds(1f);
        if (!_frostbite)
        {
            if (_physicsD)
            {
                _physicsSpels.ActivateSpels(); 
                yield return new WaitForSeconds(_animBufftimer);
                _anim.SetBool("Buff", false);
                _physicsSpels.ParticlStop();
                StartCoroutine(DamageVisualization());
                character.TakingPhysicsDamage(_pDamage);
            }

            if (_magicD)
            {
                _magicSpels.ActivateSpels();
                yield return new WaitForSeconds(_animBufftimer);
                _anim.SetBool("Buff", false);
                _magicSpels.ParticleStop();
                StartCoroutine(DamageVisualization());
                yield return new WaitForSeconds(_animAttackTimer);
                if (_mDamage > 0)
                    character.TakingPhysicsDamage(_mDamage);
            }
        }
        yield return new WaitForSeconds(_animAttackTimer);
        _debuffs.DebufEffect();
        _frostbite = false;
        _anim.enabled = true;
        DestroyDebufsAnimation();
        yield return new WaitForSeconds(_animDebuffTimer + 0.5f);
        _queueManager.QueueAttack();
        battleManager.QueueAttack();        
    }   

    public void AnimBuffTimer(float time)
    {
        _animBufftimer = time;
    }
    public void AnimDebuffTimer(float time)
    {
        _animDebuffTimer = time;
    }


    private void Dead()
    {
        if(_heals <= 0)
        {
            _isDead = true;
            DestroyDebufsAnimation();
            _anim.enabled = true;            
            _anim.SetBool("Dying", true);
           // battleManager.listEnemy.Remove(gameObject);
            battleManager.actualQueueAttack.Remove(_queueAttack);
            _queueManager._queueAttacks.Remove(_queueAttack);
            battleManager.DeadEnemy();
        }
    }
    private void Destroy()
    {
        Destroy(gameObject, 5f);
    }

    private void OnDestroy()
    {
        DestroyDebufsAnimation();        
        BattleManager.destroyEnemy -= Destroy;
        battleManager.listEnemy.Remove(gameObject);
    }

   public void AnimIdleStop()
    {
        _anim.enabled = false;
    }

    public void DebufsAnimation(GameObject gameObject, bool add)
    {
        if (add)
            _debufsAnimation.Add(gameObject);
        else
            _debufsAnimation.Remove(gameObject);
    }

    private void DestroyDebufsAnimation()
    {
        if (_debufsAnimation.Count > 0)
        {
            foreach (var d in _debufsAnimation)
            {
                if (d.GetComponent<IceChainsAnimation>().IceChains())
                    d.GetComponent<IceChainsAnimation>().Destroy();
            }
        }
    }
    
}
