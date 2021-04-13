using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class to check the bounds of the camera
public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 10; //how big the object is
    public bool keepOnScreen; //if you want to keep the object on the screen

    //camera dimensions
    public float camWidth;
    public float camHeight;

    public float xpos;
    public float ypos;

    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown; //what direction it moved off on

    //checks if the object is on the screen
    public bool isOnScreen
    {
        get
        {
            return !(offRight || offLeft || offUp || offDown); //as long as it didn't move off in any direction
        }
    }

    //set the camera dimensions
    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        xpos = Camera.main.transform.position.x;

        ypos = Camera.main.transform.position.y;


    }

    private void LateUpdate()
    {

        Vector2 pos = transform.position;
        pos.x -= xpos;
        pos.y -= ypos;
        offRight = offLeft = offUp = offDown = false; //initiate the booleans

        if (pos.x> camWidth - radius) //if it's off the right
        {
            pos.x = camWidth - radius; //max position
            offRight = true;
        }

        if (pos.x  < -camWidth + radius) //if it's off the left
        {
            pos.x = -camWidth + radius; //max position
            offLeft = true;
        }

        if (pos.y > camHeight - radius) //if it's off the top
        {
            pos.y = camHeight - radius; //max position
            offUp = true;
        }

        if (pos.y < -camHeight + radius) //if it's off the bottom
        {
            pos.y = -camHeight + radius; //max position
            offDown = true;
        }

        if (keepOnScreen && !isOnScreen) //if it's off the screen but you wanted it on the screen
        {
            pos.x += xpos;
            pos.y += ypos;
            transform.position = pos; //reset the position
            offRight = offLeft = offUp = offDown = false; //incase just so it's not false in-between update frames
        }
    }

    //draw the bounds in the scene pane using onDrawGizmos()
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
