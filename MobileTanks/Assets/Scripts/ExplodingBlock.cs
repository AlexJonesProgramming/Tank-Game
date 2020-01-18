using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBlock : MonoBehaviour
{
    public bool hit = false;
    public GameObject largeExplosion;

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            GameObject lx = GameObject.Instantiate(largeExplosion);
            lx.transform.position = this.transform.position;
            lx.transform.rotation = this.transform.rotation;
            Destroy(lx, 3);
            Destroy(this.gameObject);
        }
    }
}
