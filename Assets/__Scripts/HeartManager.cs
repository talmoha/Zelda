using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;//hearts array
    public Sprite fullHeart;//declaring sprites for states of the heart (full, half full, empty)
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;//container of hearts
    public FloatValue playerCurrentHealth;//player health progress

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();//call method to fill hearts at the start 
    }

    public void InitHearts()//looping through hearts container to fill hearts 
    {
        for (int i=0;i<heartContainers.initialValue;i++)
        {
            hearts[i].gameObject.SetActive(true);//making hearts appear one by one 
            hearts[i].sprite=fullHeart;//making the heart appear as a full heart at first
        }
    }

    public void UpdateHearts()//updating the hearts of the player according to player health
    {
        float tempHealth=playerCurrentHealth.RuntimeValue/2;//temp health value which is the divided by two because each heart can be half full, so half a heart is 1 health point
        for (int i=0;i<heartContainers.initialValue;i++)//looping over heart container
        {
            if (i<= tempHealth-1)//if container has equal to or less than player health, then it is a full heart
            {
                hearts[i].sprite=fullHeart;
            }
            else if(i>= tempHealth)//if container has greater than player health, then it is a empty heart
            {
                hearts[i].sprite=emptyHeart;
            }
            else //it is a half full heart otherwise
            {
                hearts[i].sprite=halfFullHeart;
            }

        }
    }
}
