using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject connectedDoor;

    public bool closeWhenLeaving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Door door = connectedDoor.GetComponent<Door>();
        door.DoorOpen();
    }

    private void OnTriggerExit(Collider other)
    {
        if(closeWhenLeaving)
        {
            Door door = connectedDoor.GetComponent<Door>();
            door.DoorClose();
        }
    }
}
