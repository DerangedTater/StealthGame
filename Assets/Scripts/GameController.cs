using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public Player myPlayerComponent;
    private bool isInLight;
    private GameObject gameOverEnemy;
    private int currentPoint;
    private bool playerWon = false;

    public Text helpText;

    private static GameController _instance = null;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        helpText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inLight);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(playerWon)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    

    public void PlayerIsInLight(bool inLight)
    {
        isInLight = inLight;
        
        //Debug.Log(inLight);
    }

    public void BeginInteraction()
    {
        helpText.text = "Press space to press";
    }

    public void EndInteraction()
    {
        helpText.text = "";
    }

    public void GameOver(GameObject enemy)
    {
        //Player myPlayer = player.GetComponent<Player>();
        myPlayerComponent.GameOver(enemy);
        helpText.text = "You've been caught! \n Press space to try again.";
        if(enemy.tag == "Patrol")
        {
            gameOverEnemy = enemy;
        }
    }

    public void KeepPatrolling()
    {
        if(gameOverEnemy != null)
        {
            PatrollingEnemy patrolEnemy = gameOverEnemy.GetComponent<PatrollingEnemy>();
            patrolEnemy.BackToPatrolling();
            gameOverEnemy = null;
        }
    }

    public void Win()
    {
        helpText.text = "You Escaped! \n Press space to play again.";
        helpText.color = Color.black;
        playerWon = true;
    }

    public bool LightCheck()
    {
        return isInLight;
    }
}
