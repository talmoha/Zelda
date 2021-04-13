using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishGameStart : MonoBehaviour
{

    public static bool loaded;
    public new GameObject camera;
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !loaded)
        {
            camera.SetActive(false);
            loaded = true;
            SceneManager.LoadScene("FishingGame", LoadSceneMode.Additive);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
    private void OnSceneUnloaded(Scene scene)
    {
        if (scene.name == "FishingGame")
        {
            camera.SetActive(true);
            loaded = false;
        }
    }

}
