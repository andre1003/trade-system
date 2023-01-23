using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    // Character controller
    public CharacterController2D controller;

    // Player's walk speed (default is 40f)
    public float walkSpeed = 40f;

    // Health
    public float baseHealth = 100f;

    // Coins text and health bar
    public TextMeshProUGUI coinsText;
    public Slider healthBar;

    public GameObject skillPrefab;


    // Inventory
    [SerializeField] private Inventory inventory;

    // Player movement
    private float horizontalMove;
    private float verticalMove;

    // Coins
    private int coins = 0;

    // Health
    private float health;

    // Temporary interaction
    private Collider2D interaction;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // If there is no controller set, get the CharacterController2D of this object
        if(controller == null)
        {
            controller = gameObject.GetComponent<CharacterController2D>();
        }

        health = baseHealth;
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

    // Called when a new level is loaded
    void OnLevelWasLoaded(int level)
    {
        // Reset player position
        transform.position = Vector3.zero;
    }


    /// <summary>
    /// Add a certain amount of coins.
    /// </summary>
    /// <param name="coins">Amount of coins to add.</param>
    public void AddCoins(int coins)
    {
        this.coins = Mathf.Clamp(this.coins + coins, 0, 9999);
    }

    /// <summary>
    /// Remove a certain amount of coins.
    /// </summary>
    /// <param name="coins">Amount of coins to remove.</param>
    public void RemoveCoins(int coins)
    {
        this.coins = Mathf.Clamp(this.coins - coins, 0, 9999);
    }

    /// <summary>
    /// Get the player's amount of coins.
    /// </summary>
    /// <returns>Player's amount of coins.</returns>
    public int GetCoins()
    {
        return coins;
    }

    /// <summary>
    /// Get health current value.
    /// </summary>
    /// <returns>Player health.</returns>
    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Restore a certain amount of health.
    /// </summary>
    /// <param name="healthToRestore">Health to restore.</param>
    public void RestoreHealth(float healthToRestore)
    {
        // Add health using clamp to not allow health bigger than baseHealth
        health = Mathf.Clamp(health + healthToRestore, 0, baseHealth);
    }

    /// <summary>
    /// Take a hit.
    /// </summary>
    /// <param name="damage">Enemy's damage.</param>
    public void TakeHit(float damage)
    {
        // Remove health using clamp to not allow health lower than 0
        health = Mathf.Clamp(health - damage, 0, baseHealth);

        // If health is equal to 0, kill player
        if(health == 0f)
        {
            GameOver.instance.EndGame();
        }
    }

    /// <summary>
    /// Display or hid inventory.
    /// </summary>
    public void OpenCloseInventory()
    {
        // Display or hide inventory UI
        bool success = inventory.DisplayOrHide();

        // If inventory was successfully displayed or hidden, set player busy status
        if(success)
        {
            controller.SetIsBusy(inventory.inventoryCanvas.enabled);
        }
    }

    /// <summary>
    /// Buff health points.
    /// </summary>
    /// <param name="additionalHealth">Additional health points.</param>
    public void BuffHealth(float additionalHealth)
    {
        // Add health
        baseHealth += additionalHealth;

        // If player was full life
        if(health == baseHealth - additionalHealth)
        {
            health = baseHealth;
        }

        // Change health bar size
        RectTransform rt = healthBar.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x + additionalHealth, rt.sizeDelta.y);
        rt.position = new Vector3(rt.position.x + (additionalHealth * 1.25f), rt.position.y, rt.position.z);
    }

    public void DebuffHealth(float additionalHealth)
    {
        // Add health
        baseHealth -= additionalHealth;

        // If player was full life
        if(health == baseHealth + additionalHealth)
        {
            health = baseHealth;
        }

        // Change health bar size
        RectTransform rt = healthBar.GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x - additionalHealth, rt.sizeDelta.y);
        rt.position = new Vector3(rt.position.x - (additionalHealth * 1.25f), rt.position.y, rt.position.z);
    }


    /// <summary>
    /// Method for handle all player inputs.
    /// </summary>
    private void InputManager()
    {
        // Display or hide inventory UI
        if(Input.GetButtonDown("Inventory"))
        {
            OpenCloseInventory();
        }

        // Pause game
        if(Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }

        // If player is busy, do not check for  the other input actions
        if(controller.GetIsBusy())
        {
            return;
        }

        // Player movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * walkSpeed;

        // Interact action
        if(Input.GetButtonDown("Interact"))
        {
            Interact();
        }

        // Cast skill
        if(Input.GetButtonDown("Fire1"))
        {
            CastSkill();
        }
    }

    /// <summary>
    /// Update all UI elements related to the player.
    /// </summary>
    private void UpdateUI()
    {
        // Update coins text
        coinsText.text = coins.ToString().PadLeft(4, '0');
        healthBar.value = health / baseHealth;
    }

    // Called when the player trigger start overlap something.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Save collision
        interaction = collision;
        
        // Try to get NPCController component
        NPCController npcController;
        bool success = collision.TryGetComponent(out npcController);

        // If success, display interaction
        if(success)
        {
            npcController.DisplayHideInteraction();
        }
    }

    // Called when the player trigger stop overlap something.
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Set interaction to null
        interaction = null;

        // Try to get NPCController component
        NPCController npcController;
        bool success = collision.TryGetComponent(out npcController);

        // If success, display interaction
        if(success)
        {
            npcController.DisplayHideInteraction();
        }
    }

    /// <summary>
    /// Perform an interaction, based on interaction variable.
    /// </summary>
    private void Interact()
    {
        // If there is no interaction, exit
        if(interaction == null)
        {
            return;
        }

        // Trader
        if(interaction.gameObject.tag == "Trader")
        {
            // Do not allow player to move
            controller.SetIsBusy(true);

            // Get trade system component
            TradeSystem tradeSystem = GameObject.Find("TradeSystem").GetComponent<TradeSystem>();

            // Set inventories and player controller
            tradeSystem.SetInventories(inventory, interaction.GetComponent<Inventory>());
            tradeSystem.SetPlayerController(this);
            tradeSystem.DisplayUI();

            // Set interaction to null
            interaction = null;
        }
    }

    /// <summary>
    /// Cast a skill.
    /// </summary>
    private void CastSkill()
    {
        // Get additional damage, if an weapon is equipped
        int additionalDamage = inventory.GetAdditionalDamageOfItem();

        // Spawn skill
        GameObject skill = Instantiate(skillPrefab, transform.position, Quaternion.identity);
        
        // Add additional damage
        skill.GetComponent<Skill>().damage += additionalDamage;
    }

    /// <summary>
    /// Pause or resume game
    /// </summary>
    private void PauseGame()
    {
        if(PauseMenu.instance.isGamePaused)
        {
            PauseMenu.instance.Resume();
        }
        else
        {
            PauseMenu.instance.Pause();
        }
    }
}
