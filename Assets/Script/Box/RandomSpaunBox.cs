using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpaunBox : MonoBehaviour
{
    [SerializeField]
    private int _countSpaunBox;
    [SerializeField]
    private Transform[] _spotSpaunBox;
    [SerializeField]
    private GameObject _box;
    
    private int _chance;

    public void SpaunBox()
    {
        while(_countSpaunBox > 0)
        {
            foreach (var sp in _spotSpaunBox)
            {
                _chance = Random.Range(0, 10);
                if(!sp.GetComponent<PathCell>().thereIsABox)
                {
                    if (_chance > 5)
                    {
                        Instantiate(_box, sp.position, Quaternion.identity);
                        _countSpaunBox--;
                    }
                }
            }
        }
    }
}
