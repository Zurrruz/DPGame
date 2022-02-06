using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectAnimation : MonoBehaviour
{
    [SerializeField]
    float _time;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _time -= Time.deltaTime;

        if (_time <= 0)
            Destroy(gameObject);
    }
}
