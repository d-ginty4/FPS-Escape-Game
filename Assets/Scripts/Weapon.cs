using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera camera;
    public ParticleSystem flash;
    public float range = 100f;
    public int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   // use left click to shoot
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // shoot using raycasting
        RaycastHit hit;
        try
        {
            flash.Play();
            Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

            //if the enemy was hit take health from the enemy equaling the guns damage
            if (hit.transform.name.Contains("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        catch(NullReferenceException e){}
    }
}
