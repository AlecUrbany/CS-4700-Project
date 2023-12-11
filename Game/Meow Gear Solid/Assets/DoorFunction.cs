using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    public bool doorOpen;
    [SerializeField] private bool needKey;
    [SerializeField] private bool doorLR;
    [SerializeField] private Transform door;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float speed = .5f;
    private float current, target;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        var myValue = Mathf.Lerp(0,10,.5f);
        switch(doorLR)
        {
            case false:
            {
                endPosition = new Vector3(0,0,-5) + door.transform.position;
                break;
            }
            case true:
            {
                endPosition = new Vector3(-5,0,0) + door.transform.position;
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
            door.transform.position = Vector3.Lerp(door.transform.position, endPosition, curve.Evaluate(current));
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
