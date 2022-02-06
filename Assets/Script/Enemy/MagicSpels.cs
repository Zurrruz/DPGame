using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpels : MonoBehaviour
{
    private Enemy enemy;
    private BattleManager battleManager;

    private float _shamanDamage;

    [SerializeField, Tooltip("Включить баф на усиление урона")]
    private bool _increasedDamage;
    [SerializeField, Tooltip("Количество урона прибавляемого за баф")]
    private float addDamage;
    public float _addDamage;
    private bool _iDamage;
    [SerializeField, Tooltip("Откат бафа на урон")]
    private int kdAddDamage;
    private int _kdAddDamage;

    [Header("Магический щит")]
    [SerializeField]
    private bool _magicShield;
    [SerializeField]
    GameObject _magicShieldAnimation;
    [SerializeField, Tooltip("Сколько урона поглотит щит")]
    private float _absorbDamage;
    [SerializeField, Tooltip("При каком количестве хп активируется щит")]
    private float _healsActivateShield;
        
    private Animator _anim;
    [Header("Длина анимаций")]
    [SerializeField]
    private float _animBuffTimer;
    [SerializeField]
    private float _animAttacTimer;
    [SerializeField]
    private float _animHealingCasting;
    [SerializeField]
    private ParticleSystem _buffAddDamageParticle;

    [Header("Усиление другого")]
    [SerializeField]
    private bool _shaman;
    [SerializeField, Tooltip("На сколько увеличить урон у воина")]
    private float _addDamageWarior;
    [SerializeField, Tooltip("На сколько увеличить урон у мага")]
    private float _addDamageMage;
    [SerializeField, Tooltip("Кулдаун усиления")]
    private int kdAddDamageShaman;
    private int _kdDamageShaman;
    [SerializeField, Tooltip("Количество востанавливаемого здоровья")]
    private float _healing;
    private bool _activeHealing;
    [SerializeField]
    private GameObject _healingCast;

    public delegate void MagicShieldActivate(Transform transform);
    public static event MagicShieldActivate magicShieldActivate;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        battleManager = GameObject.Find("Arena").GetComponent<BattleManager>();
        enemy = GetComponent<Enemy>();
        _kdAddDamage = 0;
        _addDamage = addDamage;
    }

    public void ActivateSpels()
    {
        _kdAddDamage--;
        if (_increasedDamage)        
            IncreasedDamage();
        
        if (enemy._heals <= _healsActivateShield)
            MagicShield();

        _kdDamageShaman--;
        if (_shaman)
            StartCoroutine(IncreasedWariorMage());

        ShamanBufsDamage(_shamanDamage, true);
    }

    public float AnimAttackTimer()
    {
        return _animAttacTimer;
    }
    public void ParticleStop()
    {
        if(!_shaman)
            _buffAddDamageParticle.Stop();
    }
    private void IncreasedDamage()
    {
        if (_kdAddDamage <= 0)
        {
            _anim.SetBool("Buff", true);
            enemy.AnimBuffTimer(_animBuffTimer);
            _buffAddDamageParticle.Play(true);
            enemy._mDamage += _addDamage;
            _kdAddDamage = kdAddDamage;
            _iDamage = true;
        }
        else if (_iDamage)
        {
            enemy.AnimBuffTimer(0);
            if (_addDamage > 1)
            {
                enemy._mDamage -= 2;
                _addDamage -= 2;
                if (_addDamage == 0)
                {
                    _addDamage = addDamage;
                    _iDamage = false;
                }
            }
            else if (_addDamage == 1)
            {
                enemy._mDamage -= 1;
                _addDamage -= 1;
                if (_addDamage == 0)
                {
                    _addDamage = addDamage;
                    _iDamage = false;
                }
            }
        }
    }

    private void MagicShield()
    {
        if(_magicShield)
        {
            enemy._magicShield = _absorbDamage;
            _magicShield = false;
            Instantiate(_magicShieldAnimation, transform);
        }
    }

    public void ShamanBufsDamage(float damage, bool active)
    {
        if (!active)
        {
            enemy._mDamage += damage;
            _shamanDamage = damage;
        }
        else if (damage > 0)
        {
            enemy._mDamage -= 2;
            _shamanDamage -= 2;
        }
    }

    private IEnumerator IncreasedWariorMage()
    {
        foreach (var e in battleManager.listEnemy)
        {
            enemy.AnimBuffTimer(_animHealingCasting);
            if (e.GetComponent<Enemy>()._heals <= e.GetComponent<Enemy>().heals / 2 && !e.GetComponent<Enemy>()._isDead)
            {
                e.GetComponent<Enemy>()._heals += _healing;
                _activeHealing = true;
                _anim.SetBool("Healing" ,true);
                Instantiate(_healingCast, e.transform);
                yield return new WaitForSeconds(_animHealingCasting);
                _anim.SetBool("Healing", false);
                break;
            }
            else
                _activeHealing = false;
        }

        List<GameObject> _warior = new List<GameObject>();
        List<GameObject> _mage = new List<GameObject>();
        if (!_activeHealing)
        {
            enemy.AnimBuffTimer(_animBuffTimer);
            if (_kdDamageShaman <= 0)
            {
                int r = Random.Range(1, 3);
                foreach (var e in battleManager.listEnemy)
                {
                    if (e.GetComponent<Enemy>()._warior && !e.GetComponent<Enemy>()._isDead)
                    {
                        _warior.Add(e);
                    }
                    else if (e.GetComponent<Enemy>()._mage && !e.GetComponent<MagicSpels>()._shaman && !e.GetComponent<Enemy>()._isDead)
                    {
                        _mage.Add(e);
                    }
                }
                int w = Random.Range(0, _warior.Count);
                int m = Random.Range(0, _mage.Count);

                if (r == 1)
                {
                    if (_warior.Count != 0)
                    {
                        _warior[w].GetComponent<PhysicsSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        _warior[w].GetComponent<PhysicsSpels>().OnOffParticleBuff(); 
                    }
                    else if (_mage.Count != 0)
                    {
                        _mage[m].GetComponent<MagicSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        _mage[m].GetComponent<MagicSpels>().OnOffParticleBuff();
                    }
                }
                else if (r == 2)
                {
                    if (_mage.Count != 0)
                    {
                        _mage[m].GetComponent<MagicSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        _mage[m].GetComponent<MagicSpels>().OnOffParticleBuff();
                    }
                    else if (_warior.Count != 0)
                    {
                        _warior[w].GetComponent<PhysicsSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        _warior[w].GetComponent<PhysicsSpels>().OnOffParticleBuff();
                    }
                }
                _kdDamageShaman = kdAddDamageShaman;
                _anim.SetBool("Buff", true);
                yield return new WaitForSeconds(_animBuffTimer);
                _anim.SetBool("Buff", false);
            }
        }        
    }

    public bool IsShaman()
    {
        return _shaman;
    }

    public void OnOffParticleBuff()
    {
        StartCoroutine(OnOff());
    }
     IEnumerator OnOff()
    {
        _buffAddDamageParticle.Play();
        yield return new WaitForSeconds(_animBuffTimer);
        _buffAddDamageParticle.Stop();
    }
}
