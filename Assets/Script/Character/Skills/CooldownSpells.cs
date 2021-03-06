using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownSpells : MonoBehaviour
{
    [SerializeField]
    private float _cooldown;
    private float _cooldownTimer;
    public bool _isActive;


    private void Start()
    {
        Character.cooldownTimer += Cooldown;
        SpelsManager.cooldownTimerSpells += IsActive;
        BattleManager.resetCooldownSpell += ResetCooldown;
    }

    private void Cooldown()
    {
        if (_isActive)
            _cooldownTimer = _cooldown;
        else
            _cooldownTimer--;

        if (_cooldownTimer <= 0)
            GetComponent<ChoiceSpell>().Active();
    }
    private void IsActive()
    {
        _isActive = false;
    }

    public float CooldownTimer()
    {
        return _cooldownTimer;
    }

    private void ResetCooldown()
    {
        _cooldownTimer = 0;
        SpelsManager.combo = 0;
        GetComponent<ChoiceSpell>().Active();
    }

    private void OnDestroy()
    {
        Character.cooldownTimer -= Cooldown;
        SpelsManager.cooldownTimerSpells -= IsActive;
        BattleManager.resetCooldownSpell -= ResetCooldown;
    }
}
