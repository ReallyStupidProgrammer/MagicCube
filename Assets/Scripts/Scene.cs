using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour {

    public void menu() {
        SceneManager.LoadSceneAsync(0);
    }

    public void play() {
        SceneManager.LoadSceneAsync(1);
    }

    public void settings() {
        SceneManager.LoadSceneAsync(3);
    }

    public void help() {
        SceneManager.LoadSceneAsync(4);
    }

    public void quit() {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
