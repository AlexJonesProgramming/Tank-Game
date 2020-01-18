using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCollider : MonoBehaviour
{
    //The list of colliders currently inside the trigger
    public List<Collider> triggerList = new List<Collider>();
    private GameObject closest;
 //called when something enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Floor" && other.tag != "Goal" && other.tag != "Player" && other.tag != "Enemy")
        {
            //if the object is not already in the list
            if (!triggerList.Contains(other))
            {
                //add the object to the list
                triggerList.Add(other);
            }
        }
    }

    //called when something exits the trigger
    void OnTriggerExit(Collider other)
    {
        if (other.tag != "Floor" && other.tag != "Goal" && other.tag != "Player" && other.tag != "Enemy")
        {
            //if the object is in the list
            if (triggerList.Contains(other))
            {
                //remove it from the list
                triggerList.Remove(other);
            }
        }
    }

    private void FixedUpdate()
    {
        float shortestDistance = 100.0f;
        GameObject GO = null;
        for (int i = 0; i < triggerList.Count; i++)
        {
            float distance = Mathf.Abs(Vector3.Distance(triggerList[i].transform.position, this.transform.position));
            if ( distance < shortestDistance)
            {
                shortestDistance = distance;
                GO = triggerList[i].gameObject;
            }
        }

        closest = GO;
    }

    public GameObject GetClosest()
    {
        return closest;
    }
}
