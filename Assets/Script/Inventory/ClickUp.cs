using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickUp : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public EquipmentData eq;
    [SerializeField]
    private GameObject _item;

    public WeaponData wD;
    [SerializeField]
    private GameObject _weapon;

    [SerializeField]
    private Text _infoName;
    [SerializeField]
    private Text _infoStrangth;
    [SerializeField]
    private Text _infoAgility;
    [SerializeField]
    private Text _infoIntellect;
    [SerializeField]
    private Text _infoPhysicsDamage;
    [SerializeField]
    private Text _infoMagicDamage;

    private float _i;
    private float _a;
    private float _s;

    [SerializeField]
    private bool item;
    [SerializeField]
    private bool weapon;

    public bool _onPointer;
    public bool _onEnter;    

    public delegate void SomeAction(bool glovers, bool boots, bool helm, bool chest, float strenght, float agility, float intellect, GameObject item);
    public delegate void UpWeapon(float pD, float mD, float st, float ag, float intel, GameObject weapon);
    public static event SomeAction clicUpItem;
    public static event UpWeapon clicUpWeapon;

    private void Awake()
    {
        if(item)
            RandomStats();
    }

    private void Start()
    {
        ChooseNotChoose.choose += NotChoose;
        OpenBox.take += AddItemWeapon;
        _onPointer = false;

        if (item)
        {
            _infoName.text = eq.nameItem;
            _infoStrangth.text = "Сила " + _s;
            _infoAgility.text = "Ловкость " + _a;
            _infoIntellect.text = "Интеллект " + _i;
        }
        else if (weapon)
        {
            _infoName.text = wD.weaponName;
            _infoStrangth.text = "Сила " + wD.strength;
            _infoAgility.text = "Ловкость " + wD.agility;
            _infoIntellect.text = "Интеллект " + wD.intellect;
            _infoPhysicsDamage.text = "Физический урон " + wD.physicsDamage;
            _infoMagicDamage.text = "Магический урон " + wD.magicDamage;
        }
        if (item)
        {
            transform.GetChild(1).GetComponent<EquipmentItems>().intellect = _i;
            transform.GetChild(1).GetComponent<EquipmentItems>().agility = _a;
            transform.GetChild(1).GetComponent<EquipmentItems>().strength = _s;
            transform.GetChild(1).GetComponent<EquipmentItems>()._name = eq.nameItem;
            transform.GetChild(1).GetComponent<EquipmentItems>().helm = eq.helm;
            transform.GetChild(1).GetComponent<EquipmentItems>().glovers = eq.glovers;
            transform.GetChild(1).GetComponent<EquipmentItems>().chest = eq.chest;
            transform.GetChild(1).GetComponent<EquipmentItems>().boots = eq.boots;
        }
    }

    private void RandomStats()
    {
        _i = Mathf.Round(Random.Range(eq.minIntellect, eq.maxIntellect));
        _a = Mathf.Round(Random.Range(eq.minAgility, eq.maxAgility));
        _s = Mathf.Round(Random.Range(eq.minStrength, eq.maxStrength));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Сhoose();
        _onPointer = true;
        ChooseNotChoose.NotChoose();
    }

    private void Сhoose()
    {
        transform.GetChild(0).GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }    
    private void NotChoose()
    {
        if (!_onEnter)
        {
            _onPointer = false;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void AddItemWeapon()
    {
        if (_onPointer)
        {
            if (item)
            {
                clicUpItem(eq.glovers, eq.boots, eq.helm, eq.chest, _s, _a, _i, _item);                
            }
            else if (weapon)
            {
                clicUpWeapon(wD.physicsDamage, wD.magicDamage, wD.strength, wD.agility, wD.intellect, _weapon);
            }
        }
        DestroyItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _onEnter = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _onEnter = false;
    }

    private void DestroyItem()
    {
        ChooseNotChoose.choose -= NotChoose;
        OpenBox.take -= AddItemWeapon;
        Destroy(gameObject, 0.2f);
    }
}
