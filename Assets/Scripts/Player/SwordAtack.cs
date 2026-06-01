using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordAttack: MonoBehaviour
{
    public WeaponData weaponData;

    public Transform parent;

    private float totalDuration;
    private bool isRotating = false;
    private float attackDamage;

    private List<GameObject> hitEnemies = new List<GameObject>();

    private void Start()
    {
        if (weaponData != null)
        {
            LoadWeaponData(weaponData);
        }
    }


    private void LoadWeaponData(WeaponData data)
    {
        attackDamage = data.damage;

        if (data.attackRate > 0)
        {
            totalDuration = 1f / data.attackRate;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isRotating)
        {
            StartCoroutine(SlashRotation());
        }
    }

    IEnumerator SlashRotation()
    {
        isRotating = true;
        hitEnemies.Clear();

        float objectiveRotation = 90f;
        float faseTime = totalDuration / 2f;

        yield return MoveRotation(objectiveRotation, faseTime);
        yield return MoveRotation(-objectiveRotation, faseTime);

        isRotating = false;
        hitEnemies.Clear();
    }

    IEnumerator MoveRotation(float grades, float time)
    {
        float inverdedGrades = 0f;
        float speed = grades / time;

        while (Mathf.Abs(inverdedGrades) < Mathf.Abs(grades))
        {
            float paso = speed * Time.deltaTime;

            if (Mathf.Abs(inverdedGrades + paso) > Mathf.Abs(grades))
            {
                paso = grades - inverdedGrades;
            }

            transform.RotateAround(parent.position, Vector3.up, paso);
            inverdedGrades += paso;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRotating && other.CompareTag("Enemigo") && !hitEnemies.Contains(other.gameObject))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.ReceiveDamage(attackDamage);
                hitEnemies.Add(other.gameObject);
            }
        }
    }
}