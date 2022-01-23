using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEvent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.gameObject.GetComponent<PathCell>().playerHere)
            StartCoroutine(EventText());
    }

    IEnumerator EventText()
    {
        yield return new WaitForSeconds(0.5f);
        EventManager.RenewText("Пока никакого события не случилось");
    }
}
