using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSoul : MonoBehaviour
{
    public TutorialManager TutorialManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TutorialManager.OnEvent(TutorialEvent.CollectSoul);
        }
    }
}
