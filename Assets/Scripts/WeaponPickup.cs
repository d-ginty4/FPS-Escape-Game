using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // When the player collides with the Gun add it to the players usable guns
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            var weapon = gameObject.transform.GetChild(0);
            WeaponsController.addWeapon(weapon.name);
            Destroy(gameObject);
        }
    }
}
