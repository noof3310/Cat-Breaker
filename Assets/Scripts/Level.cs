using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] int breakableBlocks; // for debugging purpose

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneLoader.LoadStartScene();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            FindObjectOfType<GameSession>().ToggleAutoPlay();
        }
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0) sceneLoader.LoadNextScene();
    }
}
