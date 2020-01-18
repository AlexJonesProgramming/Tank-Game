using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using SimpleFileBrowser;
public class SaveLevel : MonoBehaviour
{
    private string path;
    string toDocuments;

    private void Start()
    {

        string username = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        toDocuments = username + "\\ToyTanksLevels";
        Directory.CreateDirectory(toDocuments);

        FileBrowser.SetFilters(false, new FileBrowser.Filter("Saves", ".save"));
        FileBrowser.SetDefaultFilter(".save");
    }


    // ====================================
    // Blocks are saved by "block_number X Y Z RotX RotY RotZ"
    // Headers contain the number of that block type s
    // ====================================
    public void Save()
    {
        StartCoroutine(SaveCo());
    }


    IEnumerator SaveCo()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        
        yield return FileBrowser.WaitForSaveDialog(false, toDocuments, "Save Level", "Save");

        

        //the saving
        if (FileBrowser.Success)
        {
            path = FileBrowser.Result;
            print(path);
            if (path != "")
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter sw = File.CreateText(path);

                // Wood blocks
                GameObject[] woodBlocks = GameObject.FindGameObjectsWithTag("Wood");
                sw.WriteLine(woodBlocks.Length);

                foreach (GameObject GO in woodBlocks)
                {
                    string outString = "";
                    string name = GO.name.Split(' ')[0];
                    Vector3 pos = GO.transform.position;
                    string location = " " + pos.x + " " + pos.y + " " + pos.z + " ";
                    Vector3 rot = GO.transform.rotation.eulerAngles;
                    string rotation = rot.x + " " + rot.y + " " + rot.z;

                    outString = name + location + rotation;
                    sw.WriteLine(outString);
                    sw.WriteLine(GO.name);
                }

                // Explosive Blocks
                GameObject[] explosiveBlocks = GameObject.FindGameObjectsWithTag("ExplodingBlock");
                sw.WriteLine(explosiveBlocks.Length);

                foreach (GameObject GO in explosiveBlocks)
                {
                    string outString = "";
                    string name = GO.name.Split(' ')[0];
                    Vector3 pos = GO.transform.position;
                    string location = " " + pos.x + " " + pos.y + " " + pos.z + " ";
                    Vector3 rot = GO.transform.rotation.eulerAngles;
                    string rotation = rot.x + " " + rot.y + " " + rot.z;

                    outString = name + location + rotation;
                    sw.WriteLine(outString);
                    sw.WriteLine(GO.name);
                }

                // Bouncy Block
                GameObject[] bouncyBlocks = GameObject.FindGameObjectsWithTag("BouncyBlock");
                sw.WriteLine(bouncyBlocks.Length);

                foreach (GameObject GO in bouncyBlocks)
                {
                    string outString = "";
                    string name = GO.name.Split(' ')[0];
                    Vector3 pos = GO.transform.position;
                    string location = " " + pos.x + " " + pos.y + " " + pos.z + " ";
                    Vector3 rot = GO.transform.rotation.eulerAngles;
                    string rotation = rot.x + " " + rot.y + " " + rot.z;

                    outString = name + location + rotation;
                    sw.WriteLine(outString);
                    sw.WriteLine(GO.name);
                }

                // Physics Blocks
                GameObject[] physicsBlocks = GameObject.FindGameObjectsWithTag("MoveableBlock");
                sw.WriteLine(physicsBlocks.Length);

                foreach (GameObject GO in physicsBlocks)
                {
                    string outString = "";
                    string name = GO.name.Split(' ')[0];
                    Vector3 pos = GO.transform.position;
                    string location = " " + pos.x + " " + pos.y + " " + pos.z + " ";
                    Vector3 rot = GO.transform.rotation.eulerAngles;
                    string rotation = rot.x + " " + rot.y + " " + rot.z;

                    outString = name + location + rotation;
                    sw.WriteLine(outString);
                    sw.WriteLine(GO.name);
                }

                //Collectable Blocks
                GameObject[] collectableBlocks = GameObject.FindGameObjectsWithTag("ColorChangingBlock");
                sw.WriteLine(collectableBlocks.Length);

                foreach (GameObject GO in collectableBlocks)
                {
                    string outString = "";
                    string name = GO.name.Split(' ')[0];
                    Vector3 pos = GO.transform.position;
                    string location = " " + pos.x + " " + pos.y + " " + pos.z + " ";
                    Vector3 rot = GO.transform.rotation.eulerAngles;
                    string rotation = rot.x + " " + rot.y + " " + rot.z;

                    outString = name + location + rotation;
                    sw.WriteLine(outString);
                    sw.WriteLine(GO.name);
                }


                //Enemies
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                sw.WriteLine(enemies.Length);

                foreach (GameObject GO in enemies)
                {
                    string outString = "";
                    string name = GO.name.Split(' ')[0];
                    Vector3 pos = GO.transform.position;
                    string location = " " + pos.x + " " + pos.y + " " + pos.z + " ";
                    Vector3 rot = GO.transform.rotation.eulerAngles;
                    string rotation = rot.x + " " + rot.y + " " + rot.z;

                    outString = name + location + rotation;
                    sw.WriteLine(outString);
                    sw.WriteLine(GO.name);
                }


                //Spawn
                GameObject spawn = GameObject.FindGameObjectWithTag("Player");
                string spawnString = "";
                Vector3 spawnPos = spawn.transform.position;
                string spawnLocation = " " + spawnPos.x + " " + spawnPos.y + " " + spawnPos.z + " ";
                Vector3 spawnRot = spawn.transform.rotation.eulerAngles;
                string spawnRotation = spawnRot.x + " " + spawnRot.y + " " + spawnRot.z;

                spawnString = spawn.name + spawnLocation + spawnRotation;
                sw.WriteLine(spawnString);
                sw.WriteLine(spawn.name);


                //Goal
                GameObject goal = GameObject.FindGameObjectWithTag("Goal");
                string goalString = "";
                Vector3 goalPos = goal.transform.position;
                string goalLocation = " " + goalPos.x + " " + goalPos.y + " " + goalPos.z + " ";
                Vector3 goalRot = goal.transform.rotation.eulerAngles;
                string goalRotation = goalRot.x + " " + goalRot.y + " " + goalRot.z;

                goalString = goal.name + goalLocation + goalRotation;
                sw.WriteLine(goalString);
                sw.WriteLine(goal.name);


                //close the file
                sw.Close();

                this.gameObject.GetComponent<LoadLevel>().path = path;
                this.gameObject.GetComponent<LoadLevel>().play = true;
            }

        }
    }
}
