using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;//reference to what camera follows
    public float smoothing;//amount of distance we want to cover for Laerp function
    public Vector2 maxPosition;//positions for bounding the camera to scene
    public Vector2 minPosition;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position=new Vector3(target.position.x,target.position.y,transform.position.z);//smooth camera transition between scenes
    }

    // Update is called once per frame
    //camera goes to player in late update because we dont want camera to move before player does
    void LateUpdate()
    {
        if (transform.position!=target.position)
        {
            Vector3 targetPosition=new Vector3(target.position.x,target.position.y,transform.position.z);//so the z-position is independant to camera
            
            targetPosition.x=Mathf.Clamp(targetPosition.x,minPosition.x, maxPosition.x);//setting camera boundaries with min and max boundaries assigned and clamp function
            targetPosition.y=Mathf.Clamp(targetPosition.y,minPosition.y, maxPosition.y);

            transform.position=Vector3.Lerp(transform.position,targetPosition,smoothing);//making camera movement smooth


        }
    }
}
