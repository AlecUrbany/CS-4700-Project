using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            EnemyHealth enemyScript = other.gameObject.GetComponent<EnemyHealth>();
            Transform camLocation = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
            if(enemyScript != null)
            {
                if(Random.Range(0, 2) == 0)
                {
                    AudioSource.PlayClipAtPoint(punchSound2, camLocation.transform.position, 1.0f);
                    enemyScript.TakeDamage(damage);
                    Destroy(bullet);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(punchSound2, camLocation.transform.position, 1.0f);
                    enemyScript.TakeDamage(damage);
                    Destroy(bullet);
                }
                
            }
        }
    
    }
}
