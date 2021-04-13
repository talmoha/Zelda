using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{//using enum type for different states of the enemeies
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour {//script for all enemies of player

    public EnemyState currentState;//using enum by making reference to state
    public FloatValue maxHealth;//max health enemy can have
    public float health; //health points of enemy
    public string enemyName;//name of enemy
    public int baseAttack;//the attack for the enemy
    public float moveSpeed;//moving speed of enemy 
    public GameObject deathEffect;//death effect object for enemy death
    public GameObject endGame;//making hidden objects appear when enemy is dead
    public GameObject transition;//transition to next level
    public GameObject wonText; //text that apppears at the end of level

    private void Awake()
    {
        health=maxHealth.initialValue;//setting health to max at beginning
        transition.SetActive(false);//keeping transition to next level invisible for now
        wonText.SetActive(false);//keeping text at the end of the level invisible for now
    }

    private void TakeDamage(float damage)//for accounting for the damage that is inflicted on it
    {
        health -= damage;//updating health
        if (health<=0)//to make sure health does not go to negative
        {
            DeathEffect();//calling death effect for when the health of the enemy finishes
            this.gameObject.SetActive(false);//making object dissapear when all health is done
            endGame.SetActive(true);//making objects appear when level over
            transition.SetActive(true);//making transition into next level appear
            wonText.SetActive(true);//making text at the end of the level appear
        }
    }

    private void DeathEffect()//death effect of fire when enemy dies
    {
        if(deathEffect!=null)//if the enemy has a death effect
        {
            GameObject effect =Instantiate(deathEffect, transform.position, Quaternion.identity);//instantiate death affect according to enemy position 
            Destroy(effect, 1f);//destroy effect after 1f second 
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)//function calling knock coroutine.Takes in two passed arguments, the rigidbody and knockTime
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));//start routine in function below
        TakeDamage(damage);//call takdamage function to account for damage
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)//coroutine of knockback of the enemy rigidbody with two passed arguments, the rigidbody and knockTime
    {
        if (myRigidbody != null)//if enemy is not dead and is not in stagger state
        {
            yield return new WaitForSeconds(knockTime);//wait for knocktime to finish
            myRigidbody.velocity = Vector2.zero;//set velocity of enemy to zero
            currentState = EnemyState.idle;//male state be idle
            myRigidbody.velocity = Vector2.zero;//resetting the enemy state to idle
        }
    }
}

