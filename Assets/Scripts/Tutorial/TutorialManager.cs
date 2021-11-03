using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialManager : MonoBehaviour
{
    public TutorialScript[] tutorialScripts;

    [Header("Prefabs")]
    [SerializeField]
    private TutorialTextField _tutorialTextFieldPrefabs;
    [SerializeField]
    private GameObject _tutorialHitGOPrefab;
    [SerializeField]
    private GameObject _tutorialHitOnUIPrefab;

    private TutorialScript _currentScript;
    private int _currentStep;
    private float _lockTimer;

    private bool IsActive => _currentScript != null;
    private TutorialStep CurrentStep => _currentScript.steps[_currentStep];
    private TutorialStep NextStep => _currentScript.steps[_currentStep + 1];
    private bool HasNextStep => _currentScript.steps.Length > _currentStep + 1;
    private bool IsLocked => _lockTimer > 0;

    
    private TutorialTextField _tutorialTextField;
    private GameObject _tutorialHitGO;
    private GameObject _tutorialHitOnUI;

    private void StartTutorial(TutorialEvent @event)
    {
        foreach (var scripts in tutorialScripts)
        {
            if(scripts.startTrigger == @event)
            {
                _currentScript = scripts;
                _currentStep = 0;
                ProcessCurrentStep();
                break;
            }
        }
    }    

    private void FinishTutorial()
    {
        _currentScript = null;
        _currentStep = 0;
    }

    private void ProcessEvent(TutorialEvent @event)
    {
        if(NextStep.startTrigger == @event)
        {
            PlayNextStep();
        }
        if(!HasNextStep)
        {
            FinishTutorial();
        }
    }

    private void PlayNextStep()
    {
        _currentStep++;
        ProcessCurrentStep();
    }

    private void ProcessCurrentStep()
    {
        switch (CurrentStep.action)
        {
            case TutorialAction.ShowText:
                ShowText(CurrentStep.data);
                break;
            case TutorialAction.HintOnUI:
                ShowHitOnUI(CurrentStep.data);
                break;
            case TutorialAction.HintOnGameObject:
                ShowHitOnGo(CurrentStep.data);
                break;
            case TutorialAction.Clear:
                Clear();
                break;
            case TutorialAction.wait:
                Wait(float.Parse(CurrentStep.data));
                break;
        }
    }

    private void Wait(float time)
    {
        _lockTimer = time;
    }

    private void Clear()
    {
        if (_tutorialTextField != null)
        {
            Destroy(_tutorialTextField.gameObject);
        }
        Destroy(_tutorialHitGO);
        Destroy(_tutorialHitOnUI);
    }

    private void ShowHitOnGo(string data)
    {
        var go = GameObject.Find(data);
        if(go == null)
        {
            Debug.LogError("Game Object not Found");
            return;
        }

        if( _tutorialHitGO == null)
        {
            _tutorialHitGO = Instantiate(_tutorialHitGOPrefab);
        }

        _tutorialHitGO.transform.SetParent(go.transform, false);
    }

    private void ShowHitOnUI(string data)
    {
        var go = GameObject.Find(data);
        if (go == null)
        {
            Debug.LogError("Game Object not Found");
            return;
        }

        if (_tutorialHitOnUI == null)
        {
            _tutorialHitOnUI = Instantiate(_tutorialHitOnUIPrefab);
        }

        _tutorialHitOnUI.transform.SetParent(go.transform, false);
    }

    private void ShowText(string data)
    {
        if(_tutorialTextField == null)
        {
            _tutorialTextField = Instantiate(_tutorialTextFieldPrefabs);            
        }               
    }

    int lang;

    private void Update()
    {
        if(IsLocked)
        {
            _lockTimer -= Time.deltaTime;
        }
        OnEvent(TutorialEvent.Update);

        lang = PlayerPrefs.GetInt("lang");

    }

    public void OnEvent(TutorialEvent @event)
    {
        if(IsLocked)
        {
            return;
        }

        if(IsActive)
        {
            ProcessEvent(@event);
        }
        else
        {
            StartTutorial(@event);
        }
    }
}
