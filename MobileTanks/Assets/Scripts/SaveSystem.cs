using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    //==========================================================
    //                      EXPLINATION
    //==========================================================
    /*
     *  "HighestLevel" will be the highest level the player has reached and will corispond to the scene build index
     *  The player will be able to play the level after highest level, unless it is the final level
     * 
     *  "Level_#" will be the way we acess the number of stars earned per level it will have 3 states
     *  1 = killed all enemies, 2 = found all blue blocks, 3 = killed all and found all
     *  an example of use is PlayerPrefs.GetItn("Level_3");
     * 
     * */


    public int totalNumberOfLevels; // to
    public int highestLevel;
    public static SaveSystem ss;

    void Start()
    {
        if (ss == null)
            ss = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
        Load();
    }

    public void SaveLevel(int levelNumber, int stars)
    {
        int currentHighest = PlayerPrefs.GetInt("HighestLevel");
        if(levelNumber > currentHighest)
            PlayerPrefs.SetInt("HighestLevel", levelNumber);
        PlayerPrefs.SetInt("Level_" + levelNumber.ToString(), stars);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("HighestLevel"))
            highestLevel = PlayerPrefs.GetInt("HighestLevel");
        else
            highestLevel = 0;
    }

    public int GetLevelStars(int levelNumber)
    {
        if (PlayerPrefs.HasKey("Level_" + levelNumber.ToString()))
            return PlayerPrefs.GetInt("Level_" + levelNumber.ToString());
        else
            return 0;
    }
}
