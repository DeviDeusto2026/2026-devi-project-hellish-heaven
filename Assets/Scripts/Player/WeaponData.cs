using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "TypeWeapon/WeaponName")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public Sprite icon;
    public GameObject prefab;

    public float damage;
    public float attackRate;
    public float range;

    public WeaponType weaponType;

    public bool requiresState = false;
    public StateManager.PlayerState requiredState;
}

public enum WeaponType { Lanza, Espada, Daga, Guadana, ArcoCelestial, LatigoSombras }