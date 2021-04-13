using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{

    public static MainController S; //singleton

    public float spawnFishSeconds = 2f;

    public float spawnSharkSeconds = 30f;


    public GameObject fish;
    public GameObject shark;
    public GameObject hook;
    public int score;

    public Inv playerInventory;

    public int lives = 3;

    public Text scoreText;
    public Text livesText;

    private Hook hookScript;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        } else
        {
            Debug.LogError("singleton already exists");
        }
        SpawnFish();
        SpawnShark();
        hookScript = hook.GetComponent<Hook>();
    }

    private void Update()
    {
        CheckMouse();
    }


    private void CheckMouse()
    {
        if (Input.GetKey("mouse 0") && hookScript.Hooked && Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 3)
        {
            hookScript.Hooked = false;
            UpdateScore(score + 1);
            playerInventory.coins += 1;
        }
    }

    private void UpdateScore(int score)
    {
        this.score = score;
        scoreText.text = "Score: " + score;
    }

    public void LooseHP()
    {
        lives--;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            SceneManager.UnloadSceneAsync("FishingGame");
        }
    }

    public void SpawnFish()
    {

        GameObject go = Instantiate<GameObject>(fish); //make the fish

        float x = -10;

        float y = 0;
        if (go.GetComponent<BoundsCheck>() != null) //see if the enemy has a boundcheck
        {
            BoundsCheck bndCheck = go.GetComponent<BoundsCheck>();
            x = bndCheck.xpos - bndCheck.camWidth - 2;
            y = Random.Range(1 - bndCheck.camHeight, bndCheck.camHeight - 3f) - bndCheck.ypos; //set the padding to the boundcheck radius
        }

        Vector2 pos = new Vector2(x, y);

        go.transform.position = pos; //set the position

        Invoke("SpawnFish", Random.Range(spawnFishSeconds, spawnFishSeconds*2));
    }

    public void SpawnShark()
    {
        GameObject go = Instantiate<GameObject>(shark); //make the shark

        float x = 1000;

        float y = 0;
        if (go.GetComponent<BoundsCheck>() != null) //see if the enemy has a boundcheck
        {
            BoundsCheck bndCheck = go.GetComponent<BoundsCheck>();
            y = Random.Range(1 - bndCheck.camHeight, bndCheck.camHeight - 3f) - bndCheck.ypos; //set the padding to the boundcheck radius
            x = bndCheck.camWidth + bndCheck.xpos + 5;
        }

        Vector2 pos = new Vector2(x, y);

        go.transform.position = pos; //set the position

        Invoke("SpawnShark", Random.Range(2, 4));
    }
}
