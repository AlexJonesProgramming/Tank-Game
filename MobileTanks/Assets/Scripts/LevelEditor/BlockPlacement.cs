using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockPlacement : MonoBehaviour
{

    private int blockNumber = 0, typeNumber = 0;
    private int rotation = 0;

    public Material[] materials;
    public GameObject[] placementBlocks;
    public GameObject[] woodBlocks;
    public GameObject[] explosiveBlocks;
    public GameObject[] bouncyBlocks;
    public GameObject[] physicsBlocks;
    public GameObject[] collectableBlocks;

    private GameObject block;

    private bool snapping = false;
    public GameObject snapColider;
    public Material invisableMaterial;
    private GameObject snapBlock;


    private int placementMode = 0;
    public GameObject spawn, enemyPlacement, goal, enemy;
    private Vector3 spawnLastPos, goalLastPos;
    private Quaternion spawnLastRot, goalLastRot;

    // Start is called before the first frame update
    void Start()
    {
        block = GameObject.Instantiate(placementBlocks[blockNumber]);
        ChangeMaterial();

        spawnLastPos = spawn.transform.position;
        goalLastPos = goal.transform.position;
        spawnLastRot = spawn.transform.rotation;
        goalLastRot = goal.transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {

        if (placementMode == 0)
        {
            //place the block under the mouse
            if (typeNumber != -1)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    block.transform.position = new Vector3(hit.point.x, hit.point.y + block.GetComponent<PlacementBlock>().offset, hit.point.z);
                    snapColider.transform.position = hit.point;
                }

                if (snapping)
                {
                    GetSnapBlock();
                    if (snapBlock != null)
                        Snap();
                }
            }


            //turn snapping on/off
            if (Input.GetKeyDown(KeyCode.Space))
            {
                snapping = !snapping;

                if (!snapping)
                    Destroy(snapBlock);
            }


            // Change the block Type
            if (Input.GetKeyDown(KeyCode.W))
            {
                typeNumber += 1;
                if (typeNumber >= materials.Length)
                    typeNumber -= materials.Length + 1;
                if (typeNumber != -1) // we do this if/else because change block has the code to remove the current placement block
                    ChangeMaterial();
                else
                    ChangeBlock();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                typeNumber -= 1;
                if (typeNumber < -1)
                    typeNumber += materials.Length + 1;
                if (typeNumber != -1) // we do this if/else because change block has the code to remove the current placement block
                    ChangeMaterial();
                else
                    ChangeBlock();

            }


            //Change the block shape
            if (Input.GetKeyDown(KeyCode.D))
            {
                blockNumber += 1;
                if (blockNumber >= placementBlocks.Length)
                    blockNumber -= placementBlocks.Length;
                ChangeBlock();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                blockNumber -= 1;
                if (blockNumber < 0)
                    blockNumber += placementBlocks.Length;
                ChangeBlock();
            }


            //place and pick up blocks
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (typeNumber == -1)
                {
                    PickUpBlock();
                }
                else
                    PlaceBlock();
            }


            //rotate and delete blocks
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (typeNumber == -1)
                {
                    DeleteBlock();
                }
                else
                    RotateBlock();
            }
        }

        if (placementMode == 1)
        {
            //place the block under the mouse
            if (typeNumber != -1)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    block.transform.position = new Vector3(hit.point.x, hit.point.y + block.GetComponent<PlacementBlock>().offset, hit.point.z);
                }
            }

            // change what you are placing
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (blockNumber == 0)
                {
                    spawn.transform.position = spawnLastPos;
                    block.transform.rotation = spawnLastRot;
                }
                else if (blockNumber == 2)
                {
                    block.transform.position = goalLastPos;
                    block.transform.rotation = goalLastRot;
                }

                blockNumber += 1;
                if (blockNumber > 2)
                    blockNumber = 0;
                ChangeBlock();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (blockNumber == 0)
                {
                    spawn.transform.position = spawnLastPos;
                    block.transform.rotation = spawnLastRot;
                }
                else if (blockNumber == 2)
                {
                    block.transform.position = goalLastPos;
                    block.transform.rotation = goalLastRot;
                }

                blockNumber -= 1;
                if (blockNumber < 0)
                    blockNumber = 2;
                ChangeBlock();
            }

            //rotate block
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                RotateBlock();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    
                }
                else if (blockNumber == 0)
                {
                    blockNumber = 1;
                    spawnLastPos = block.transform.position;
                    spawnLastRot = block.transform.rotation;
                    ChangeBlock();
                }
                else if (blockNumber == 1)
                {
                    GameObject GO = GameObject.Instantiate(enemy);
                    GO.transform.position = block.transform.position;
                    Vector3 oldRot = GO.transform.rotation.eulerAngles;
                    GO.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
                }
                else if (blockNumber == 2)
                {
                    blockNumber = 1;
                    goalLastPos = block.transform.position;
                    goalLastRot = block.transform.rotation;
                    ChangeBlock();
                }
            }
        }
    }

    private void ChangeMaterial()
    {
        if (block == null)
            ChangeBlock();
        if (block != null)
            block.GetComponent<Renderer>().material = materials[typeNumber];
    }

    private void ChangeBlock()
    {
        if (typeNumber != -1)
        {
            if (placementMode == 0)
            { 

                //handle placing the goal / spawn if we change placements modes
                if(block == spawn)
                {
                    block.transform.position = spawnLastPos;
                    block.transform.rotation = spawnLastRot;
                    block = null;
                }
                else if (block == goal)
                {
                    block.transform.position = goalLastPos;
                    block.transform.rotation = goalLastRot;
                    block = null;
                }

                if (block != null)
                    Destroy(block);
                GameObject GO = GameObject.Instantiate(placementBlocks[blockNumber]);
                block = GO;
                ChangeMaterial();

                Vector3 oldRot = block.transform.rotation.eulerAngles;
                block.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
            }
            else
            {
                if (blockNumber == 0)
                {
                    if (block != spawn && block != goal)
                        Destroy(block);
                    block = spawn;
                    Vector3 oldRot = block.transform.rotation.eulerAngles;
                    block.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
                }
                if (blockNumber == 1)
                {
                    if (block != spawn && block != goal)
                        Destroy(block);
                    block = GameObject.Instantiate(enemyPlacement);
                    Vector3 oldRot = block.transform.rotation.eulerAngles;
                    block.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
                }
                if (blockNumber == 2)
                {
                    if (block != spawn && block != goal)
                        Destroy(block);
                    block = goal;
                    Vector3 oldRot = block.transform.rotation.eulerAngles;
                    block.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
                }

            }
        }
        else
        {
            Destroy(block);
        }
    }

    private void PlaceBlock()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            GameObject GO;

            switch (typeNumber)
            {
                case 0:
                    GO = GameObject.Instantiate(woodBlocks[blockNumber]);
                    break;
                case 1:
                    GO = GameObject.Instantiate(explosiveBlocks[blockNumber]);
                    break;
                case 2:
                    GO = GameObject.Instantiate(bouncyBlocks[blockNumber]);
                    break;
                case 3:
                    GO = GameObject.Instantiate(physicsBlocks[blockNumber]);
                    break;
                case 4:
                    GO = GameObject.Instantiate(collectableBlocks[blockNumber]);
                    break;
                default:
                    GO = GameObject.Instantiate(woodBlocks[blockNumber]);
                    break;
            }

            GO.transform.position = block.transform.position;
            Vector3 oldRot = GO.transform.rotation.eulerAngles;
            GO.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
            GO.name = blockNumber.ToString() + " " + typeNumber.ToString();
        }
    }

    private void RotateBlock()
    {
        rotation += 45;
        rotation += rotation % 45; //just incase it gets changed picking blocks up


        if (rotation >= 360)
            rotation = 0;

        Vector3 oldRot = block.transform.rotation.eulerAngles;
        block.transform.rotation = Quaternion.Euler(oldRot.x, rotation, oldRot.z);
    }

    private void PickUpBlock()
    {
        GameObject GO = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GO = hit.collider.gameObject;
        }

        if (GO != null)
        {
            if (GO.tag != "Floor")
            {
                string[] strings = GO.name.Split(' ');
                blockNumber = int.Parse(strings[0]);
                typeNumber = int.Parse(strings[1]);
                rotation = (int)GO.transform.rotation.eulerAngles.y;
                DeleteBlock();
                ChangeBlock();
                ChangeMaterial();
            }
        }
    }

    private void GetSnapBlock()
    {
        GameObject GO = snapColider.GetComponent<SnapCollider>().GetClosest();

        if (GO != null)
        {
            string[] strings = GO.name.Split(' ');

            if (snapBlock != null)
                Destroy(snapBlock);

            snapBlock = GameObject.Instantiate(placementBlocks[int.Parse(strings[0])]);
            snapBlock.GetComponent<Renderer>().material = invisableMaterial;
            snapBlock.transform.position = GO.transform.position;
            snapBlock.transform.rotation = GO.transform.rotation;
        }
        else
        {
            Destroy(snapBlock);
        }
    }

    private void Snap()
    {
        Transform[] mySnaps = new Transform[block.transform.childCount];
        Transform[] otherSnaps = new Transform[snapBlock.transform.childCount];

        //fill the arrays
        for (int i = 0; i < block.transform.childCount; i++)
        {
            mySnaps[i] = block.transform.GetChild(i);
        }
        for (int i = 0; i < snapBlock.transform.childCount; i++)
        {
            otherSnaps[i] = snapBlock.transform.GetChild(i);
        }

        Transform myClosest = null;
        Transform otherClosest = null;
        float closestDistance = 100;

        //find the closest node
        foreach (Transform mt in mySnaps)
        {
            foreach (Transform ot in otherSnaps)
            {
                float distance = Mathf.Abs(Vector3.Distance(ot.position, mt.position));
                if(distance < closestDistance)
                {
                    myClosest = mt;
                    otherClosest = ot;
                    closestDistance = distance;
                }
            }
        }

        //move the placement block into posistion
        Vector3 toMySnap = block.transform.position - myClosest.position;

        Vector3 newPosition = otherClosest.position + toMySnap;//  - toOtherSnap + toMySnap;

        block.transform.position = newPosition;
    }

    private void DeleteBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject GO = hit.collider.gameObject;
            if (GO.tag != "Floor")
                Destroy(GO);
        }
    }

    public void SetBlockType(int i)
    {
        typeNumber = i;
        ChangeBlock();
        if(i != -1)
            ChangeMaterial();
        
    }

    public void SetBlock(int i)
    {

        if (placementMode == 1)
        {
            if (blockNumber == 0)
            {
                block.transform.position = spawnLastPos;
                block.transform.rotation = spawnLastRot;
            }
            else if (blockNumber == 2)
            {
                block.transform.position = goalLastPos;
                block.transform.rotation = goalLastRot;
            }

        }

        blockNumber = i;
        ChangeBlock();
    }

    public void SetPlacementMode(int i)
    {
        placementMode = i;
        if (placementMode == 1)
            typeNumber = 0;
        ChangeBlock();
    }
}
