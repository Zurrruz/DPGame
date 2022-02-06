using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostStormAnimation : MonoBehaviour
{
    [SerializeField]
    float _timer;
    [SerializeField]
    float _speed;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(new Vector2(-1 * _speed, -1 * _speed));
        _timer -= Time.deltaTime;

        if (_timer <= 0)
            Destroy(gameObject);
    }
}
