using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool goalRached = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            goalRached = true;
        }
    }
}
