using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private int totalNumberOfLevels;
    public int highestLevel;
    private int currentLevel;
    private SaveSystem saveSys;

    public Text levelText;
    public GameObject starComplete, starKill, starBlue;



    void Start()
    {
        saveSys = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>();
        saveSys.Load();
        totalNumberOfLevels = saveSys.totalNumberOfLevels;
        highestLevel = saveSys.highestLevel;
        if (highestLevel != saveSys.totalNumberOfLevels)
            currentLevel = highestLevel + 1;
        else
            currentLevel = highestLevel;

        UpdateBanner();
    }

    public void Next()
    {
        if (currentLevel != highestLevel + 1 && currentLevel != totalNumberOfLevels)
        {
            currentLevel += 1;
            UpdateBanner();
        }
    }

    public void Last()
    {
        if (currentLevel != 1)
        {
            currentLevel -= 1;
            UpdateBanner();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void UpdateBanner()
    {
        int stars = 0;
        if (currentLevel != highestLevel + 1)
        {
            stars = saveSys.GetLevelStars(currentLevel);
        }

        levelText.text = currentLevel.ToString();

        switch(stars)
        {
            case 0:
                starComplete.SetActive(false);
                starKill.SetActive(false);
                starBlue.SetActive(false);
                break;
            case 1:
                starComplete.SetActive(true);
                starKill.SetActive(true);
                starBlue.SetActive(false);
                break;
            case 2:
                starComplete.SetActive(true);
                starKill.SetActive(false);
                starBlue.SetActive(true);
                break;
            case 3:
                starComplete.SetActive(true);
                starKill.SetActive(true);
                starBlue.SetActive(true);
                break;
        }


    }

}
