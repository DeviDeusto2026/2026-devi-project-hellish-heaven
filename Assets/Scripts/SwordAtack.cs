using UnityEngine;
using System.Collections;

public class RotacionIdaYVuelta : MonoBehaviour
{
    public Transform padre;
    public float duracionTotal = 1.0f;
    private bool estaRotando = false;

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
            Destroy(other.gameObject);
            Debug.Log("¡Enemigo derrotado!");
        }
    }
}