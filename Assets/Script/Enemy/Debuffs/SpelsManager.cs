using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpelsManager : MonoBehaviour
{
    public delegate void CooldownTimerSpells();
    public static event CooldownTimerSpells cooldownTimerSpells;    

    [SerializeField]
    private Transform[] _spellPosition;
    public bool[] _spellSlot;

    public static float damageEffect;

    public static bool spelIsActive;
    public static float timeFlySpell;
    public List<float> timeFlySpelToEnemy;

    public ManagerAnimationSpells managerAnimationSpells;
    public BattleManager battleManager;
    public Character character;
    public static float weakeningPhysicsDamage;
    public static bool aimedStrike;

    public static bool sweepingBlow;
    public static bool furiousBlow;
    public static bool furiousTwo;

    public static float combo;

    public static bool scorchIsActive;
    public static float scorch;
    public static bool fireBall;
    public static bool fireJet;

    public static bool frostbiteIsActive;
    public static float frostbite;
    public static bool frostStorm;
    public static bool frostThorn;

    public static float staticElectricity;
    public static bool lightningBolt;
    public static bool electricalDischarge;
    public static bool electricSpell;

    private void Start()
    {
        ClicUpSpells.upSpell += UpSpell;
    }
    private void OnDestroy()
    {
        ClicUpSpells.upSpell -= UpSpell;
    }

    private void UpSpell(GameObject spell)
    {
        for (int i = 0; i < _spellPosition.Length; i++)
        {
            if(!_spellSlot[i])
            {
                Instantiate(spell, _spellPosition[i]);
                _spellSlot[i] = true;
                break;
            }
        }
    }

    public static void EffectActiveFalse()
    {
        aimedStrike = false;
        sweepingBlow = false;
        furiousBlow = false;
        furiousTwo = false;
        frostbiteIsActive = false;
        scorchIsActive = false;
        frostStorm = false;
        electricSpell = false;
        lightningBolt = false;
        electricalDischarge = false;
        frostThorn = false;
        fireBall = false;
        fireJet = false;

        cooldownTimerSpells();
    }

    public void Masseffect()
    {
        if (sweepingBlow)
           SweepingBlow();

        if (furiousTwo)
            FuriousTwo();

        if (frostStorm)
            FrostStorm();

        if (electricSpell)
            StaticElectric();

        if (lightningBolt)
            LightningBolt();
    }    

    public void SweepingBlow()
    {
        for (int i = 0; i < battleManager.listEnemy.Count; i++)
        {
            if (battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
            {
                if (battleManager.listEnemy.Count == 1) return;

                else if (i == 0)
                {
                    if (!battleManager.listEnemy[i + 1].GetComponent<Enemy>()._isDead)
                    {
                        battleManager.listEnemy[i + 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                        managerAnimationSpells.SweepingBlowAnimation(i + 1);
                    }
                }
                else if (i == battleManager.listEnemy.Count - 1)
                {
                    if (!battleManager.listEnemy[i - 1].GetComponent<Enemy>()._isDead)
                    {
                        battleManager.listEnemy[i - 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                        managerAnimationSpells.SweepingBlowAnimation(i - 1);
                    }
                }
                else
                {
                    if (!battleManager.listEnemy[i + 1].GetComponent<Enemy>()._isDead)
                    { 
                        battleManager.listEnemy[i + 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                        managerAnimationSpells.SweepingBlowAnimation(i + 1);
                    }
                    if (!battleManager.listEnemy[i - 1].GetComponent<Enemy>()._isDead)
                    {
                        battleManager.listEnemy[i - 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                        managerAnimationSpells.SweepingBlowAnimation(i - 1);
                    }             
                }
            }
        }
    }

    public void FuriousTwo()
    {
        foreach (var i in battleManager.listEnemy)
        {
            if (i.GetComponent<Enemy>().isTarget)
                i.GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
        }
    }
   

    public void FrostStorm()
    {
        foreach (var i in battleManager.listEnemy)
        {
            if (!i.GetComponent<Enemy>().isTarget && !i.GetComponent<Enemy>()._isDead)
            {
                i.GetComponent<Enemy>().TakingDebufsDamage(character.intellect);                
            }
            i.GetComponent<Debuffs>().Frostbite();
        }       
    }

    public void StaticElectric()
    {
        foreach (var e in battleManager.listEnemy)
        {
            if(e.GetComponent<Enemy>().isTarget && e.GetComponent<Debuffs>().StaticElectricity() > 0)
            {
                StartCoroutine(ElectricStaticDamage(e.GetComponent<Debuffs>().StaticElectricity(), e));
            }
        }
    }

    private void LightningBolt()
    {
        for (int i = 0; i < battleManager.listEnemy.Count; i++)
        {
            if (battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
            {
                if (battleManager.listEnemy.Count == 1) return;

                else if (i == 0)
                {
                    battleManager.listEnemy[i + 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                    if(battleManager.listEnemy[i + 1].GetComponent <Debuffs>().StaticElectricity() > 0)
                    {
                        StartCoroutine(ElectricStaticDamage(battleManager.listEnemy[i + 1].GetComponent<Debuffs>().StaticElectricity(), battleManager.listEnemy[i + 1]));
                    }
                }
                else if (i == battleManager.listEnemy.Count - 1)
                {
                    battleManager.listEnemy[i - 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                    if (battleManager.listEnemy[i - 1].GetComponent<Debuffs>().StaticElectricity() > 0)
                    {
                        StartCoroutine(ElectricStaticDamage(battleManager.listEnemy[i - 1].GetComponent<Debuffs>().StaticElectricity(), battleManager.listEnemy[i - 1]));
                    }
                }
                else
                {
                    battleManager.listEnemy[i + 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                    battleManager.listEnemy[i - 1].GetComponent<Enemy>().TakingDebufsDamage(damageEffect);
                    if (battleManager.listEnemy[i + 1].GetComponent<Debuffs>().StaticElectricity() > 0)
                    {
                        StartCoroutine(ElectricStaticDamage(battleManager.listEnemy[i + 1].GetComponent<Debuffs>().StaticElectricity(), battleManager.listEnemy[i + 1]));
                    }
                    if (battleManager.listEnemy[i - 1].GetComponent<Debuffs>().StaticElectricity() > 0)
                    {
                        StartCoroutine(ElectricStaticDamage(battleManager.listEnemy[i - 1].GetComponent<Debuffs>().StaticElectricity(), battleManager.listEnemy[i - 1]));
                    }
                }
            }
        }
    }    

    IEnumerator ElectricStaticDamage(float damage, GameObject o)
    {
        List<GameObject> enemyNotStaticElectric = new List<GameObject>();
        foreach (var i in battleManager.listEnemy)
        {            
            if( i != o && !i.GetComponent<Enemy>()._isDead)
            {
                enemyNotStaticElectric.Add(i);
            }
        }
        
        float staticDamage = damage;
        while(enemyNotStaticElectric.Count > 0)
        {
            int r = Random.Range(0, enemyNotStaticElectric.Count);
            enemyNotStaticElectric[r].GetComponent<Enemy>().TakingDebufsDamage(staticDamage);

            if (enemyNotStaticElectric[r].GetComponent<Debuffs>().StaticElectricity() > 0)
            {
                staticDamage = enemyNotStaticElectric[r].GetComponent<Debuffs>().StaticElectricity();
                enemyNotStaticElectric.RemoveAt(r);
            }
            else
                break;
        }
        yield return null;
    }

    public float TimFlySpell()
    {
        for (int i = 0; i < battleManager.listEnemy.Count; i++)
        {
            if (battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
            {
                timeFlySpell = timeFlySpelToEnemy[i];
            }
        }
        return timeFlySpell;
    }
}
