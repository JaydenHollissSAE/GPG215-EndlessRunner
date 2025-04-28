using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    [SerializeField] private string spriteFileName = "sprite.png";
    [SerializeField] private string textureFileName = "texture.png";
    private Renderer textureRenderer;
    private SpriteRenderer spriteRenderer;
    [SerializeField] bool isSprite;
    // Start is called before the first frame update
    void Start()
    {
        //if (GetComponent<SpriteRenderer>() != null)
        //{
        //    spriteRenderer = GetComponent<SpriteRenderer>();
        //    isSprite = true;
        //    LoadSprite();
        //}
        //else if (GetComponent<Renderer>() != null)
        //{
        //    textureRenderer = GetComponent<Renderer>();
        //    isSprite = false;
        //    LoadTexture();
        //}

        if (isSprite)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            LoadSprite();
        }
        else
        {
            textureRenderer = GetComponent<Renderer>();
            LoadTexture();
        }


    }

    private void LoadTexture()
    {

        string filePath = Path.Combine(Application.persistentDataPath, textureFileName);

        if (File.Exists(filePath))
        {
            // Read data
            byte[] imageBytes = File.ReadAllBytes(filePath);

            // Load image data to new Texture2D object
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);

            // Use the loaded texture on object
            textureRenderer.material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("Texture file not found at path: " + filePath);
        }
    }
    private void LoadSprite()
    {

        string spritePath = Path.Combine(Application.persistentDataPath, spriteFileName);

        if (File.Exists(spritePath))
        {
            // Read data
            byte[] spriteBytes = File.ReadAllBytes(spritePath);

            // Load image data to new Texture2D object
            Texture2D texture = new Texture2D(16, 16);
            texture.LoadImage(spriteBytes);
            if (texture.width > 16 || texture.height > 16)
            {
                SpriteCreator.WriteTextureToFileFunc(texture);
                LoadSprite();
            }
            else
            {
                Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);

                // Use the loaded texture on object
                spriteRenderer.sprite = newSprite;
            }


        }
        else
        {
            Debug.LogError("Sprite file not found at path: " + spritePath);
        }
    }
}
