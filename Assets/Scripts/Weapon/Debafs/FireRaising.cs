using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaising : MonoBehaviour
{
    public LowEnemy lw;

    bool _isBurning = false;
    float _fireDamage;

    public void Arson(float fireDamage)
    {
        _fireDamage = fireDamage;
        if (!_isBurning)
        {
            _isBurning = true;
            _fireDamage -= lw._resistFire;
            StartCoroutine(TakingDamage());
        }
    }

    IEnumerator TakingDamage()
    {
        while(_fireDamage > 0)
        {
            _fireDamage--;
            lw._health--;
            yield return new WaitForSeconds(1f);
        }       
    }

    private void Update()
    {
        if(_fireDamage > 0)
        {
            _isBurning = true;
        }
        else
        {
            _isBurning = false;
        }
    }
}
