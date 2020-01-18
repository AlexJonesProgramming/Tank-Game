using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 velLastFrame, velThisFrame;
    private void Update()
    {
        velLastFrame = velThisFrame;
        velLastFrame = GetComponent<Rigidbody>().velocity;
    }
    private void OnTriggerEnter(Collider other)
    {
        bool destroy = true;
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().hit = true;
        }
        else if (other.tag == "Player")
        {
            other.GetComponent<Movement>().health -= 34;
        }
        else if (other.tag == "ExplodingBlock")
        {
            other.GetComponent<ExplodingBlock>().hit = true;
        }
        else if (other.tag == "BouncyBlock")
        {
            destroy = false;
            Vector3 vel = GetComponent<Rigidbody>().velocity;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, vel, out hit))
            {
                Vector3 newDir = Vector3.Reflect(vel, hit.normal);
                GetComponent<Rigidbody>().velocity = newDir;
            }
            else
            {
                destroy = true;
                print("bounce failed");
            }

        }
        else if (other.tag == "MoveableBlock")
        {
            other.GetComponent<MoveableBlock>().getHit(this.transform.position);
        }
        else if (other.tag == "ColorChangingBlock")
        {
            other.GetComponent<ColorChangingBlock>().changeColor();
        }
        if(destroy)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        bool destroy = true;
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().hit = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().health -= 34;
        }
        else if (other.gameObject.tag == "ExplodingBlock")
        {
            other.gameObject.GetComponent<ExplodingBlock>().hit = true;
        }
        else if (other.gameObject.tag == "BouncyBlock")
        {
            destroy = false;
            Vector3 vel = velLastFrame;
            Vector3 newDir = Vector3.Reflect(vel, other.GetContact(0).normal);
            GetComponent<Rigidbody>().velocity = newDir.normalized * 30;
        }
        else if (other.gameObject.tag == "MoveableBlock")
        {
            other.gameObject.GetComponent<MoveableBlock>().getHit(this.transform.position);
        }
        else if (other.gameObject.tag == "ColorChangingBlock")
        {
            other.gameObject.GetComponent<ColorChangingBlock>().changeColor();
        }
        if (destroy)
            Destroy(this.gameObject);
    }
}
