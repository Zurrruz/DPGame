using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostThornAnimation : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    bool _tr;
    public float timer;
    void Start()
    {
        timer = 0;
        Destroy(gameObject, SpelsManager.timeFlySpell);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_tr)
        {
            transform.Translate(new Vector2(1 + _speed, 0));
            timer += Time.deltaTime;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<Enemy>().isTarget)
    //    {
    //        //_tr = true;
    //        Destroy(gameObject);
    //    }
    //}
}
