using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    // Next scene is Battle
    public bool toBattle = true;


    /// <summary>
    /// Load Battle scene.
    /// </summary>
    public void LoadBattle()
    {
        StartCoroutine(LoadAsync("Battle"));
    }

    /// <summary>
    /// Load MainScene scene.
    /// </summary>
    public void LoadMainScene()
    {
        StartCoroutine(LoadAsync("MainScene"));
    }


    /// <summary>
    /// Load scene async.
    /// </summary>
    /// <param name="scene">Scene to load.</param>
    private IEnumerator LoadAsync(string scene)
    {
        // Start loading scene
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(scene);

        // While is not loaded, wait
        while(!loadAsync.isDone)
        {
            yield return null;
        }
    }

    // On main scene, player can change scene passing through a portal
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If destination is Battle scene, load battle
        if(toBattle)
        {
            LoadBattle();
        }

        // Else, load MainScene
        else
        {
            LoadMainScene();
        }
    }
}
