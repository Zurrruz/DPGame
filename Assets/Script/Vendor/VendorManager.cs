using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _vendorButton;
    [SerializeField]
    private GameObject _exitStore;

    public static List<GameObject> storeItemWeapon;

    public delegate void GoToStore();
    public static event GoToStore goToStore;

    void Start()
    {
        _vendorButton.SetActive(false);
        goToStore += ActivateVendorButton;
    }

    public static void EventVendorButton()
    {
        goToStore();
    }

    private void ActivateVendorButton()
    {
        _vendorButton.SetActive(true);
    }

    public void VendorButton()
    {
        _exitStore.SetActive(true);
        _vendorButton.SetActive(false);
    }
   
    public void ExitStore()
    {
        Dice.thereIsAMove = false;
        _exitStore.SetActive(false);
        MainCameraController.inStore = false;
    }

    private void OnDestroy()
    {
        goToStore -= ActivateVendorButton;
    }
}
