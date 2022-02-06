using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnimationSpells : MonoBehaviour
{
    BattleManager _battleManager;
    [SerializeReference]
    private List<Transform> _topAnimSpellsSpawn;
    [SerializeField]
    private Transform _topSpawnSpell;
    [SerializeField]
    private Transform _botSpawnSpell;

    [SerializeField]
    GameObject _fireJat;
    [SerializeField]
    GameObject _electricalDischarge;
    [SerializeField]
    GameObject lightningBolt;
    [SerializeField]
    private GameObject _frostThorn;
    [SerializeField]
    private GameObject _fireBall;
    [SerializeField]
    GameObject _frostStormAnimation;
    [SerializeField]
    List<Transform> _spawnFrostStorm;
    [SerializeField]
    int _maxFrostStorm;

    [SerializeField]
    GameObject _magicShield;

    [SerializeField]
    GameObject _aimedStrike;

    private void Start()
    {
        _battleManager = GetComponent<BattleManager>();
        Enemy.animationSpells += AnimationSpellsActivate;
        MagicSpels.magicShieldActivate += MagicShield;
    }
    private void OnDestroy()
    {
        Enemy.animationSpells -= AnimationSpellsActivate;
    }

    private void AnimationSpellsActivate()
    {
        if(SpelsManager.fireJet)
        {
            for (int i = 0; i < _battleManager.listEnemy.Count; i++)
            {
                if(_battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
                {
                    Instantiate(_fireJat, _topAnimSpellsSpawn[i]);
                }
            }
        }

        else if(SpelsManager.electricalDischarge)
        {
            for (int i = 0; i < _battleManager.listEnemy.Count; i++)
            {
                if (_battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
                {
                    Instantiate(_electricalDischarge, _topAnimSpellsSpawn[i]);
                }
            }
        }

        else if(SpelsManager.lightningBolt)
        {
            for (int i = 0; i < _battleManager.listEnemy.Count; i++)
            {
                if (_battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
                {
                    if (i == 0 || i == 2 || i == 4)
                        Instantiate(lightningBolt, _topSpawnSpell);
                    else
                        Instantiate(lightningBolt, _botSpawnSpell);
                }
            }
        }

        else if(SpelsManager.frostThorn)
        {
            for (int i = 0; i < _battleManager.listEnemy.Count; i++)
            {
                if(_battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
                {
                    if(i == 0 || i == 2 || i == 4)
                        Instantiate(_frostThorn, _topSpawnSpell);
                    else
                        Instantiate(_frostThorn, _botSpawnSpell);
                }
            }            
        }

        else if(SpelsManager.fireBall)
        {
            for (int i = 0; i < _battleManager.listEnemy.Count; i++)
            {
                if (_battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
                {
                    if (i == 0 || i == 2 || i == 4)
                        Instantiate(_fireBall, _topSpawnSpell);
                    else
                        Instantiate(_fireBall, _botSpawnSpell);
                }
            }
        }

        else if(SpelsManager.aimedStrike || SpelsManager.furiousBlow || SpelsManager.sweepingBlow)
        {
            for (int i = 0; i < _battleManager.listEnemy.Count; i++)
            {
                if (_battleManager.listEnemy[i].GetComponent<Enemy>().isTarget)
                {
                    Instantiate(_aimedStrike, _battleManager.listEnemy[i].transform);
                }
            }
        }

        else if(SpelsManager.frostStorm)
        {
            StartCoroutine(StartAnimrostStorm());
        }
    }

    public void SweepingBlowAnimation(int i)
    {
        Instantiate(_aimedStrike, _battleManager.listEnemy[i].transform);
    }

    public void MagicShield(Transform transform)
    {
        Instantiate(_magicShield, transform);
    }

    IEnumerator StartAnimrostStorm()
    {
        for (int i = 0; i < _maxFrostStorm; i++)
        {
            int r = Random.Range(0, _spawnFrostStorm.Count - 1);
            Instantiate(_frostStormAnimation, _spawnFrostStorm[r].transform);
            float t = Random.Range(0, 0.1f);
            yield return new WaitForSeconds(t);
        }
    }
}
