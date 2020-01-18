using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBlock : MonoBehaviour
{
    public float force = 1000, radius = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getHit(Vector3 hitPos)
    {
        GetComponent<Rigidbody>().AddExplosionForce(force, hitPos, radius);
    }
}
