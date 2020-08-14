using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public GameObject RayCastSource;
    public GameObject lightSource;
    public float scanRange;
    public float timeOn;
    public float timeOff;
    public bool lightStartOn;
    private bool isLightOn;
    private float lightTimer;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        isLightOn = lightStartOn;
        lightSource.SetActive(isLightOn);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameController.Instance.player;

        if (isLightOn)
        {
            //Debug.Log(PlayerIsInLight(player));
            GameController.Instance.PlayerIsInLight(PlayerIsInLight(player));
            if (lightTimer >= timeOn)
            {
                isLightOn = false;
                lightSource.SetActive(false);
                lightTimer = 0;

            }
            else
            {
                lightTimer += Time.deltaTime;
            }

        }

        else
        {
            //Debug.Log("light Off");
            GameController.Instance.PlayerIsInLight(false);
            if (lightTimer >= timeOff)
            {
                isLightOn = true;
                lightSource.SetActive(true);
                lightTimer = 0;
            }
            else
            {
                lightTimer += Time.deltaTime;
            }

        }

        

    }

    private bool PlayerIsInLight(GameObject targetTrans)
    {
        Vector3 RayCastStartPos = RayCastSource.transform.position;
        RaycastHit hit;
        Vector3 RayCastDir = targetTrans.transform.position - RayCastStartPos;
        //RayCastDir.y = 0.0f;
        RayCastDir.Normalize();
        int layerMask = LayerMask.GetMask("Ignore Raycast");
        layerMask = ~layerMask;

        if (Physics.Raycast(RayCastStartPos, RayCastDir, out hit, scanRange, layerMask))
        {
            if (hit.collider.gameObject.name != "Cube" && hit.collider.gameObject.name != "Player(Clone)")
            {
                //Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);
            }



            Player player = hit.transform.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * scanRange, Color.red);
                return true;
            }

            else
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * scanRange, Color.yellow);
                return false;
            }

        }

        else
        {
            Debug.DrawRay(RayCastStartPos, RayCastDir * scanRange, Color.yellow);
            return false;
        }
    }
}
