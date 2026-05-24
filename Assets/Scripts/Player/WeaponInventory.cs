using System;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public const int MAX_WEAPONS = 3;

    [Header("Armas iniciales (opcional)")]
    [SerializeField] private WeaponData[] startingWeapons;

    // Punto de anclaje: un GameObject vacío hijo del jugador
    [Header("Ancla del arma (GameObject vacío hijo del jugador)")]
    [SerializeField] private Transform weaponAnchor;

    private WeaponData[] _slots = new WeaponData[MAX_WEAPONS];
    private int _currentSlot = 0;
    private GameObject _currentWeaponGO;
    private StateManager _state;

    public static event Action<WeaponData[]> OnInventoryChanged;
    public static event Action<WeaponData, int> OnWeaponEquipped;

    public WeaponData CurrentWeapon => _slots[_currentSlot];
    public WeaponData[] Slots => _slots;
    public int CurrentSlot => _currentSlot;

    void Awake()
    {
        _state = GetComponent<StateManager>();
    }

    void Start()
    {
        for (int i = 0; i < startingWeapons.Length && i < MAX_WEAPONS; i++)
            _slots[i] = startingWeapons[i];

        NotifyChange();
    }

    void OnEnable()
    {
        StateManager.OnStateChanged += OnStateChanged;
    }

    void OnDisable()
    {
        StateManager.OnStateChanged -= OnStateChanged;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) CycleSlot(-1);
        if (scroll < 0f) CycleSlot(1);

        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
    }

    public bool TryAddWeapon(WeaponData weapon)
    {
        for (int i = 0; i < MAX_WEAPONS; i++)
        {
            if (_slots[i] == null)
            {
                _slots[i] = weapon;
                NotifyChange();
                return true;
            }
        }
        Debug.Log("Inventario lleno.");
        return false;
    }

    public void SwapWeapon(int slot, WeaponData newWeapon)
    {
        if (slot < 0 || slot >= MAX_WEAPONS) return;
        _slots[slot] = newWeapon;
        NotifyChange();
    }

    public void DropCurrentWeapon()
    {
        _slots[_currentSlot] = null;
        if (_currentWeaponGO != null) Destroy(_currentWeaponGO);
        NotifyChange();
    }

    private void SelectSlot(int slot)
    {
        if (_slots[slot] == null) return;
        _currentSlot = slot;
        EquipCurrentWeapon();
        NotifyChange();
    }

    private void CycleSlot(int dir)
    {
        // Buscar el siguiente slot que no esté vacío
        for (int i = 1; i <= MAX_WEAPONS; i++)
        {
            int next = (_currentSlot + dir * i + MAX_WEAPONS) % MAX_WEAPONS;
            if (_slots[next] != null)
            {
                _currentSlot = next;
                EquipCurrentWeapon();
                NotifyChange();
                return;
            }
        }
    }

    private void EquipCurrentWeapon()
    {
        if (_currentWeaponGO != null) Destroy(_currentWeaponGO);

        WeaponData weapon = _slots[_currentSlot];
        if (weapon == null || weapon.prefab == null) return;

        // Si el arma requiere un estado y no estamos en él, no instanciar
        if (weapon.requiresState && _state.estadoActual != weapon.requiredState)
        {
            Debug.Log($"{weapon.weaponName} requiere estado {weapon.requiredState}");
            return;
        }

        // Sin rig: instanciar en el anchor manual
        if (weaponAnchor == null)
        {
            Debug.LogWarning("WeaponAnchor no asignado en el Inspector.");
            return;
        }

        _currentWeaponGO = Instantiate(weapon.prefab, weaponAnchor);
        _currentWeaponGO.transform.localPosition = Vector3.zero;
        _currentWeaponGO.transform.localRotation = Quaternion.identity;

        OnWeaponEquipped?.Invoke(weapon, _currentSlot);
    }

    private void OnStateChanged(StateManager.PlayerState newState)
    {
        // Revalidar arma equipada al cambiar de estado
        WeaponData current = _slots[_currentSlot];
        if (current == null) return;

        if (current.requiresState && current.requiredState != newState)
        {
            if (_currentWeaponGO != null) _currentWeaponGO.SetActive(false);
        }
        else
        {
            // Si no hay modelo instanciado, intentar equipar ahora
            if (_currentWeaponGO == null)
                EquipCurrentWeapon();
            else
                _currentWeaponGO.SetActive(true);
        }
    }

    private void NotifyChange()
    {
        OnInventoryChanged?.Invoke(_slots);
    }
}