using UnityEngine;


public class CameraController : MonoBehaviour
{
    // Player transform
    public Transform player;

    // Offset
    public float offset;
    public float offsetSmoothing;


    // Player position
    private Vector3 playerPosition;


    // Update is called once per frame
    void Update()
    {
        // If player is null, exit
        if(player == null)
        {
            return;
        }

        // Get player position
        playerPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

        // If the player local scale X axis is bigger than 0, add the offset to X axis
        if(player.localScale.x > 0f)
        {
            playerPosition = new Vector3(player.position.x + offset, player.position.y, playerPosition.z);
        }

        // If not, subtract the offset from X axis
        else
        {
            playerPosition = new Vector3(player.position.x - offset, player.position.y, playerPosition.z);
        }

        // Lerp camera transform position to player position, using the offset smoothing value
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
