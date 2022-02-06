using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticElectricityAnimation : MonoBehaviour
{
    Animator _anim;
    public float t;

    void Start()
    {
        _anim = GetComponent<Animator>();
        t = Random.Range(1, 6);
        StartCoroutine(AnimationStatic());
    }

    private void Update()
    {
        if (transform.parent.GetComponent<Debuffs>().StaticElectricity() <= 0 || transform.parent.GetComponent<Enemy>()._isDead)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<Debuffs>()._staticAnimTrue = false;
    }

    IEnumerator AnimationStatic()
    {
        yield return new WaitForSeconds(t);
        _anim.SetBool("True", true);
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("True", false);
        t = Random.Range(1, 6);
        StartCoroutine(AnimationStatic());
    }
}
