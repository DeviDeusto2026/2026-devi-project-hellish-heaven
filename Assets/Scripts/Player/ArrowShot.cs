using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    public WeaponData datosArma;
    public GameObject arrow;

    public float costeMana = 10f;

    private float force = 10f;
    private float attackDamage;
    private PlayerMana sistemaMana;
    private Transform jugador;

    private void Start()
    {
        Move moveScript = GetComponentInParent<Move>();
        if (moveScript != null)
            jugador = moveScript.transform;
        else
            Debug.LogWarning("ArrowShot: no se encontró Move en el padre.");

        sistemaMana = GetComponentInParent<PlayerMana>();
        if (sistemaMana == null)
            Debug.LogWarning("ArrowShot: no se encontró PlayerMana en el padre.");

        if (datosArma != null)
            CargarDatosDeScriptableObject(datosArma);
    }

    public void InicializarArma(WeaponData datos, Transform anchorPadre, PlayerMana manaScript)
    {
        sistemaMana = manaScript;
        datosArma = datos;
        if (datos != null)
            CargarDatosDeScriptableObject(datos);
    }

    private void CargarDatosDeScriptableObject(WeaponData datos)
    {
        attackDamage = datos.damage;
        if (datos.range > 0)
            force = datos.range;
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

        // Comprobar mana antes de disparar
        if (sistemaMana != null && !sistemaMana.ConsumirMana(costeMana))
            return; // Sin mana, no dispara

        if (jugador == null)
        {
            Debug.LogWarning("ArrowShot: jugador es null, no se puede disparar.");
            return;
        }

        // Instanciar la flecha en la posición del anchor
        GameObject go = Instantiate(arrow, transform.position, Quaternion.identity);

        // Orientar la flecha hacia donde mira el jugador
        go.transform.forward = jugador.forward;

        Arrow scriptFlecha = go.GetComponent<Arrow>();
        if (scriptFlecha != null)
            scriptFlecha.ConfigurarDanyo(attackDamage);

        Rigidbody rb = go.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(jugador.forward * force, ForceMode.Impulse);

        Destroy(go, 3f);
    }
}