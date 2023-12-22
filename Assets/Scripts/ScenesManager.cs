using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake(){
        Instance = this;
    }

    public enum Scene{
        MainMenu,
        MathMenu,
        GeometryMenu,
        GraphMenu
    }
    public void LoadScene(Scene scene){
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
