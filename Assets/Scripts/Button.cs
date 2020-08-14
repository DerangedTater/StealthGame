using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Material off;
    public Material on;
    public GameObject button;
    public GameObject connectedDoor;

    public bool startOn;
    private bool isInRange = false;
    private bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        if(startOn)
        {
            button.GetComponent<MeshRenderer>().material = on;
            isOn = true;
        }
        else
        {
            button.GetComponent<MeshRenderer>().material = off;
            isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isInRange)
            {
                if(!isOn)
                {
                    button.GetComponent<MeshRenderer>().material = on;
                    isOn = true;
                    Door door = connectedDoor.GetComponent<Door>();
                    door.DoorOpen();
                }

                else
                {
                    button.GetComponent<MeshRenderer>().material = off;
                    isOn = false;
                    Door door = connectedDoor.GetComponent<Door>();
                    door.DoorClose();
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameController.Instance.BeginInteraction();
        isInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GameController.Instance.EndInteraction();
        isInRange = false;
    }
}
