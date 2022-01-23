using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private SceneMoveType sceneMoveType = SceneMoveType.Forward;
    public void Execute() 
    {
        switch (sceneMoveType) 
        {
            case SceneMoveType.Quit:
                Application.Quit();
                break;
            default:
                int mod = sceneMoveType == SceneMoveType.Forward ? 1 : -1;
                int nextScene = SceneManager.GetActiveScene().buildIndex + mod;
                SceneManager.LoadScene(nextScene);
                break;
        }
    }

    
}

public enum SceneMoveType
{
    Forward,
    Backwards,
    Quit
}