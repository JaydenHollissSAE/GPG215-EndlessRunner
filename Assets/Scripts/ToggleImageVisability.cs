using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class ToggleImageVisability : MonoBehaviour
{
    private Image image;
    private SpriteCreator spriteCreator;
    private void Start()
    {
        image = GetComponent<Image>();
        spriteCreator = FindFirstObjectByType<SpriteCreator>();
    }

    public void ToggleImage(bool selection = true)
    {
        if (!selection)
        {
            spriteCreator.ChangeColour(gameObject);
        }
        image.enabled = !image.enabled;
    }

}
