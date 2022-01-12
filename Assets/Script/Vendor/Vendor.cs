using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(GoToStore());
    }

    IEnumerator GoToStore()
    {
        yield return new WaitForSeconds(0.5f);
        if (transform.parent.gameObject.GetComponent<PathCell>().playerHere && Dice.result == 0)
        {
            VendorManager.EventVendorButton();
        }
    }
}
