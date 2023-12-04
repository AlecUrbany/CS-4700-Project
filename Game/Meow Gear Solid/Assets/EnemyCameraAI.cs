using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameraAI : MonoBehaviour
{
    public Transform enemyMouth;
    [SerializeField] private GameObject floatingTextBox;
    public float nameLifeSpan = .5f;
    public AudioSource source;
    public AudioClip alertSound;
    public bool hasBeenAlerted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool canSeePlayer = GetComponent<visionCone>().canSeePlayer;
        if(canSeePlayer == true)
        {
            if(!hasBeenAlerted)
            {
                ShowAlertSound();
                hasBeenAlerted = true;
            }
        }
        
    }
    void ShowAlertSound()
    {
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, enemyMouth, false);
            source.PlayOneShot(alertSound);
            Destroy(prefab, nameLifeSpan);
        }
    }
}
