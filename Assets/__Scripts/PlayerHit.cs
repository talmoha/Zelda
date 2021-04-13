using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)//assigning damage to what the player's hitbox hits
    {
        if(other.CompareTag("breakable"))//if the object that is being hit is breakable
        {
            other.GetComponent<Pot>().Smash();//smash the pot
        }
    }   
}
