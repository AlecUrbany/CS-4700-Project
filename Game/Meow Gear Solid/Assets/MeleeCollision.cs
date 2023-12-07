using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 50f;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyHealth enemyScript = other.gameObject.GetComponent<EnemyHealth>();
            if(enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
                Destroy(bullet);
            }
        }
    
    }
}
