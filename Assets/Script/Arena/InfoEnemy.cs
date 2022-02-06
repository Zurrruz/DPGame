using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoEnemy : MonoBehaviour
{
    [SerializeField]
    private Text _healsEnemy;
    [SerializeField]
    private Text _damageEnemy;
    [SerializeField]
    private Text _magicShield;

    [SerializeField]
    private GameObject _frostbiteDebuf;
    [SerializeField]
    private Text _frostbiteDebufText;
    [SerializeField]
    private GameObject _scorchDebuf;
    [SerializeField]
    private Text _scorchDebufText;
    [SerializeField]
    private GameObject _staticElectricityDebuf;
    [SerializeField]
    private Text _staticElectricityDebufText;
    [SerializeField]
    private GameObject _weakeningDebuf;
    [SerializeField]
    private Text _weakeningDebufText;

    private void Start()
    {
        NoDebufs();
    }

    void Update()
    {
        if (transform.childCount > 0)
        {
            if (!transform.GetChild(0).GetComponent<Enemy>()._isDead)
            {
                _healsEnemy.text = "HP " + transform.GetChild(0).GetComponent<Enemy>()._heals;

                if (transform.GetChild(0).GetComponent<Enemy>()._mage)
                {
                    _magicShield.text = "" + transform.GetChild(0).GetComponent<Enemy>()._magicShield;
                    _damageEnemy.text = "" + transform.GetChild(0).GetComponent<Enemy>()._mDamage;
                }
                else if (transform.GetChild(0).GetComponent<Enemy>()._warior)
                {
                    _damageEnemy.text = "" + transform.GetChild(0).GetComponent<Enemy>()._pDamage;
                }
                FrostDebuf();
                ScorchDebuf();
                StaticElectricityDebuf();
                WeakeningDebuf();
            }
            else
            {
                _magicShield.text = "";
                _damageEnemy.text = "";
                _healsEnemy.text = "";
                NoDebufs();
            }
        }
        else
        {
            _magicShield.text = "";
            _damageEnemy.text = "";
            _healsEnemy.text = "";
            NoDebufs();
        }
    }

    private void NoDebufs()
    {
        _frostbiteDebuf.SetActive(false);
        _scorchDebuf.SetActive(false);
        _staticElectricityDebuf.SetActive(false);
        _weakeningDebuf.SetActive(false);
    }

    private void FrostDebuf()
    {
        if(transform.GetChild(0).GetComponent<Debuffs>().FrostbiteDebuf() > 0)
        {
            _frostbiteDebuf.SetActive(true);
            _frostbiteDebufText.text = "" + transform.GetChild(0).GetComponent<Debuffs>().FrostbiteDebuf();
        }
        else
        {
            _frostbiteDebuf.SetActive(false);
        }
    }

    private void ScorchDebuf()
    {
        if (transform.GetChild(0).GetComponent<Debuffs>().ScorchDebuf() > 0)
        {
            _scorchDebuf.SetActive(true);
            _scorchDebufText.text = "" + transform.GetChild(0).GetComponent<Debuffs>().ScorchDebuf();
        }
        else
            _scorchDebuf.SetActive(false);
    }

    private void StaticElectricityDebuf()
    {
        if (transform.GetChild(0).GetComponent<Debuffs>().StaticElectricity() > 0)
        {
            _staticElectricityDebuf.SetActive(true);
            _staticElectricityDebufText.text = "" + transform.GetChild(0).GetComponent<Debuffs>().StaticElectricity();
        }
        else
            _staticElectricityDebuf.SetActive(false);
    }

    private void WeakeningDebuf()
    {
        if (transform.GetChild(0).GetComponent<Debuffs>().WeakeningDebuf() > 0)
        {
            _weakeningDebuf.SetActive(true);
            _weakeningDebufText.text = "" + transform.GetChild(0).GetComponent<Debuffs>().WeakeningDebuf();
        }
        else
            _weakeningDebuf.SetActive(false);
    }
}
