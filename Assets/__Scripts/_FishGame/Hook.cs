using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{

    private bool isHooked =false;
    public GameObject fishSprite;


    public bool Hooked
    {
        get { return isHooked; }
        set { isHooked = value; 
            if (!isHooked)
            {
                fishSprite.SetActive(false);
            }
        
        }   
    }




    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, worldPosition.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "fish" && !isHooked)
        {

            Destroy(other.gameObject);
            isHooked = true;
            fishSprite.SetActive(true);
        }
        if (other.tag == "shark")
        {
            MainController.S.LooseHP();
        }
    }

}
