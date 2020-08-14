using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorOpen;
    public GameObject doorClose;
    public bool startOpen;

    private bool isOpen = false;
    private bool isClosed = false;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;


        // Calculate the journey length.
        journeyLength = Vector3.Distance(doorOpen.transform.position, doorClose.transform.position);

        if (startOpen)
        {
            this.gameObject.transform.position = doorOpen.transform.position;
        }

        else
        {
            this.gameObject.transform.position = doorClose.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;
        
        if(isOpen)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, doorOpen.transform.position, fractionOfJourney);
        }

        if (isClosed)
        {
            this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, doorClose.transform.position, fractionOfJourney);
        }

        //this.gameObject.transform.position = Vector3.Lerp(doorClose.transform.position, doorOpen.transform.position, fractionOfJourney);
    }

    public void DoorOpen()
    {
        isOpen = true;
        isClosed = false;
    }
    public void DoorClose()
    {
        isOpen = false;
        isClosed = true;
    }
}
