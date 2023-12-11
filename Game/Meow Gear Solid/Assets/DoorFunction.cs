using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    public bool doorOpen;
    public bool inAnimation;
    [SerializeField] private bool needKey;
    [SerializeField] private bool doorLR;
    [SerializeField] private Transform door;
    [SerializeField] private Transform doorHitbox;
    [SerializeField] private AnimationCurve curve, curveReturn;
    [SerializeField] private Vector3 endPosition, startPosition;
    [SerializeField] private float speed = .5f;
    private float current, currentReturn, target;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        inAnimation = false;
        var myValue = Mathf.Lerp(0,10,.5f);
        startPosition = door.transform.position;
        switch(doorLR)
        {
            case false:
            {
                endPosition = new Vector3(0,0,-6.7f) + door.transform.position;
                break;
            }
            case true:
            {
                endPosition = new Vector3(-6.7f,0,0) + door.transform.position;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(doorOpen == true)
        {        
            current = Mathf.MoveTowards(current, 5, speed * Time.deltaTime);
            door.transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(current));
            doorHitbox.transform.position = startPosition; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(needKey == false)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                doorOpen = true;
            }
            else
                doorOpen = false;            
        }

        else
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Key1"))
            {
                doorOpen = true;
            }
            else
                doorOpen = false;
        }
    }
}
