using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    #region Attributes
    [Header("Player Movement Stats Settings")]
    [Space]

    [SerializeField] [Range(0f, .3f)] private float movementSmoothing = 0.05f;  // Smooth out the player movement
    [SerializeField] private bool isFacingRight = true;                         // Is player facing right


    private Rigidbody2D playerRigidbody2D;      // Player's rigidbody 2D
    private Vector3 velocity = Vector3.zero;    // Player's velocity
    private bool isBusy = false;
    #endregion

    #region Methods
    #region Unity Methods
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Setup player's rigidbody 2D
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }
    #endregion

    /// <summary>
    /// Move player, based on a Top-Down view.
    /// </summary>
    /// <param name="horizontalMovement">Horizontal movement value.</param>
    /// <param name="verticalMovement">Vertical movement value.</param>
    public void Move(float horizontalMovement, float verticalMovement)
    {
        // If player is busy, do not move
        if(isBusy)
        {
            return;
        }

        // Get player's target velocity and apply it to player
        Vector3 targetVelocity = new Vector2(horizontalMovement * 10f, verticalMovement * 10f);
        playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        // If player is moving right but player is not facing right, perform a flip
        if(horizontalMovement > 0 && !isFacingRight)
        {
            Flip();
        }

        // If player is moving left but player is facing right, perform a flip
        else if (horizontalMovement < 0 && isFacingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Make player stop movement immediatly.
    /// </summary>
    public void StopMoving()
    {
        playerRigidbody2D.velocity = Vector2.zero;
    }

    /// <summary>
    /// Set player status to busy. This means that the player won't be able to move. This does not affect the input.
    /// </summary>
    /// <param name="isBusy">Busy value.</param>
    public void SetIsBusy(bool isBusy)
    {
        StopMoving();
        this.isBusy = isBusy;
    }

    /// <summary>
    /// Get player busy status value.
    /// </summary>
    /// <returns>Value of isBusy variable.</returns>
    public bool GetIsBusy()
    {
        return isBusy;
    }

    /// <summary>
    /// Flip player.
    /// </summary>
    private void Flip()
    {
        // Switch the way player is facing
        isFacingRight = !isFacingRight;

        // Invert player's X local scale
        Vector3 _scale = transform.localScale;
        _scale.x *= -1f;
        transform.localScale = _scale;
    }
    #endregion
}
