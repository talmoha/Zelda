using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy//inherits enemy class behavior
{
    private Rigidbody2D myRigidbody;//reference to rigidbody of log
    public Transform target;//target's (player) location variable 
    public float chaseRadius;//inside the radius is where log chases the player
    public float attackRadius;//inside the radius is where log attacks the player
    public Transform homePosition;//the original sleeping location of the log that it will restart to when player is outside its radius
    public Animator anim;//animator of log


	// Use this for initialization
	void Start () {
        currentState = EnemyState.idle;//starting the log in idle state
        myRigidbody = GetComponent<Rigidbody2D>();//refrence to rigibody component
        anim = GetComponent<Animator>();//setting the animator component anim
        target = GameObject.FindWithTag("Player").transform;//find the location of the target with the tag "player" 
	}
	

    //calling check distance function to check if player is within log's radius
	void FixedUpdate () {
        CheckDistance();
	}

    public virtual void CheckDistance() //find distance from log to target
    {
        //if the distance between the log and player is less than or equal to the chase radius and the distance between the log and player is less than the attack radius
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position,transform.position) > attackRadius)
        {
            //we dont want the log to move in stagger or attack state
            //and if current state is idle or walk and the state is not stagger
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                //move position of log towards the target (player) with the given speed of the log
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);//move towards takes 3 parameters. the first is the log's position, the second is the target that the log is moving towards (player), and the third is the movement at frames per second (moveSpeed * Time.deltaTime)
                
                changeAnim(temp-transform.position);//changing animation by the difference in position from player to log
                myRigidbody.MovePosition(temp);//change the log's position to the temporary variable "temp" made earlier

                ChangeState(EnemyState.walk);//change the state to make the log walk
                anim.SetBool("wakeUp", true); //setting wakeUp boolean to be true in order to make the log wake up
            }
        }else if (Vector3.Distance(target.position, transform.position) > chaseRadius ){
            anim.SetBool("wakeUp", false);
        }
    }

    public void SetAnimFloat(Vector2 setVector)//function for setting animator
    {
        anim.SetFloat("moveX",setVector.x);//move x 
        anim.SetFloat("moveY",setVector.y);//move y
    }

    public void changeAnim(Vector2 direction)//changing animation of the log according to where the player is moving
    {   
        ////check if x or y part of the direction (magnitude) is bigger
        if(Mathf.Abs(direction.x)> Mathf.Abs(direction.y))//if player's x's magnitude of movement is bigger
        {
            if (direction.x>0)
            {
                SetAnimFloat(Vector2.right);//set moveX to the right with the player 
            }else if (direction.x<0)
            {
                SetAnimFloat(Vector2.left);
            }
        }else if (Mathf.Abs(direction.x)< Mathf.Abs(direction.y))//if y's magnitude is bigger 
        {
            if (direction.y>0)
            {
                SetAnimFloat(Vector2.up);
            }else if (direction.y<0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    public void ChangeState(EnemyState newState){//function for changing state to passed state
        if(currentState != newState)//check if passed state is same as current state
        {
            currentState = newState;//if not, then make passed state the new state
        }
    }
}