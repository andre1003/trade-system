using System.Collections;
using UnityEngine;


public class Coin : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Try to get PlayerController component from collision
        PlayerController controller;
        bool hasComponent = collision.gameObject.TryGetComponent(out controller);

        // If success
        if(hasComponent)
        {
            // Add coin and play coin clip
            controller.AddCoins(1);
            audioSource.Play();

            // Set to invisible and not interactable, just for visual purpose
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            // Destroy after 0.5 second (needed, once the audio clip stops if the coin is destroyed)
            StartCoroutine(WaitToDestroy(0.5f));
        }
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
