
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 50f;
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(bullet);
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            EnemyHealth enemyScript = other.gameObject.GetComponent<EnemyHealth>();
            if(enemyScript != null){
                enemyScript.TakeDamage(damage);
            }
        }
    
    }
}
