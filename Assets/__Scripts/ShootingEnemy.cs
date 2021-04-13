using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySeconds-=Time.deltaTime;
        if(fireDelaySeconds<=0)
        {
            canFire=true;
            fireDelaySeconds=fireDelay;
        }
    }

    public override void CheckDistance() //find distance from log to target
        {
            //if the distance between the log and player is less than or equal to the chase radius and the distance between the log and player is less than the attack radius
            if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position,transform.position) > attackRadius)
            {
                //we dont want the log to move in stagger or attack state
                //and if current state is idle or walk and the state is not stagger
                if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
                {
                    if (canFire)
                    {
                    Vector3 tempVector = target.transform.position-transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Launch(tempVector);
                    canFire=false;
                    ChangeState(EnemyState.walk);//change the state to make the log walk
                    anim.SetBool("wakeUp", true); //setting wakeUp boolean to be true in order to make the log wake up
                    }
                }
            }else if (Vector3.Distance(target.position, transform.position) > chaseRadius ){
                anim.SetBool("wakeUp", false);
            }
        }
}
