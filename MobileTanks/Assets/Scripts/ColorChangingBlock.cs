using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingBlock : MonoBehaviour
{

    public Material brown;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColor()
    {
        if (!hit)
        {
            GetComponent<Renderer>().material = brown;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().blueBlocks -= 1;
            hit = true;
        }
    }
}
