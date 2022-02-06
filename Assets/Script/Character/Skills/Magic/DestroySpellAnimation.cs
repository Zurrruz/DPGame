using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpellAnimation : MonoBehaviour
{
    [SerializeField]
    private float _timerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _timerAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
