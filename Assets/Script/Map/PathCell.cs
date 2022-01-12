using UnityEngine;
using UnityEngine.EventSystems;

public class PathCell : MonoBehaviour, IPointerClickHandler
{
    public bool playerHerePassed;
    public bool playerHere;
    public bool selectedCell;
    [SerializeField]
    private bool _leftPath;
    public bool _lastHighlighted—ell;

    [SerializeField]
    private ParticleSystem _ps;
    private SpriteRenderer _spriteRenderer;

    public bool thereIsAChest;
    public bool thereIsAEnemy;

    public bool theBoss;
    public int numberPath;
    public bool thereIsABox;

    public GameManager gm;
    public Map map;

    public delegate void PlayerOnTheChest();
    public static event PlayerOnTheChest playerOnTheChest;
    public delegate void FightInTheArena();
    public static event FightInTheArena fightInTheArena;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {       
        _lastHighlighted—ell = false;
        playerHerePassed = false;
        selectedCell = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_ps != null)
            _ps.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (thereIsAChest)
        {
            playerOnTheChest();
        }
        if(thereIsAEnemy)
        {
            fightInTheArena();
        }
        playerHerePassed = true;
        playerHere = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerHere = false;
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

    public void ChangeSprite(Sprite sp)
    {
        _spriteRenderer.sprite = sp;
    }
    public void StartPS()
    {
        if (_ps != null)
            _ps.Play();
    }
    public void StopPS()
    {
        if (_ps != null)
            _ps.Stop();
    }

}
