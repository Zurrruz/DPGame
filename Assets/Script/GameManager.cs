using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Map map;

    [SerializeField]
    private float _speed;
    private int _spot;

    public static bool isMove;

    [SerializeField]
    private List<Transform> _moveSpots;
    [SerializeField]
    private List<Transform> _oneMoveSpots;
    [SerializeField]
    private List<Transform> _twooMoveSpots;
    [SerializeField]
    private List<Transform> _threeMoveSpots;
    [SerializeField]
    private List<Transform> _forMoveSpots;

    public Transform _actualSpot;

    private bool _addOnePath;
    private bool _addTwooPath;
    private bool _addThreePath;
    private bool _addForPath;


    [SerializeField]
    private GameObject _player; 
     
    private void Start()
    {
        isMove = false;
        _spot = 0;
        _addOnePath = true;
        _addTwooPath = true;
        _addThreePath = true;
        _addForPath = true;
    }

    void Update()
    {
        Path();
        Move();
    }    

    private void Move()
    {       
        if (isMove && Dice.result > 0)
        {
            map.ResetMatireals();
            _player.transform.position = Vector2.MoveTowards(_player.transform.position, _actualSpot.position, _speed * Time.deltaTime);
            if (Vector2.Distance(_player.transform.position, _actualSpot.position) < 0.1f)
            {
                if(_spot +1 < _moveSpots.Count)
                {
                    _spot++;
                }
            }
        }      

        if (Dice.result <= 0)
            isMove = false;
    }   

    private void Path()
    {
        _actualSpot = _moveSpots[_spot];
    }

    public void AddPath(int n)
    {
        switch (n)
        {
            case 1:
                if (_addOnePath)
                {
                    foreach (var path in _oneMoveSpots)
                    {
                        _moveSpots.Add(path);
                    }
                    _addOnePath = false;
                }                  
                break;
            case 2:
                if (_addTwooPath)
                {
                    foreach (var path in _twooMoveSpots)
                    {
                        _moveSpots.Add(path);
                    }
                    _addTwooPath = false;
                }
                break;
            case 3:
                if (_addThreePath)
                {
                    foreach (var path in _threeMoveSpots)
                    {
                        _moveSpots.Add(path);
                    }
                    _addThreePath = false;
                }
                break;
            case 4:
                if (_addForPath)
                {
                    foreach (var path in _forMoveSpots)
                    {
                        _moveSpots.Add(path);
                    }
                    _addForPath = false;
                }
                break;
        }    
    }
}
