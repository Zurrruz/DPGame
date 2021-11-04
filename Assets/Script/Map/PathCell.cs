using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathCell : MonoBehaviour, IPointerClickHandler
{
    public bool playerHere;
    public bool selectedCell;
    [SerializeField]
    private bool _leftPath;
    public bool _lastHighlighted—ell;

    public bool theBoss;
    
    public int numberPath;


    public bool thereIsABox;

    public GameManager gm;
    public Map map;

    private void Start()
    {
        _lastHighlighted—ell = false;
        playerHere = false;
        selectedCell = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHere = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(_lastHighlighted—ell)
        {
            GameManager.isMove = true;
            
            if(!theBoss)
            {
                gm.AddPath(numberPath);
                map.AddMoveSpots(numberPath);
            }
        }
    }
}
