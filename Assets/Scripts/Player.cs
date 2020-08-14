using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed;
    public float TurningSpeed;
    public float MaximumPitch;
    public Camera PlayerCamera;
    public CharacterController PlayerController;
    public GameObject initialCheckpoint;
    private GameObject currentCheckpoint;
    private bool gameOver = false;   

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = initialCheckpoint;   
    }

    // Update is called once per frame
    void Update()
    {


        if(gameOver)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                GameController.Instance.KeepPatrolling();
                gameOver = false;
                this.gameObject.transform.position = currentCheckpoint.transform.position;
                this.gameObject.transform.rotation = currentCheckpoint.transform.rotation;
                GameController.Instance.EndInteraction();
            }

        }
        else
        {
            Movement();
        }
    }

    public void GameOver(GameObject enemy)
    {
        gameOver = true;
        Vector3 facing = transform.forward;
        Vector3 toTarget = enemy.transform.position - this.transform.position;
        Vector3 newFacing = Vector3.RotateTowards(facing, toTarget, TurningSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newFacing);
    }

    private void Movement()
    {
        Vector3 move = Vector3.zero;

        //Forward/Back/Left/Right Movement
        float frameMovementSpeed = Time.deltaTime * MovementSpeed;
        float moveX = Input.GetAxis("Horizontal") * frameMovementSpeed;
        float moveZ = Input.GetAxis("Vertical") * frameMovementSpeed;
        move = new Vector3(moveX, 0.0f, moveZ);
        move = Vector3.ClampMagnitude(move, frameMovementSpeed);
        move = this.transform.TransformVector(move);
        PlayerController.Move(move);

        // Yaw Rotation (Player)
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * TurningSpeed;
        Vector3 playerRot = transform.rotation.eulerAngles;
        playerRot.y += mouseX;
        transform.rotation = Quaternion.Euler(playerRot);

        // Mouse Look (Camera)
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * TurningSpeed;
        Vector3 cameraRot = PlayerCamera.transform.rotation.eulerAngles;
        if (cameraRot.x > MaximumPitch)
        {
            cameraRot.x -= 360.0f;
        }
        if (cameraRot.x < -MaximumPitch)
        {
            cameraRot.x += 360.0f;
        }
        cameraRot.x -= mouseY;
        cameraRot.x = Mathf.Clamp(cameraRot.x, -MaximumPitch, MaximumPitch);
        PlayerCamera.transform.rotation = Quaternion.Euler(cameraRot);
    }

    public void gameReset()
    {
        currentCheckpoint = initialCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckpointSetter")
        {
            CheckpointSetter checkpointSetter = other.gameObject.GetComponent<CheckpointSetter>();
            currentCheckpoint = checkpointSetter.checkpoint;
            Debug.Log("Triggered");
        }
    }
}
