using UnityEngine.UI;
using UnityEngine;


public class TutorialTextField : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public void SetText(string text)
    {
        this._text.text = text;        
    }
}
