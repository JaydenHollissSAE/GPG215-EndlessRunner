using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteCreator : MonoBehaviour
{
    [SerializeField] private Texture2D targetTexture;
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    [SerializeField] private Color selectedColour = Color.white;
    private Camera cam;
    [SerializeField] Color[] allPixels;
    [SerializeField] List<AudioClip> paintSounds = new List<AudioClip>();
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        targetTexture = LoadTexture(targetTexture);
        WriteTextureToFile();
        cam = GetComponent<Camera>();
        GameObject tmpObj = GameObject.FindGameObjectWithTag("SpritePixels");
        for (int i = 0; i < tmpObj.transform.childCount; i++)
        {
            GameObject tmpObj2 = tmpObj.transform.GetChild(i).gameObject;
            for (int j = 0; j < tmpObj2.transform.childCount; j++)
            {
                spriteRenderers.Add(tmpObj2.transform.GetChild(j).GetComponent<SpriteRenderer>());
            }
            
        }
        LoadTextures();
    }



    public Texture2D LoadTexture(Texture2D originalTexture)
    {

        string filePath = Path.Combine(Application.persistentDataPath, "sprite.png");

        if (File.Exists(filePath))
        {
            // Read data
            byte[] imageBytes = File.ReadAllBytes(filePath);

            // Load image data to new Texture2D object
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageBytes);


            // Use the loaded texture on object
            //textureRenderer.material.mainTexture = texture;
            return texture;
        }
        else
        {
            Debug.LogError("Texture file not found at path: " + filePath);
            
            return originalTexture;
        }
    }


    private void WriteTextureToFile(Texture2D inputFile = null)
    {
        if (inputFile == null) inputFile = targetTexture;
        WriteTextureToFileFunc(inputFile);
    }

    public static void WriteTextureToFileFunc(Texture2D inputFile)
    {
        if (inputFile.width > 16 || inputFile.height > 16)
        {
            TextureScale.Point(inputFile, 16, 16);
        }
        byte[] textureData = inputFile.EncodeToPNG();
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "sprite.png"), textureData);

    }



    private void Update()
    {
        if (Input.touchCount > 0)
        {
            //Debug.Log("Pressed");
            //Debug.Log(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));


            RaycastHit2D rayHit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            

            // If it hits something...
            if (rayHit.collider != null)
            {
                GameObject hitObj = rayHit.transform.gameObject;
                if (hitObj.tag == "Colours")
                {
                    selectedColour = hitObj.GetComponent<SpriteRenderer>().color;
                }
                else if (hitObj.tag == "Pixels")
                {
                    int posX = 1000;
                    int posY = 1000;
                    if (hitObj.name.Contains("Square"))
                    {
                        if (!hitObj.name.Contains("("))
                        {
                            posX = 0;
                        }
                        else
                        {
                            posX = int.Parse(hitObj.name.Replace("Square (", "").Replace(")", ""));
                        }
                        posY = int.Parse(hitObj.transform.parent.gameObject.name.Replace("Row (", "").Replace(")", ""));

                    }
                    else if (hitObj.name.Contains(","))
                    {
                        string[] pos = hitObj.name.Split(',');
                        posX = int.Parse(pos[1]);
                        posY = int.Parse(pos[0]);
                    }
                    //Debug.Log(posX + " " + posY);
                    if (posX != 1000 && posY != 1000)
                    {
                        spriteRenderers[posX + posY*16].color = selectedColour;
                        UpdatePixel(posX, posY, selectedColour);
                    }

                }
            }
        }

    }



    void LoadTextures()
    {
        allPixels = targetTexture.GetPixels();
        for (int i = 1; i < allPixels.Length+1; i++)
        {
            spriteRenderers[allPixels.Length-i].color = allPixels[allPixels.Length - i];         
        }
    }


    // Update is called once per frame
    void UpdatePixel(int x, int y, Color newColour)
    {
        if (!audioSource.isPlaying) audioSource.PlayOneShot(paintSounds[Random.Range(0, paintSounds.Count)]);
        targetTexture.SetPixel(x, y, newColour);
        targetTexture.Apply();
        WriteTextureToFile();
    }
}
