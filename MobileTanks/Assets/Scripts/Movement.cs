using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    //player movement
    public float speed = 10.0f;
    public Joystick joystick;
    public GameObject tankTop, tankBottom;
    private Rigidbody rb;
    private AudioSource engineNoise;

    public bool CanMove = true;


    //bullet firing
    public Transform bulletSpawn;
    public GameObject bullet, explosion, largeExplosion;
    public float bulletSpeed = 30;

    public int health = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineNoise = GetComponent<AudioSource>();
    }


    void Update()
    {
        //Input
        float jh = Input.GetAxis("Horizontal");//joystick.Horizontal;
        float jv = Input.GetAxis("Vertical");//joystick.Vertical;

        //movement
        if (CanMove)
            rb.velocity = new Vector3(jh, 0, jv) * speed;
        else
            rb.velocity = new Vector3(0, 0, 0);

        //TankNoise
        engineNoise.pitch = 1 + ((Mathf.Abs(jh) + Mathf.Abs(jv)) * 0.4f);

        //direction and firing
        if (jv != 0 || jh != 0)
        {
            Vector3 LookAtPoint = new Vector3(jh,0,jv) * 10;
            LookAtPoint += transform.position;
            tankBottom.transform.LookAt(LookAtPoint);

            if (true)//Input.touchCount > 1)
            {
                Vector3 lookAtTarget;
                if (Input.GetMouseButtonDown(0))//Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// Input.GetTouch(1).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        lookAtTarget = hit.point;

                        lookAtTarget.y = tankTop.transform.position.y;

                        tankTop.transform.LookAt(lookAtTarget);

                        FireBullet();
                    }
                }
            }

        }
        else if (Input.GetMouseButtonDown(0))//Input.GetTouch(1).phase == TouchPhase.Ended)
        {
            Vector3 lookAtTarget;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);// Input.GetTouch(1).position);
            if (Physics.Raycast(ray, out hit))
            {
                lookAtTarget = hit.point;

                lookAtTarget.y = tankTop.transform.position.y;

                tankTop.transform.LookAt(lookAtTarget);

                FireBullet();
            }
        }
        
        //+!+!+!+!+!
        // Remove the above else is and replace it with the botom else if for mobile

        /*else if (Input.touchCount > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 lookAtTarget;
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        lookAtTarget = hit.point;

                        lookAtTarget.y = tankTop.transform.position.y;

                        tankTop.transform.LookAt(lookAtTarget);

                        FireBullet();
                    }
                }
            }
        }*/
    }


    void FireBullet()
    {
        if (CanMove)
        {
            GameObject ex = GameObject.Instantiate(explosion);
            GameObject bl = GameObject.Instantiate(bullet);

            ex.transform.position = bulletSpawn.position;
            bl.transform.position = bulletSpawn.position;

            ex.transform.rotation = bulletSpawn.rotation;
            bl.transform.rotation = bulletSpawn.rotation;

            bl.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;

            Destroy(ex, 3);
        }
    }

}
