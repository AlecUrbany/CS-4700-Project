using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    public GameObject bullet;
    public float damage = 50f;

    //Sound Stuff
    public AudioClip punchSound1;
    public AudioClip punchSound2;
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AudioSource.PlayClipAtPoint(punchSound1, transform.position, 1f);
            Destroy(bullet);
            Debug.Log("destroy");

            EnemyHealth enemyScript = other.gameObject.GetComponent<EnemyHealth>();
            if(enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
                Destroy(bullet);
            }
        }
    
    }
}
