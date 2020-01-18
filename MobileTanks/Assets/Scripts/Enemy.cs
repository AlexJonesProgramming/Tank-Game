using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject tankTop, tankBottom;
    public float fireRate = 5.5f;
    private float timer = 0.0f;
    private Transform player;


    //bullet firing
    public Transform bulletSpawn;
    public GameObject bullet, explosion, largeExplosion;
    public float bulletSpeed = 30;

    public bool hit = false;

    public bool CanMove = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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

        Vector3 lookAtTarget = player.position;
        lookAtTarget.y = transform.position.y;
        tankTop.transform.LookAt(lookAtTarget);

        if(CanMove)
            timer += Time.deltaTime;
        if(timer > fireRate)
        {
            timer = 0;
            FireBullet();
        }
        
    }


    void FireBullet()
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
