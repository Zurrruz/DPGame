using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoDebufsEnemy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool _frostbiteDebuf;
    [SerializeField]
    private bool _scorchDebuf;
    [SerializeField]
    private bool _staticElectricityDebuf;
    [SerializeField]
    private bool _weakeningDebuf;

    public delegate void InfoDebufBlockActiveTrue(bool frost, bool scorch, bool staticElectric, bool weakening);
    public static event InfoDebufBlockActiveTrue infoDebufBlockActiveTrue;
    public delegate void InfoDebufBlockActiveFalse();
    public static event InfoDebufBlockActiveFalse infoDebufBlockActiveFalse;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoDebufBlockActiveTrue(_frostbiteDebuf, _scorchDebuf, _staticElectricityDebuf, _weakeningDebuf);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoDebufBlockActiveFalse();
    }
}
