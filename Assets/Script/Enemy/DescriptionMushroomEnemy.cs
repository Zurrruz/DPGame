using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionMushroomEnemy : MonoBehaviour
{
    [SerializeField]
    private string _descriptionWarior;
    [SerializeField]
    private string _descriptionMage;
    [SerializeField]
    private string _descriptionShaman;

    [SerializeField]
    private GameObject _infoBlock;
    [SerializeField]
    private Text _infoText;

    private void Start()
    {
        _infoBlock.SetActive(false);
        DescriptionMushroomEnemyBlock.descriptionBlockTrue += InfoEnemy;
        DescriptionMushroomEnemyBlock.descriptionBlockFalse += InfoEnemyActiveFalse;
    }
    private void OnDestroy()
    {
        DescriptionMushroomEnemyBlock.descriptionBlockTrue -= InfoEnemy;
        DescriptionMushroomEnemyBlock.descriptionBlockFalse -= InfoEnemyActiveFalse;
    }

    private void InfoEnemy(bool warior, bool mage, bool shaman)
    {
        _infoBlock.SetActive(true);
        _infoBlock.transform.position = Input.mousePosition;
        if (warior)
            _infoText.text = _descriptionWarior;
        else if (mage)
            _infoText.text = _descriptionMage;
        else if (shaman)
            _infoText.text = _descriptionShaman;
    }

    private void InfoEnemyActiveFalse()
    {
        _infoBlock.SetActive(false);
    }
}
