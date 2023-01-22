using UnityEngine;
using UnityEngine.UI;


public class NPCController : MonoBehaviour
{
    // Keyboard and controller interaction key sprites
    public Sprite keyboardInteractionKey;
    public Sprite controllerInteractionKey;

    // Interaction canvas reference
    public Canvas interactionCanvas;


    // Start is called before the first frame update
    void Start()
    {
        // Set interaction sprite to keyboard
        interactionCanvas.GetComponentInChildren<Image>().sprite = keyboardInteractionKey;
    }

    /// <summary>
    /// Display or hide interaction UI.
    /// </summary>
    public void DisplayHideInteraction()
    {
        interactionCanvas.enabled = !interactionCanvas.enabled;
    }
}
