using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    public GameObject RayCastSource;
    public float scanRange;
    private GameObject player;
    private bool sentFalse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameController.Instance.player;
        if(!sentFalse && !PlayerIsInLight(player))
        {
            GameController.Instance.PlayerIsInLight(PlayerIsInLight(player));
            sentFalse = true;
            Debug.Log("I Sent False");
        }
        else if(PlayerIsInLight(player))
        {
            GameController.Instance.PlayerIsInLight(PlayerIsInLight(player));
            Debug.Log("I am True!");
            sentFalse = false;
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
