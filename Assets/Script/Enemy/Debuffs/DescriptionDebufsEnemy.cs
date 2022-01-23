using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionDebufsEnemy : MonoBehaviour
{
    [SerializeField]
    private string _descriptionFrostbiteDebuf;
    [SerializeField]
    private string _descriptionScorchDebuf;
    [SerializeField]
    private string _descriptionStaticElectricityDebuf;
    [SerializeField]
    private string _descriptionWeakeningDebuf;

    [SerializeField]
    private GameObject _infoDebufBlock;
    [SerializeField]
    private Text _infoText;

    private void Start()
    {
        InfoDebufsEnemy.infoDebufBlockActiveTrue += InfoDebufActiveTrue;
        InfoDebufsEnemy.infoDebufBlockActiveFalse += InfoDebufActiveFalse;
        _infoDebufBlock.SetActive(false);
    }

    private void InfoDebufActiveTrue(bool frost, bool scorch, bool staticElectric, bool weakening)
    {
        _infoDebufBlock.SetActive(true);
        _infoDebufBlock.transform.position = Input.mousePosition;

        if (frost)
        {            
            _infoText.text = _descriptionFrostbiteDebuf;
        }
        else if(scorch)
        {
            _infoText.text = _descriptionScorchDebuf;
        }
        else if(staticElectric)
        {
            _infoText.text = _descriptionStaticElectricityDebuf;
        }
        else if(weakening)
        {
            _infoText.text = _descriptionWeakeningDebuf;
        }
    }

    private void InfoDebufActiveFalse()
    {
        _infoDebufBlock.SetActive(false);
    }

    private void OnDestroy()
    {
        InfoDebufsEnemy.infoDebufBlockActiveTrue -= InfoDebufActiveTrue;
        InfoDebufsEnemy.infoDebufBlockActiveFalse -= InfoDebufActiveFalse;
    }
}
