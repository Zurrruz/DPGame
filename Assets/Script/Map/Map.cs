using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Material _pathMaterial;
    [SerializeField]
    private Material _defaultPathMaterial;

    [SerializeField]
    private List<GameObject> _MoveSpots;
    [SerializeField]
    private List<GameObject> _oneMoveSpots;
    [SerializeField]
    private List<GameObject> _twooMoveSpots;
    [SerializeField]
    private List<GameObject> _threeMoveSpots;
    [SerializeField]
    private List<GameObject> _forMoveSpots;

    public List<GameObject> _notTraveledPath;
    public List<GameObject> _notTraveledPathLeft;
    public List<GameObject> _notTraveledPathRight;

    int countOfPath;

    private bool _addOnePath;
    private bool _addTwooPath;
    private bool _addThreePath;
    private bool _addForPath;


    void Start()
    {
        _addOnePath = true;
        _addTwooPath = true;
        _addThreePath = true;
        _addForPath = true;
    }
    
    void Update()
    {
        

    }

    public void PathMaterials()
    {
        StartCoroutine(Er());        
    }

    public void ResetMatireals()
    {
        foreach (var path in _notTraveledPathLeft)
        {
            path.GetComponent<SpriteRenderer>().material = _defaultPathMaterial;
            path.GetComponent<PathCell>()._lastHighlightedСell = false;
        }
        foreach (var path in _notTraveledPathRight)
        {
            path.GetComponent<SpriteRenderer>().material = _defaultPathMaterial;
            path.GetComponent<PathCell>()._lastHighlightedСell = false;
        }
        foreach (var path in _MoveSpots)
        {
            path.GetComponent<SpriteRenderer>().material = _defaultPathMaterial;
            path.GetComponent<PathCell>()._lastHighlightedСell = false;
        }
    }

    private void ClearLastCall()
    {
        foreach (var path in _notTraveledPath)
        {
            path.GetComponent<PathCell>()._lastHighlightedСell = false;
        }
        foreach (var path in _notTraveledPathLeft)
        {
            path.GetComponent<PathCell>()._lastHighlightedСell = false;
        }
        foreach (var path in _notTraveledPathRight)
        {
            path.GetComponent<PathCell>()._lastHighlightedСell = false;
        }

    }

    public void AddMoveSpots(int p)
    {
        switch(p)
        {
            case 1:
                if (!_addOnePath) return;
                {
                    foreach (var path in _oneMoveSpots)
                    {
                        _MoveSpots.Add(path);
                    }
                    _addOnePath = false;
                }
                break;
            case 2:
                if (!_addTwooPath) return;
                {
                    foreach (var path in _twooMoveSpots)
                    {
                        _MoveSpots.Add(path);
                    }
                    _addTwooPath = false;
                }
                break;
            case 3:
                if (!_addThreePath) return;
                {
                    foreach (var path in _threeMoveSpots)
                    {
                        _MoveSpots.Add(path);
                    }
                    _addThreePath = false;
                }
                break;
            case 4:
                if (!_addForPath) return;
                {
                    foreach (var path in _forMoveSpots)
                    {
                        _MoveSpots.Add(path);
                    }
                    _addForPath = false;
                }
                break;
        }    
    }

    IEnumerator Er()
    {
        countOfPath = Dice.result;
        ClearLastCall();
        ResetMatireals();
        _notTraveledPath.Clear();
        _notTraveledPathLeft.Clear();
        _notTraveledPathRight.Clear();

        foreach (var path in _MoveSpots)
        {
            if (!path.GetComponent<PathCell>().playerHere)
            {
                _notTraveledPath.Add(path);
            }
        }

        if (countOfPath > _notTraveledPath.Count)
        {
            var difference = countOfPath - _notTraveledPath.Count;

            for (int i = 0; i < _notTraveledPath.Count; i++)
            {
                _notTraveledPath[i].GetComponent<SpriteRenderer>().material = _pathMaterial;
                _notTraveledPath[i].GetComponent<PathCell>().selectedCell = true;
            }
          
            switch (_MoveSpots[_MoveSpots.Count - 1].GetComponent<PathCell>().numberPath)
            {
                case 0:
                    foreach (var p in _oneMoveSpots)
                    {
                        _notTraveledPathLeft.Add(p);
                    }
                    foreach (var p in _twooMoveSpots)
                    {
                        _notTraveledPathRight.Add(p);
                    } 
                    break;

                case 1:
                    _notTraveledPath[_notTraveledPath.Count - 1].GetComponent<PathCell>()._lastHighlightedСell = true;
                    break;

                case 2:
                    foreach (var p in _threeMoveSpots)
                    {
                        _notTraveledPathLeft.Add(p);
                    }
                    foreach (var p in _forMoveSpots)
                    {
                        _notTraveledPathRight.Add(p);
                    }
                    break;

                case 3:
                    _notTraveledPath[_notTraveledPath.Count - 1].GetComponent<PathCell>()._lastHighlightedСell = true;
                    break;
                case 4:
                    _notTraveledPath[_notTraveledPath.Count - 1].GetComponent<PathCell>()._lastHighlightedСell = true;
                    break;
            }
            for (int i = 0; i < difference; i++)
            {
                _notTraveledPathLeft[i].GetComponent<SpriteRenderer>().material = _pathMaterial;
                _notTraveledPathRight[i].GetComponent<SpriteRenderer>().material = _pathMaterial;
                _notTraveledPathLeft[i].GetComponent<PathCell>().selectedCell = true;
                _notTraveledPathRight[i].GetComponent<PathCell>().selectedCell = true;
                _notTraveledPathLeft[difference - 1].GetComponent<PathCell>()._lastHighlightedСell = true;
                _notTraveledPathRight[difference - 1].GetComponent<PathCell>()._lastHighlightedСell = true;
            }
            for (int i = difference; i < _notTraveledPathLeft.Count; i++)
            {
                _notTraveledPathLeft[i].GetComponent<SpriteRenderer>().material = _defaultPathMaterial;
                _notTraveledPathLeft[i].GetComponent<PathCell>().selectedCell = false;
            }
            for (int i = difference; i < _notTraveledPathRight.Count; i++)
            {                
                _notTraveledPathRight[i].GetComponent<SpriteRenderer>().material = _defaultPathMaterial;
                _notTraveledPathRight[i].GetComponent<PathCell>().selectedCell = false;
            }
        }

        else
        {
            for (int i = 0; i < countOfPath; i++)
            {
                _notTraveledPath[i].GetComponent<SpriteRenderer>().material = _pathMaterial;
                _notTraveledPath[i].GetComponent<PathCell>().selectedCell = true;
                _notTraveledPath[countOfPath - 1].GetComponent<PathCell>()._lastHighlightedСell = true;
            }
            for (int i = countOfPath; i < _notTraveledPath.Count; i++)
            {
                _notTraveledPath[i].GetComponent<SpriteRenderer>().material = _defaultPathMaterial;
                _notTraveledPath[i].GetComponent<PathCell>().selectedCell = false;                
            }
        }

        yield return null;
    }    
}
