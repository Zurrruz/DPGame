using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour, IPointerClickHandler
{
    public Map map;

    [SerializeField]
    private Sprite[] _edge;
    
    public static int result;
    public int t;

    public static bool thereIsAMove;

    private SpriteRenderer _sr;


    // Start is called before the first frame update
    void Start()
    {
     
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        t = result;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!thereIsAMove)
        {
            StartCoroutine(Result());
            thereIsAMove = true;
        }
    }

    IEnumerator Result()
    {
        result = Random.Range(1, 7);
        for (int i = 0; i < 6; i++)
        {
            _sr.sprite = _edge[i];
            yield return new WaitForSeconds(0.1f);
        }               
        _sr.sprite = _edge[result];
        map.PathMaterials();
    }
}
