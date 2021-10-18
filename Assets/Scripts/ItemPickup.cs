using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // When the player collides with the Item add it to the player items
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Player.addItem(gameObject.transform.name);
            Destroy(gameObject);
        }
    }
}
