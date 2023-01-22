using UnityEngine;


public class Skill : MonoBehaviour
{
    // Skill stats
    public float speed = 20f;
    public int damage = 1;

    // Audio source
    public AudioSource audioSource;


    // Destination
    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Move to mouse position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If arrive mouse position, destroy
        if(transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If hit an enemy, give damage and destroy
        if(collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().TakeHit(damage);
            Destroy(gameObject);
        }
    }
}
