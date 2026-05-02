using UnityEngine;
using System.Collections;

public class SwordAttack: MonoBehaviour
{
    public Transform padre;
    public float duracionTotal = 1.0f;
    private bool estaRotando = false;
    private float attackDamage = 20f;
    private PlayerMana sistemaMana;

    private void Start()
    {
        sistemaMana = GetComponentInParent<PlayerMana>();
    }


    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !estaRotando)
        {
            if (sistemaMana != null && sistemaMana.ConsumirMana(10))
            {
                StartCoroutine(GiroMitadYVuelta());
            }
        }

    }

    IEnumerator GiroMitadYVuelta()
    {
        estaRotando = true;

        float gradosObjetivo = 90f;
        float tiempoFase = duracionTotal / 2f;

        yield return MoverRotacion(gradosObjetivo, tiempoFase);

        yield return MoverRotacion(-gradosObjetivo, tiempoFase);

        estaRotando = false;
    }

    IEnumerator MoverRotacion(float grados, float tiempo)
    {
        float gradosInvertidos = 0f;
        float velocidad = grados / tiempo;

        while (Mathf.Abs(gradosInvertidos) < Mathf.Abs(grados))
        {
            float paso = velocidad * Time.deltaTime;

            if (Mathf.Abs(gradosInvertidos + paso) > Mathf.Abs(grados))
            {
                paso = grados - gradosInvertidos;
            }

            transform.RotateAround(padre.position, Vector3.up, paso);
            gradosInvertidos += paso;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (estaRotando && other.CompareTag("Enemigo"))
        {
            EnemyHealth saludEnemigo = other.GetComponent<EnemyHealth>();


            if (saludEnemigo != null)
            {
                saludEnemigo.RecibirDanyo(attackDamage);
            }
        }
    }
}