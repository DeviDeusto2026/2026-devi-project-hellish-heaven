using UnityEngine;

public class CombatManager : MonoBehaviour
{
    Transform weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.transform.position += Vector3.right;
        }
    }
}
