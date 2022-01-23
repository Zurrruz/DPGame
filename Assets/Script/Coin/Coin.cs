using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int _countCoin;

    private void Start()
    {
        CoinManager.destroyCoin += DestroyCoin;
    }
    private void OnDestroy()
    {
        CoinManager.destroyCoin -= DestroyCoin;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.gameObject.GetComponent<PathCell>().playerHere)
        {
            CoinManager.EventActiveButton();
            CoinManager.coin = _countCoin;
        }
    }

    private void DestroyCoin()
    {
        if (transform.parent.gameObject.GetComponent<PathCell>().playerHere)
        {            
            Destroy(gameObject, 0.5f);
        }
    }
}
