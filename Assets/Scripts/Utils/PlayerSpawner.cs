using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public TextMeshProUGUI coinsText;
    public Slider healthBar;
    public Canvas inventoryCanvas;

    public ChangeScene changeScene;

    void Start()
    {
        // Find player
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");

        // If player was not found and it's MainScene, spawn player
        if(player == null && changeScene.toBattle)
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        // Setup player controller
        player.GetComponent<PlayerController>().coinsText = coinsText;
        player.GetComponent<PlayerController>().healthBar = healthBar;

        // Setup player inventory
        player.GetComponent<Inventory>().inventoryCanvas = inventoryCanvas;

        // Setup main camera
        Camera.main.gameObject.GetComponent<CameraController>().player = player.transform;
    }
}
