using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceChainsAnimation : MonoBehaviour
{

    [SerializeField]
    private bool _iceChains;

    void Start()
    {
        transform.parent.GetComponent<Enemy>().DebufsAnimation(gameObject, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IceChains()
    {
        return _iceChains;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<Enemy>().DebufsAnimation(gameObject, false);
    }
}
