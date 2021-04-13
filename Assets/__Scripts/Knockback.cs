using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {//for when player attacks and knocks back or breaks things

    public float thrust;//the force the character knocks things back
    public float knockTime;//time to delay knockback
    public float damage;//the damage the enemy does

    private void OnTriggerEnter2D(Collider2D other)//checking if enemy is in trigger area
    {
        //if the object has a tag "breakable"
        if(other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))//if the object that is being hit is breakable and the player is the one breaking it
        {
            other.GetComponent<Pot>().Smash();//smash the pot
        }
        //if the object has a tag "enemy" or "player"
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();//declaring a regidbody for the object being hit
            if(hit != null)//if hit is a rigidbody (it's not null)
            {
                Vector2 difference = hit.transform.position - transform.position;//the difference between the enemy's location (hit) and player
                difference = difference.normalized * thrust;//normalize the difference (making it have a length of 1) times the thrust
                hit.AddForce(difference, ForceMode2D.Impulse);//adding a force to the enemy (hit). The force added is the "difference" and the force mode is the impulse of Forcemode2D
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)//if tag is enemy amd if player hits the triggered
                {
                    //if it's the enemy, we put it into stagger state
                    hit.GetComponent<Enemy>().currentState=EnemyState.stagger;//making the current state of the enemy stagger
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);//initiate knockback and let each enemy keep track of knockbacks
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    //if the tag is the player tag
                    if ((other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)&&(other.GetComponent<PlayerMovement>().currentState !=PlayerState.shield ))//if current player state is not stagger then knockback happens
                    {    hit.GetComponent<PlayerMovement>().currentState=PlayerState.stagger;//making the current state of the player stagger when hitting
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);//let each enemy keep track of knockbacks
                        
                    }
                }
            }
        }
    }
}
