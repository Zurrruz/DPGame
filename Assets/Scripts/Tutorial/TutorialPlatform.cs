using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlatform : MonoBehaviour
{
    public TutorialManager TutorialManager;

    public bool left;
    public bool right;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TutorialManager.OnEvent(TutorialEvent.Up);
        if (left)
            TutorialManager.OnEvent(TutorialEvent.CollectSoul);
        
        if(right)
            TutorialManager.OnEvent(TutorialEvent.RightPlatform);

        if (collision.CompareTag("TriggerBlock"))
        {
            TutorialManager.OnEvent(TutorialEvent.TriggerBlock);
        }
    }
}
