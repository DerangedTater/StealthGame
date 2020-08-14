using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public GameObject[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public float TurnSpeed;
    public float delayAmount;
    public bool turnBeforeMoving;
    private float delayTimer = 0;
    private bool gameOver = false;

    public GameObject RayCastSource;
    public float lightScanRange;
    public float darkScanRange;
    public float baseScanRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && !gameOver)
        {
            agent.destination = this.gameObject.transform.position;
            if(turnBeforeMoving)
            {
                if (!IsFacingPoint())
                {
                    TurnTowardsPoint();
                }
            }


            if (delayTimer >= delayAmount)
            {
                GotoNextPoint();
                delayTimer = 0;
            }
            else
            {
                delayTimer += Time.deltaTime;
            }

        }

        GameObject Player = GameController.Instance.player;

        if (CanSeeTargetBase(Player.transform))
        {
            GameController.Instance.GameOver(this.gameObject);
            agent.destination = this.gameObject.transform.position;
            gameOver = true;
        }

        if (CanSeeTargetLight(Player.transform))
        {
            if (GameController.Instance.LightCheck())
            {
                GameController.Instance.GameOver(this.gameObject);
                agent.destination = this.gameObject.transform.position;
                gameOver = true;
            }
        }

        if (CanSeeTargetDark(Player.transform))
        {
            if (!GameController.Instance.LightCheck())
            {
                GameController.Instance.GameOver(this.gameObject);
                agent.destination = this.gameObject.transform.position;
                gameOver = true;
            }
        }
        //Debug.Log(destPoint);
        //Debug.Log(IsFacingPoint());
        //Debug.Log(CanSeeTargetBase(Player.transform));
    }

    private bool IsFacingPoint()
    {
        GameObject point = points[destPoint];
        Vector3 facing = transform.forward;
        Vector3 toPlayer = point.transform.position - this.transform.position;
        float dotProduct = Vector3.Dot(facing, toPlayer);
        float radianAngle = Mathf.Acos(dotProduct);
        float degreesAngle = Mathf.Rad2Deg * radianAngle;

        return (degreesAngle == 0.0f);
    }

    private void TurnTowardsPoint()
    {
        GameObject point = points[destPoint];
        Vector3 facing = transform.forward;
        Vector3 toTarget = point.transform.position - this.transform.position;
        Vector3 newFacing = Vector3.RotateTowards(facing, toTarget, TurnSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newFacing);
    }

    private void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].transform.position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void BackToPatrolling()
    {
        agent.destination = points[destPoint].transform.position;
        gameOver = false;
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
