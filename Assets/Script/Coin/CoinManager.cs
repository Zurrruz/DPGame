using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static int coin;
    private int _coin;
    [SerializeField]
    private Text _coinText;
    [SerializeField]
    private GameObject _takeCoinButton;

    public delegate void TakeCoin();
    public static event TakeCoin takeCoin;

    public delegate void DestroyCoin();
    public static event DestroyCoin destroyCoin;

    void Start()
    {
        _takeCoinButton.SetActive(false);
        takeCoin += ActiveButtonCoin;
    }

    public static void EventActiveButton()
    {
        takeCoin();
    }

    private void ActiveButtonCoin()
    {
        StartCoroutine(ActivButton());
    }
    IEnumerator ActivButton()
    {
        yield return new WaitForSeconds(0.5f);
        if(Dice.result == 0)
            _takeCoinButton.SetActive(true);

    }

    public void ButtonCoin()
    {
        _coin += coin;
        _coinText.text = "" + _coin;
        _takeCoinButton.SetActive(false);
        destroyCoin();
        Dice.thereIsAMove = false;
    }

    private void OnDestroy()
    {
        takeCoin -= ActiveButtonCoin;
    }
}
