using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static string eventText;
    [SerializeField]
    private Text _eventText;
    [SerializeField]
    private GameObject _windowEventText;

    public delegate void StartEvent(string text);
    public static event StartEvent startEvent;


    void Start()
    {
        _windowEventText.SetActive(false);
        startEvent += EventText;
    }

    public static void  RenewText(string text)
    {
        startEvent(text);
    }

    private void EventText(string text)
    {
        _windowEventText.SetActive(true);
        _eventText.text = text;
    }

   
}
