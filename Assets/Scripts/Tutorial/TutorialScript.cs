using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Tutorial.asset", menuName ="Tutorial/Create Tutorial Script")]
public class TutorialScript : ScriptableObject
{
    public TutorialEvent startTrigger;
    public TutorialStep[] steps;
}

[System.Serializable]
public class TutorialStep
{
    public TutorialEvent startTrigger;
    public TutorialAction action;    
    public string data;
}

[System.Serializable]
public enum TutorialEvent
{
    Update,
    GameStart,
    PlayerMove,
    CollectSoul,
    TriggerBlock,
    RightPlatform,
    World,
    Up
}

[System.Serializable]
public enum TutorialAction
{
    ShowText,
    HintOnUI,
    HintOnGameObject,
    Clear,
    wait
}
