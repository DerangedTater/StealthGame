using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    public GameObject RayCastSource;
    public float lightScanRange;
    public float darkScanRange;
    public float baseScanRange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player = GameController.Instance.player;

        if (CanSeeTargetBase(Player.transform))
        {
            GameController.Instance.GameOver(this.gameObject);
        }

        if (CanSeeTargetLight(Player.transform))
        {
            if (GameController.Instance.LightCheck())
            {
                GameController.Instance.GameOver(this.gameObject);
            }
        }

        if (CanSeeTargetDark(Player.transform))
        {
            if (!GameController.Instance.LightCheck())
            {
                GameController.Instance.GameOver(this.gameObject);
            }
        }
        //Debug.Log(destPoint);
        //Debug.Log(IsFacingPoint());
        //Debug.Log(CanSeeTargetBase(Player.transform));
    }

    private bool CanSeeTargetBase(Transform targetTrans)
    {
        Vector3 RayCastStartPos = RayCastSource.transform.position;
        RaycastHit hit;
        Vector3 RayCastDir = targetTrans.position - RayCastStartPos;
        //RayCastDir.y = 0.0f;
        RayCastDir.Normalize();
        int layerMask = LayerMask.GetMask("Ignore Raycast");
        layerMask = ~layerMask;

        if (Physics.Raycast(RayCastStartPos, RayCastDir, out hit, baseScanRange, layerMask))
        {

            //Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            Player player = hit.transform.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * baseScanRange, Color.red);
                return true;
            }

            else
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * baseScanRange, Color.yellow);
                return false;
            }

        }

        else
        {
            Debug.DrawRay(RayCastStartPos, RayCastDir * baseScanRange, Color.yellow);
            return false;
        }
    }

    private bool CanSeeTargetLight(Transform targetTrans)
    {
        Vector3 RayCastStartPos = RayCastSource.transform.position;
        RaycastHit hit;
        Vector3 RayCastDir = targetTrans.position - RayCastStartPos;
        //RayCastDir.y = 0.0f;
        RayCastDir.Normalize();
        int layerMask = LayerMask.GetMask("Ignore Raycast");
        layerMask = ~layerMask;

        if (Physics.Raycast(RayCastStartPos, RayCastDir, out hit, lightScanRange, layerMask))
        {

            //Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            Player player = hit.transform.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * lightScanRange, Color.red);
                return true;
            }

            else
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * lightScanRange, Color.yellow);
                return false;
            }

        }

        else
        {
            Debug.DrawRay(RayCastStartPos, RayCastDir * lightScanRange, Color.yellow);
            return false;
        }
    }

    private bool CanSeeTargetDark(Transform targetTrans)
    {
        Vector3 RayCastStartPos = RayCastSource.transform.position;
        RaycastHit hit;
        Vector3 RayCastDir = targetTrans.position - RayCastStartPos;
        //RayCastDir.y = 0.0f;
        RayCastDir.Normalize();
        int layerMask = LayerMask.GetMask("Ignore Raycast");
        layerMask = ~layerMask;

        if (Physics.Raycast(RayCastStartPos, RayCastDir, out hit, darkScanRange, layerMask))
        {

            //Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            Player player = hit.transform.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * darkScanRange, Color.red);
                return true;
            }

            else
            {
                Debug.DrawRay(RayCastStartPos, RayCastDir * darkScanRange, Color.yellow);
                return false;
            }

        }

        else
        {
            Debug.DrawRay(RayCastStartPos, RayCastDir * darkScanRange, Color.yellow);
            return false;
        }
    }
}
