using UnityEngine;
using UnityEngine.UI;

public class WeaponHUD : MonoBehaviour
{
    [Header("Iconos de los 3 slots")]
    public Image slotIcon1;
    public Image slotIcon2;
    public Image slotIcon3;

    [Header("Frames de los 3 slots (para resaltar el activo)")]
    public Image slotFrame1;
    public Image slotFrame2;
    public Image slotFrame3;

    [Header("Sprite cuando el slot está vacío")]
    public Sprite spriteVacio;

    [Header("Colores")]
    public Color colorSeleccionado = Color.white;
    public Color colorNoSeleccionado = new Color(0.4f, 0.4f, 0.4f, 1f);

    private Image[] _icons;
    private Image[] _frames;

    void Awake()
    {
        _icons = new Image[] { slotIcon1, slotIcon2, slotIcon3 };
        _frames = new Image[] { slotFrame1, slotFrame2, slotFrame3 };
    }
    void OnEnable()
    {
        WeaponInventory.OnInventoryChanged += RefrescarSlots;
        WeaponInventory.OnWeaponEquipped += ResaltarSlot;
    }

    void OnDisable()
    {
        WeaponInventory.OnInventoryChanged -= RefrescarSlots;
        WeaponInventory.OnWeaponEquipped -= ResaltarSlot;
    }

    private void RefrescarSlots(WeaponData[] slots)
    {
        for (int i = 0; i < 3; i++)
        {
            if (_icons[i] == null) continue;

            bool tieneArma = slots[i] != null && slots[i].icon != null;
            _icons[i].sprite = tieneArma ? slots[i].icon : spriteVacio;
        }
    }

    private void ResaltarSlot(WeaponData weapon, int slotActivo)
    {
        for (int i = 0; i < 3; i++)
        {
            if (_frames[i] == null) continue;
            _frames[i].color = i == slotActivo ? colorSeleccionado : colorNoSeleccionado;
        }
    }
}


    