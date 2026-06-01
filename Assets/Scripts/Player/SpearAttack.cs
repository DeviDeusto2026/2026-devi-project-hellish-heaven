using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpearAttack : MonoBehaviour
{
    public WeaponData weaponData;

    public Transform parent;

    private float totalDuration;
    private bool isAttacking = false;
    private float attackDamage;

    private List<GameObject> hitEnemies = new List<GameObject>();

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;

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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(backForwardAttack());
        }
    }

    IEnumerator backForwardAttack()
    {
        isAttacking = true;
        hitEnemies.Clear();

        float objectiveDistance = (weaponData != null) ? weaponData.range : 2f;
        float faseDuration = totalDuration / 2f;

        yield return MoveWeapon(objectiveDistance, faseDuration);

        yield return MoveWeapon(-objectiveDistance, faseDuration);

        transform.localPosition = initialPosition;

        isAttacking = false;
        hitEnemies.Clear();
    }

    IEnumerator MoveWeapon(float distance, float time)
    {
        float travelledDistance = 0f;
        float speed = distance / time;

        while (Mathf.Abs(travelledDistance) < Mathf.Abs(distance))
        {
            float paso = speed * Time.deltaTime;

            if (Mathf.Abs(travelledDistance + paso) > Mathf.Abs(distance))
            {
                paso = distance - travelledDistance;
            }

            transform.Translate(Vector3.up * paso, Space.Self);

            travelledDistance += paso;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking && other.CompareTag("Enemigo") && !hitEnemies.Contains(other.gameObject))
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