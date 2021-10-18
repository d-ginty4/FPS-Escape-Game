using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    float yMove = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // find any x axis movement with the mouse and rotate the player accordingly
        float mouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        player.transform.Rotate(Vector3.up * mouseX);

        // find any y axis movement and move the camera accordingly
        // clamp the camera to stop the camera rotating too much
        float mouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;
        yMove -= mouseY;
        yMove = Mathf.Clamp(yMove, -45f, 45f);
        transform.localRotation = Quaternion.Euler(yMove, 0f, 0f);

        // check if the player crounchs
        Crouch();
    }

    private void Crouch()
    {
        // if the player is holding space lower the camera and change the height of the collider 
        if (Input.GetKey(KeyCode.Space))
        {
            transform.localPosition = (new Vector3(0, 0.5f, 0));
            GetComponentInParent<CapsuleCollider>().height = 1;
        }

        // if not holding space keep everything as normal
        else
        {
            transform.localPosition = (new Vector3(0, 1.5f, 0));
            GetComponentInParent<CapsuleCollider>().height = 1;
        }
    }
}
