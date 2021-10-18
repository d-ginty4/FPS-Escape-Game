using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public static int playerHealth = 100;
    Rigidbody character;
    public static bool moveable = false;    //Player is not allowed move initally

    // Items will be used to hold the keys the player has to collect
    public static ArrayList items = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
        PlayerDeath();
    }

    private void PlayerDeath()
    {
        // If the players health runs out call the gameManagers DeadUI method which stops the game
        if(playerHealth <= 0)
        {
            FindObjectOfType<GameManager>().DeadUI();
        }
    }

    private void ControlPlayer()
    {
        // if the player is allowed move wait for inputs
        // can control with WASD or arrow keys
        if (moveable)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                character.velocity = transform.forward * moveSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                character.velocity = -transform.forward * moveSpeed;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                character.velocity = -transform.right * moveSpeed;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                character.velocity = transform.right * moveSpeed;
            }
        }
    }

    // Method used to add to the list of player items
    // will be called by item pickup script
    public static void addItem(String item)
    {
        items.Add(item);
    }
}
