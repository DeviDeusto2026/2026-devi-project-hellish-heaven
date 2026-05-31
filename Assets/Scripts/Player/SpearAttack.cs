using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpearAttack : MonoBehaviour
{
    public WeaponData datosArma;

    public Transform padre;

    public float duracionTotal = 1.0f;
    private bool estaAtacando = false;
    private float attackDamage;
    private PlayerMana sistemaMana;

    private List<GameObject> enemigosGolpeados = new List<GameObject>();

    private Vector3 posicionInicialLocal;

    private void Start()
    {
        sistemaMana = GetComponentInParent<PlayerMana>();
        posicionInicialLocal = transform.localPosition;

        if (datosArma != null)
        {
            CargarDatosDeScriptableObject(datosArma);
        }
    }

    private void CargarDatosDeScriptableObject(WeaponData datos)
    {
        attackDamage = datos.damage;

        if (datos.attackRate > 0)
        {
            duracionTotal = 1f / datos.attackRate;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !estaAtacando)
        {
            StartCoroutine(EstocadaAlanteYAtras());
        }
    }

    IEnumerator EstocadaAlanteYAtras()
    {
        estaAtacando = true;
        enemigosGolpeados.Clear();

        float distanciaObjetivo = (datosArma != null) ? datosArma.range : 2f;
        float tiempoFase = duracionTotal / 2f;

        yield return MoverEstocada(distanciaObjetivo, tiempoFase);

        yield return MoverEstocada(-distanciaObjetivo, tiempoFase);

        transform.localPosition = posicionInicialLocal;

        estaAtacando = false;
        enemigosGolpeados.Clear();
    }

    IEnumerator MoverEstocada(float distancia, float tiempo)
    {
        float distanciaRecorrida = 0f;
        float velocidad = distancia / tiempo;

        while (Mathf.Abs(distanciaRecorrida) < Mathf.Abs(distancia))
        {
            float paso = velocidad * Time.deltaTime;

            if (Mathf.Abs(distanciaRecorrida + paso) > Mathf.Abs(distancia))
            {
                paso = distancia - distanciaRecorrida;
            }

            transform.Translate(Vector3.up * paso, Space.Self);

            distanciaRecorrida += paso;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (estaAtacando && other.CompareTag("Enemigo") && !enemigosGolpeados.Contains(other.gameObject))
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