using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    public WeaponData weaponData;
    public GameObject arrow;

    public float manaCost = 10f;

    private float force;
    private float attackDamage;
    private PlayerMana manaSystem;
    private Transform player;

    private void Start()
    {
        Move moveScript = GetComponentInParent<Move>();
        if (moveScript != null)
            player = moveScript.transform;
        else
            Debug.LogWarning("ArrowShot: no se encontró Move en el padre.");

        manaSystem = GetComponentInParent<PlayerMana>();
        if (manaSystem == null)
            Debug.LogWarning("ArrowShot: no se encontró PlayerMana en el padre.");

        if (weaponData != null)
            LoadWeaponData(weaponData);
    }

    public void InitialiceWeapon(WeaponData data, Transform parentAnchor, PlayerMana manaScript)
    {
        manaSystem = manaScript;
        weaponData = data;
        if (data != null)
            LoadWeaponData(data);
    }

    private void LoadWeaponData(WeaponData data)
    {
        attackDamage = data.damage;
        if (data.range > 0)
            force = data.range;
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

        if (manaSystem != null && !manaSystem.ConsumeMana(manaCost))
            return;

        if (player == null)
        {
            Debug.LogWarning("ArrowShot: jugador es null, no se puede disparar.");
            return;
        }

        GameObject go = Instantiate(arrow, transform.position, transform.rotation);

        go.transform.forward = player.forward;
        go.transform.Rotate(90, 0, 0, Space.Self);

        Arrow scriptFlecha = go.GetComponent<Arrow>();
        if (scriptFlecha != null)
            scriptFlecha.configureDamage(attackDamage);

        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(player.forward * force, ForceMode.Impulse);

        Destroy(go, 2f);
    }
}