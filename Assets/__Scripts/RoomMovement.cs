using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//to access text object

public class RoomMovement : MonoBehaviour
{
    public Vector2 cameraChange;//how much camera is moving
    public Vector3 playerChange;//how much player is moving
    private CameraMovement cam;//refernce to camera
    public bool needText; //to determine which rooms need text
    public string placeName;//what the text is 
    public GameObject text;//the reference to the card object 
    public Text placeText;//refrence to text object on card 

    // Start is called before the first frame update
    void Start()
    {
        cam=Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)//
    {
        if (other.CompareTag("Player") && !other.isTrigger)//check tag of variable other, to check player is in trigger zone
        {
            cam.minPosition+=cameraChange;
            cam.maxPosition+=cameraChange;
            other.transform.position+= playerChange;
            if (needText)//if room needs a text 
            {
                StartCoroutine(placeNameSet());
            }
        }
    }

    private IEnumerator placeNameSet() //method to place text on scene using IEnumerator which  a method running in parallel to other processes
    {
        text.SetActive(true); //making the text appear
        placeText.text=placeName;//changing what the text is 
        yield return new WaitForSeconds(4f);//waiting time before text dissapears
        text.SetActive(false);//making text not appear 
    }

}
