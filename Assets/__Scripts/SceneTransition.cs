using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour//script for scene transition from main scene to earth boss battle scene
{
    public string sceneToLoad;//what scene to load when player goes in trigger area
    public Vector2 playerPosition;
    public VectorValue playerStorage;//temp value to store initial position of player

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)//if player is in trigger area
        {
            playerStorage.initialValue=playerPosition;//storing player position in temp value
            SceneManager.LoadScene(sceneToLoad);//load main scene
        }
    }
}
