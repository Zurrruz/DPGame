using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShieldAnimation : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<Enemy>()._magicShield <= 0 || transform.parent.GetComponent<Enemy>()._isDead)
            Destroy(gameObject);
    }
}
