using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Character controller
    public CharacterController2D controller;

    // Player's walk speed (default is 40f)
    public float walkSpeed = 40f;

    public TextMeshProUGUI coinsText;


    // Player movement
    private float horizontalMove;
    private float verticalMove;

    // Coins
    private int coins = 0;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // If there is no controller set, get the CharacterController2D of this object
        if(controller == null)
        {
            controller = gameObject.GetComponent<CharacterController2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        UpdateUI();
    }


    // FixedUpdate is called every fixed frame-rate frame
    void FixedUpdate()
    {
        // Move player
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
    }


    /// <summary>
    /// Add a certain amount of coins.
    /// </summary>
    /// <param name="coins">Amount of coins to add.</param>
    public void AddCoins(int coins)
    {
        this.coins += coins;
    }

    /// <summary>
    /// Remove a certain amount of coins.
    /// </summary>
    /// <param name="coins">Amount of coins to remove.</param>
    public void RemoveCoins(int coins)
    {
        this.coins -= coins;
    }

    /// <summary>
    /// Get the player's amount of coins.
    /// </summary>
    /// <returns>Player's amount of coins.</returns>
    public int GetCoins()
    {
        return this.coins;
    }


    /// <summary>
    /// Method for handle all player inputs.
    /// </summary>
    private void InputManager()
    {
        // Player movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * walkSpeed;
    }

    /// <summary>
    /// Update all UI elements related to the player.
    /// </summary>
    private void UpdateUI()
    {
        // Update coins text
        coinsText.text = coins.ToString().PadLeft(4, '0');
    }
}
