using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    // Audio source
    public AudioSource audioSource;

    // Health amount to restore
    public float healthToRestore = 10f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get PlayerController component from collision
        PlayerController controller;
        bool hasComponent = collision.gameObject.TryGetComponent(out controller);

        // If failed, exit
        if(!hasComponent)
        {
            return;
        }

        // If player is full life, exit
        if(controller.GetHealth() == controller.baseHealth)
        {
            return;
        }

        // Restore health and play heal clip
        controller.RestoreHealth(healthToRestore);
        audioSource.Play();

        // Set to invisible and not interactable, just for visual purpose
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // Destroy after 0.5 second (needed, once the audio clip stops if the coin is destroyed)
        StartCoroutine(WaitToDestroy(0.5f));
    }


    /// <summary>
    /// Wait some seconds and than destroy this game object.
    /// </summary>
    /// <param name="seconds">Time to wait.</param>
    private IEnumerator WaitToDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}
