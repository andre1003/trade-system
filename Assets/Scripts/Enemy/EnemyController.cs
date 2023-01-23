using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public int health = 1;
    public float damage = 10f;
    public float speed = 20f;

    public int coins = 1;

    public AudioSource audioSource;
    public AudioClip deathClip;
    public AudioClip damageClip;


    private GameObject player;
    private bool canDoDamage = true;
    private bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead || player == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            audioSource.clip = deathClip;
            audioSource.Play();

            // Set to invisible and not interactable, just for visual purpose
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject.GetComponent<Rigidbody2D>());

            isDead = true;

            StartCoroutine(WaitToDie(0.5f));

            player.GetComponent<PlayerController>().AddCoins(coins);
            
        }
    }

    private IEnumerator WaitToDie(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && canDoDamage && !isDead)
        {
            player.GetComponent<PlayerController>().TakeHit(damage);

            audioSource.clip = damageClip;
            audioSource.Play();

            canDoDamage = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        canDoDamage = true;
    }
}
