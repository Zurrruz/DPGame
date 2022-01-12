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
    private float _addDamage;
    private bool _iDamage;
    [SerializeField, Tooltip("Откат бафа на урон")]
    private int kdAddDamage;
    private int _kdAddDamage;

    [Header("Магический щит")]
    [SerializeField]
    private bool _magicShield;
    [SerializeField, Tooltip("Сколько урона поглотит щит")]
    private float _absorbDamage;
    [SerializeField, Tooltip("При каком количестве хп активируется щит")]
    private float _healsActivateShield;

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



    // Start is called before the first frame update
    void Start()
    {
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
            IncreasedWariorMage();

        ShamanBufsDamage(_shamanDamage, true);
    }

    private void IncreasedDamage()
    {
        if (_kdAddDamage <= 0)
        {
            enemy._mDamage += _addDamage;
            _kdAddDamage = kdAddDamage;
            _iDamage = true;
        }
        else if (_iDamage)
        {
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
                enemy._pDamage -= 1;
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

    private void IncreasedWariorMage()
    {
        foreach (var enemy in battleManager.listEnemy)
        {
            if (enemy.GetComponent<Enemy>()._heals <= enemy.GetComponent<Enemy>().heals / 2)
            {
                enemy.GetComponent<Enemy>()._heals += _healing;
                _activeHealing = true;
                break;
            }
            else
                _activeHealing = false;
        }

        List<GameObject> _warior = new List<GameObject>();
        List<GameObject> _mage = new List<GameObject>();
        if (!_activeHealing)
        {
            if (_kdDamageShaman <= 0)
            {
                int r = Random.Range(1, 3);
                foreach (var e in battleManager.listEnemy)
                {
                    if (e.GetComponent<Enemy>()._warior)
                    {
                        _warior.Add(e);
                    }
                    else if (e.GetComponent<Enemy>()._mage && !e.GetComponent<MagicSpels>()._shaman)
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
                        Debug.Log("Усилил воина");
                    }
                    else if (_mage.Count != 0)
                    {
                        _mage[w].GetComponent<MagicSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        Debug.Log("Усилил мага");
                    }
                }
                else if (r == 2)
                {
                    if (_mage.Count != 0)
                    {
                        _mage[w].GetComponent<MagicSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        Debug.Log("Усилил мага");
                    }
                    else if (_warior.Count != 0)
                    {
                        _warior[w].GetComponent<PhysicsSpels>().ShamanBufsDamage(_addDamageWarior, false);
                        Debug.Log("Усилил воина");
                    }
                }
                _kdDamageShaman = kdAddDamageShaman;
            }
        }        
    }

    public bool IsShaman()
    {
        return _shaman;
    }
}
