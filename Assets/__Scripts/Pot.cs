using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public LootTable thisLoot;
    private Animator anim; //reference to animator

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();//getting start animator
	}
	
	private void MakeLoot()
    {
        if(thisLoot !=null)
        {
            PowerUp current =thisLoot.LootPowerUp();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }


    public void Smash()//smash method to smash pot
    {
        anim.SetBool("smash", true);//setting the boolean smash to true inorder to smash pot
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()//making pot inactive after smashing
    {
        yield return new WaitForSeconds(.3f);//wait for 0.3f before next action
        this.gameObject.SetActive(false);//make pot invisible after smashing
        MakeLoot();
    }
}
