using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionBlock : MonoBehaviour
{
    [SerializeField]
    GameObject _descriptionBlock;
    [SerializeField]
    Text _name;
    [SerializeField]
    Text _description;
    [SerializeField]
    Text _damage;
    [SerializeField]
    Text _descriptionEffect;
    [SerializeField]
    Text _damageEffect;

    void Start()
    {
        _descriptionBlock.SetActive(false);
        DescriptionSpell.descriptionBlockActiveTrue += DescriptionBlockActiveTrue;
        DescriptionSpell.descriptionBlockActiveFalse += DescriptionBlockActiveFalse;
    }

    private void DescriptionBlockActiveTrue(string name, string description, float damage, string desEffect, float damageEffect)
    {
        _descriptionBlock.SetActive(true);
        _descriptionBlock.transform.position = Input.mousePosition;
        _name.text = name;
        _description.text = description;
        _damage.text = "" + damage;
        _descriptionEffect.text = desEffect;
        _damageEffect.text = "" + damageEffect;
    }
    private void DescriptionBlockActiveFalse()
    {
        _descriptionBlock.SetActive(false);
    }

    private void OnDestroy()
    {
        DescriptionSpell.descriptionBlockActiveTrue -= DescriptionBlockActiveTrue;
        DescriptionSpell.descriptionBlockActiveFalse -= DescriptionBlockActiveFalse;
    }
}
