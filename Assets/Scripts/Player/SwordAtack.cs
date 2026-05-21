using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordAttack: MonoBehaviour
{
    public Transform padre;
    public float duracionTotal = 1.0f;
    private bool estaRotando = false;
    private float attackDamage = 20f;
    private PlayerMana sistemaMana;

    private List<GameObject> enemigosGolpeados = new List<GameObject>();

    private void Start()
    {
        sistemaMana = GetComponentInParent<PlayerMana>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !estaRotando)
        {
            StartCoroutine(GiroMitadYVuelta());
        }
    }

    IEnumerator GiroMitadYVuelta()
    {
        estaRotando = true;
        enemigosGolpeados.Clear();

        float gradosObjetivo = 90f;
        float tiempoFase = duracionTotal / 2f;

        yield return MoverRotacion(gradosObjetivo, tiempoFase);
        yield return MoverRotacion(-gradosObjetivo, tiempoFase);

        estaRotando = false;
        enemigosGolpeados.Clear();
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
        if (estaRotando && other.CompareTag("Enemigo") && !enemigosGolpeados.Contains(other.gameObject))
        {
            EnemyHealth saludEnemigo = other.GetComponent<EnemyHealth>();

            if (saludEnemigo != null)
            {
                saludEnemigo.RecibirDanyo(attackDamage);
                enemigosGolpeados.Add(other.gameObject);
            }
        }
    }
}