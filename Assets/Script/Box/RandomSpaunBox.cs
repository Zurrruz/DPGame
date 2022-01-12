using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpaunBox : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _spotSpaunChestLow;
    [SerializeField]
    private GameObject[] _spotSpaunChestMedium;
    [SerializeField]
    private GameObject[] _spotSpaunChestHard;

    [SerializeField]
    private GameObject _chestLow;
    [SerializeField]
    private GameObject _chestMedium;
    [SerializeField]
    private GameObject _chestHard;

    [SerializeField]
    private GameObject _enemyLow;
    [SerializeField]
    private GameObject _enemyMedium;
    [SerializeField]
    private GameObject _enemyHard;

    [SerializeField]
    private GameObject _event;
    [SerializeField]
    private GameObject _coin;
    [SerializeField]
    private GameObject _store;

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        for (int i = 0; i < _spotSpaunChestLow.Length; i++)
        {
            int r = Random.Range(1, 3);

            if (r == 1)
            {
                Instantiate(_enemyLow, _spotSpaunChestLow[i].transform);
                //_spotSpaunBox[i].GetComponent<PathCell>().ChangeSprite(_chest);
                _spotSpaunChestLow[i].GetComponent<PathCell>().thereIsAEnemy = true;
            }
            else if (r == 2)
            {
                int c = Random.Range(1, 5);
                switch (c)
                {
                    case 1:
                        Instantiate(_chestLow, _spotSpaunChestLow[i].transform);
                        //_spotSpaunBox[i].GetComponent<PathCell>().ChangeSprite(_chest);
                        _spotSpaunChestLow[i].GetComponent<PathCell>().thereIsAChest = true;
                        break;

                    case 2:
                        Instantiate(_event, _spotSpaunChestLow[i].transform);
                        break;

                    case 3:
                        Instantiate(_coin, _spotSpaunChestLow[i].transform);
                        break;

                    case 4:
                        Instantiate(_store, _spotSpaunChestLow[i].transform);
                        break;
                }

            }
        }

        for (int i = 0; i < _spotSpaunChestMedium.Length; i++)
        {
            int r = Random.Range(1, 3);

            if (r == 1)
            {
                Instantiate(_enemyMedium, _spotSpaunChestMedium[i].transform);
                //_spotSpaunBox[i].GetComponent<PathCell>().ChangeSprite(_chest);
                _spotSpaunChestMedium[i].GetComponent<PathCell>().thereIsAEnemy = true;
            }
            else if (r == 2)
            {
                int c = Random.Range(1, 5);
                switch (c)
                {
                    case 1:
                        Instantiate(_chestMedium, _spotSpaunChestMedium[i].transform);
                        //_spotSpaunBox[i].GetComponent<PathCell>().ChangeSprite(_chest);
                        _spotSpaunChestMedium[i].GetComponent<PathCell>().thereIsAChest = true;
                        break;

                    case 2:
                        Instantiate(_event, _spotSpaunChestMedium[i].transform);
                        break;

                    case 3:
                        Instantiate(_coin, _spotSpaunChestMedium[i].transform);
                        break;

                    case 4:
                        Instantiate(_store, _spotSpaunChestMedium[i].transform);
                        break;
                }

            }
        }



        for (int i = 0; i < _spotSpaunChestHard.Length; i++)
        {
            int r = Random.Range(1, 3);

            if (r == 1)
            {
                Instantiate(_enemyHard, _spotSpaunChestHard[i].transform);
                //_spotSpaunBox[i].GetComponent<PathCell>().ChangeSprite(_chest);
                _spotSpaunChestHard[i].GetComponent<PathCell>().thereIsAEnemy = true;
            }
            else if (r == 2)
            {
                int c = Random.Range(1, 5);
                switch (c)
                {
                    case 1:
                        Instantiate(_chestHard, _spotSpaunChestHard[i].transform);
                        //_spotSpaunBox[i].GetComponent<PathCell>().ChangeSprite(_chest);
                        _spotSpaunChestHard[i].GetComponent<PathCell>().thereIsAChest = true;
                        break;

                    case 2:
                        Instantiate(_event, _spotSpaunChestHard[i].transform);
                        break;

                    case 3:
                        Instantiate(_coin, _spotSpaunChestHard[i].transform);
                        break;

                    case 4:
                        Instantiate(_store, _spotSpaunChestHard[i].transform);
                        break;
                }

            }
        }

        yield return null;
    }
}
