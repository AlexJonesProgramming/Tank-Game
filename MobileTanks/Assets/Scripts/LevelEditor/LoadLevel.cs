using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using SimpleFileBrowser;
public class LoadLevel : MonoBehaviour
{

    public GameObject[] woodBlocks;
    public GameObject[] explosiveBlocks;
    public GameObject[] bouncyBlocks;
    public GameObject[] physicsBlocks;
    public GameObject[] collectableBlocks;
    public GameObject spawn, enemy, goal, player, camera;

    public string path;
    string toDocuments;

    private bool useFilePanel = true;
    public bool play = false;

    private void Start()
    {
        string username = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        toDocuments = username + "\\ToyTanksLevels";
    }



    // ====================================
    // Blocks are saved by "block_number X Y Z RptX RotY RotZ"
    // Headers contain the number of that block type 
    // ====================================
    public void Load(bool filePanel)
    {
        useFilePanel = filePanel;
        ClearLevel();
        StartCoroutine(Load());

    }


    public void StartTestMode()
    {
        play = false;
        StartCoroutine(waitForSave());
    }

    IEnumerator waitForSave()
    {
        yield return new WaitUntil(canPlay);

        camera.SetActive(false);

        Vector3 pos = spawn.transform.position;
        Quaternion rot = spawn.transform.rotation;
        spawn.SetActive(false);

        player.SetActive(true);
        player.transform.position = pos;
        player.transform.rotation = rot;

        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GO.GetComponent<Enemy>().enabled = true;
        }
    }

    public bool canPlay()
    { return play; }

    public void StopTestMode()
    {
        camera.SetActive(true);
        spawn.SetActive(true);
        player.SetActive(false);

        ClearLevel();
        Load(false);
    }


    private void ClearLevel()
    {
        GameObject[] woodBlocks = GameObject.FindGameObjectsWithTag("Wood");

        foreach (GameObject GO in woodBlocks)
        {
            Destroy(GO);
        }

        // Explosive Blocks
        GameObject[] explosiveBlocks = GameObject.FindGameObjectsWithTag("ExplodingBlock");

        foreach (GameObject GO in explosiveBlocks)
        {
            Destroy(GO);
        }

        // Bouncy Block
        GameObject[] bouncyBlocks = GameObject.FindGameObjectsWithTag("BouncyBlock");

        foreach (GameObject GO in bouncyBlocks)
        {
            Destroy(GO);
        }

        // Physics Blocks
        GameObject[] physicsBlocks = GameObject.FindGameObjectsWithTag("MoveableBlock");

        foreach (GameObject GO in physicsBlocks)
        {
            Destroy(GO);
        }

        //Collectable Blocks
        GameObject[] collectableBlocks = GameObject.FindGameObjectsWithTag("ColorChangingBlock");

        foreach (GameObject GO in collectableBlocks)
        {
            Destroy(GO);
        }


        //Enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject GO in enemies)
        {
            Destroy(GO);
        }

        //Bullets

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject GO in bullets)
        {
            Destroy(GO);
        }
    }

    IEnumerator Load()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        if (useFilePanel)
        {
            yield return FileBrowser.WaitForLoadDialog(false, toDocuments, "Load Level", "Load");
            if(FileBrowser.Success)
                path = FileBrowser.Result;
        }

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        //Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        
        //the loading
        if (File.Exists(path))
        {
            string countStr;
            int count;

            StreamReader sr = File.OpenText(path);

            // Wood blocks
            countStr = sr.ReadLine();
            count = int.Parse(countStr);

            for (int i = 0; i < count; i++)
            {
                string[] blockInfo = sr.ReadLine().Split(' ');
                GameObject GO = GameObject.Instantiate(woodBlocks[int.Parse(blockInfo[0])]);
                Vector3 pos = new Vector3(float.Parse(blockInfo[1]), float.Parse(blockInfo[2]), float.Parse(blockInfo[3]));
                Vector3 rot = new Vector3(float.Parse(blockInfo[4]), float.Parse(blockInfo[5]), float.Parse(blockInfo[6]));
                GO.transform.position = pos;
                GO.transform.rotation = Quaternion.Euler(rot);
                GO.name = sr.ReadLine();
            }

            // Explosive Blocks
            countStr = sr.ReadLine();
            count = int.Parse(countStr);

            for (int i = 0; i < count; i++)
            {
                string[] blockInfo = sr.ReadLine().Split(' ');
                GameObject GO = GameObject.Instantiate(explosiveBlocks[int.Parse(blockInfo[0])]);
                Vector3 pos = new Vector3(float.Parse(blockInfo[1]), float.Parse(blockInfo[2]), float.Parse(blockInfo[3]));
                Vector3 rot = new Vector3(float.Parse(blockInfo[4]), float.Parse(blockInfo[5]), float.Parse(blockInfo[6]));
                GO.transform.position = pos;
                GO.transform.rotation = Quaternion.Euler(rot);
                GO.name = sr.ReadLine();
            }

            // Bouncy Block
            countStr = sr.ReadLine();
            count = int.Parse(countStr);

            for (int i = 0; i < count; i++)
            {
                string[] blockInfo = sr.ReadLine().Split(' ');
                GameObject GO = GameObject.Instantiate(bouncyBlocks[int.Parse(blockInfo[0])]);
                Vector3 pos = new Vector3(float.Parse(blockInfo[1]), float.Parse(blockInfo[2]), float.Parse(blockInfo[3]));
                Vector3 rot = new Vector3(float.Parse(blockInfo[4]), float.Parse(blockInfo[5]), float.Parse(blockInfo[6]));
                GO.transform.position = pos;
                GO.transform.rotation = Quaternion.Euler(rot);
                GO.name = sr.ReadLine();
            }

            // Physics Blocks
            countStr = sr.ReadLine();
            count = int.Parse(countStr);

            for (int i = 0; i < count; i++)
            {
                string[] blockInfo = sr.ReadLine().Split(' ');
                GameObject GO = GameObject.Instantiate(physicsBlocks[int.Parse(blockInfo[0])]);
                Vector3 pos = new Vector3(float.Parse(blockInfo[1]), float.Parse(blockInfo[2]), float.Parse(blockInfo[3]));
                Vector3 rot = new Vector3(float.Parse(blockInfo[4]), float.Parse(blockInfo[5]), float.Parse(blockInfo[6]));
                GO.transform.position = pos;
                GO.transform.rotation = Quaternion.Euler(rot);
                GO.name = sr.ReadLine();
            }

            //Collectable Blocks
            countStr = sr.ReadLine();
            count = int.Parse(countStr);

            for (int i = 0; i < count; i++)
            {
                string[] blockInfo = sr.ReadLine().Split(' ');
                GameObject GO = GameObject.Instantiate(collectableBlocks[int.Parse(blockInfo[0])]);
                Vector3 pos = new Vector3(float.Parse(blockInfo[1]), float.Parse(blockInfo[2]), float.Parse(blockInfo[3]));
                Vector3 rot = new Vector3(float.Parse(blockInfo[4]), float.Parse(blockInfo[5]), float.Parse(blockInfo[6]));
                GO.transform.position = pos;
                GO.transform.rotation = Quaternion.Euler(rot);
                GO.name = sr.ReadLine();
            }

            //enemies
            countStr = sr.ReadLine();
            count = int.Parse(countStr);

            for (int i = 0; i < count; i++)
            {
                string[] blockInfo = sr.ReadLine().Split(' ');
                GameObject GO = GameObject.Instantiate(enemy);
                Vector3 pos = new Vector3(float.Parse(blockInfo[1]), float.Parse(blockInfo[2]), float.Parse(blockInfo[3]));
                Vector3 rot = new Vector3(float.Parse(blockInfo[4]), float.Parse(blockInfo[5]), float.Parse(blockInfo[6]));
                GO.transform.position = pos;
                GO.transform.rotation = Quaternion.Euler(rot);
                GO.name = sr.ReadLine();
            }


            // player spwan
            string[] spawnInfo = sr.ReadLine().Split(' ');
            Vector3 spawnPos = new Vector3(float.Parse(spawnInfo[1]), float.Parse(spawnInfo[2]), float.Parse(spawnInfo[3]));
            Vector3 spawnRot = new Vector3(float.Parse(spawnInfo[4]), float.Parse(spawnInfo[5]), float.Parse(spawnInfo[6]));
            spawn.transform.position = spawnPos;
            spawn.transform.rotation = Quaternion.Euler(spawnRot);
            sr.ReadLine(); // we do this because we have the name saved on the next line and do not need it.

            // goal
            string[] goalInfo = sr.ReadLine().Split(' ');
            Vector3 goalPos = new Vector3(float.Parse(goalInfo[1]), float.Parse(goalInfo[2]), float.Parse(goalInfo[3]));
            Vector3 goalRot = new Vector3(float.Parse(goalInfo[4]), float.Parse(goalInfo[5]), float.Parse(goalInfo[6]));
            goal.transform.position = goalPos;
            goal.transform.rotation = Quaternion.Euler(goalRot);


            //close the file
            sr.Close();
        }
        
    }

}
