using UnityEngine;

public class CombatManager : MonoBehaviour
{
    Transform weapon;
    void Start()
    {
        
    }

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
