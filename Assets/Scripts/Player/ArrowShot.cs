using UnityEngine;

public class ArrowShot : MonoBehaviour
{
    public WeaponData datosArma;
    public GameObject arrow;
    private float force = 10f;

    private float attackDamage;
    private PlayerMana sistemaMana;

    private void Start()
    {
        sistemaMana = GetComponentInParent<PlayerMana>();

        if (datosArma != null)
        {
            CargarDatosDeScriptableObject(datosArma);
        }
    }

    public void InicializarArma(WeaponData datos, Transform anchorPadre, PlayerMana manaScript)
    {
        sistemaMana = manaScript;
        datosArma = datos;

        if (datos != null)
        {
            CargarDatosDeScriptableObject(datos);
        }
    }

    private void CargarDatosDeScriptableObject(WeaponData datos)
    {
        attackDamage = datos.damage;

        if (datos.range > 0)
        {
            force = datos.range;
        }
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject go = Instantiate(arrow, transform.position, transform.rotation);
            go.transform.Rotate(0, 0, 90, Space.Self);

            Arrow scriptFlecha = go.GetComponent<Arrow>();
            if (scriptFlecha != null)
            {
                scriptFlecha.ConfigurarDanyo(attackDamage);
            }
            
            go.GetComponent<Rigidbody>().AddForce(Vector3.right * force, ForceMode.Impulse);

            Destroy(go, 3f);
        }
    }
}
