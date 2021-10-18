using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    static LinkedList<GameObject> weapons = new LinkedList<GameObject>();
    static LinkedListNode<GameObject> currentWeapon;
    static Weapon[] weaponsArray;

    // Start is called before the first frame update
    void Start()
    {
        // create the array of weapons attached to the player
        weaponsArray = FindObjectsOfType<Weapon>();

        // disable all the weapons in the array as they cant be used while there here
        foreach(Weapon w in weaponsArray)
        {
            w.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // change weapon with a right click
        if (Input.GetMouseButtonDown(1))
        {
            ChangeWeapon();
        }
    }
    
    // used to set the weapon sent in as the active one
    private static void SetActiveWeapon(LinkedListNode<GameObject> weapon)
    {
        // disable all weapons 
        foreach (GameObject w in weapons)
        {
            w.SetActive(false);
        }

        // enable the desired one
        weapon.Value.SetActive(true);
    }

    private void ChangeWeapon()
    {
        // check if there is guns available
        if(currentWeapon == null) { return;}

        // set the current gun to the next one in the linked list
        currentWeapon = currentWeapon.Next;

        // check if there is no next weapon, if so go back to the ehad of the ist
        if(currentWeapon == null)
        {
            currentWeapon = weapons.First;
        }

        // activate the current weapon
        SetActiveWeapon(currentWeapon);
    }

    // called by weapon pickup script
    public static void addWeapon(String weapon)
    {
        // cycle through all possible guns
        foreach(Weapon w in weaponsArray)
        {
            // if the pickup matches one of the guns add it to the list of usable guns
            if(w.name == weapon)
            {
                weapons.AddLast(w.gameObject);
                currentWeapon = weapons.Find(w.gameObject);
                SetActiveWeapon(currentWeapon);
            }
        }
    }
}
