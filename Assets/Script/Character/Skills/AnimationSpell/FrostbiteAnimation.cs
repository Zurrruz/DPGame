using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostbiteAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.GetComponent<Debuffs>().FrostbiteDebuf() <= 0 || transform.parent.GetComponent<Enemy>()._isDead)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<Debuffs>().frostbiteAnimTrue = false;
    }
}
