using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Goal goal;
    private Movement player;
    public GameObject levelComplete, levelFailed;
    public Text levelTimerText;
    public float levelTimer = 120;
    private bool levelStarted = false;
    private bool levelOver = false;
    public int blueBlocks = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        player.CanMove = false;

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<Enemy>().CanMove = false;
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ColorChangingBlock"))
        {
            blueBlocks += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //We lost :(
        if (player.health <= 0 || levelTimer <= 0)
        {
            levelFailed.SetActive(true);
            player.CanMove = false;
            levelTimer = 0;
        }

        //We won!
        if (!levelOver && goal.goalRached)
        {
            levelOver = true;

            //save the game
            int oldStars = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().GetLevelStars((SceneManager.GetActiveScene().buildIndex));

            int stars = 0;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                stars += 1;
            if (blueBlocks == 0)
                stars += 2;
            if (oldStars > stars)
                stars = oldStars;
            GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().SaveLevel(SceneManager.GetActiveScene().buildIndex, stars);


            levelComplete.SetActive(true);
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                go.GetComponent<Enemy>().CanMove = false;
                levelStarted = false;
            }
        }

        if (levelStarted)
        {
            levelTimer -= Time.deltaTime;
            int min = Mathf.FloorToInt(levelTimer / 60);
            int sec = Mathf.FloorToInt(levelTimer % 60);
            levelTimerText.text = min.ToString("00") + ":" + sec.ToString("00");

            if(levelTimer < 0)
            {
                levelTimerText.text = "00:00";
            }
        }
    }

    public void StartGame()
    {
        player.CanMove = true;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<Enemy>().CanMove = true;
        }

        levelStarted = true;
    }

    public void Pause()
    {
        player.CanMove = false;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            go.GetComponent<Enemy>().CanMove = false;
        }

        levelStarted = false;
    }
}
