using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static NativeGallery;

public class SpriteCreator : MonoBehaviour
{
    [SerializeField] private Texture2D targetTexture;
    public List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    [SerializeField] private Color selectedColour = Color.white;
    private Camera cam;
    [SerializeField] Color[] allPixels;
    [SerializeField] List<AudioClip> paintSounds = new List<AudioClip>();
    AudioSource audioSource;
    public GameObject activeColourObject = null;
    public NativeGallery.Permission permission;


    public void ChangeColour(GameObject inputObject)
    {
        if (inputObject != activeColourObject)
        {
            if (activeColourObject != null) activeColourObject.GetComponent<ToggleImageVisability>().ToggleImage(true);
            selectedColour = inputObject.transform.GetChild(0).GetComponent<Image>().color;
            Debug.Log(selectedColour);
            activeColourObject = inputObject;
        }
        else
        {
            activeColourObject.GetComponent<ToggleImageVisability>().ToggleImage(true);
            activeColourObject = null;
            selectedColour = Color.white;
        }
       
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GameManager.instance.volume;
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
        WhiteEdges();
        LoadTextures();


    }

    private void WhiteEdges()
    {
        for (int i = 0; i < allPixels.Length; i++)
        {
            if (i < 16 || i > 240 || i % 16 == 0 || i % 16 == 15)
            {
                UpdatePixel(i % 16, Mathf.FloorToInt(i / 16), Color.white, true);
            }

        }

    }



    public static Texture2D LoadTexture(Texture2D originalTexture)
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
        GameManager.instance.SetSprite();

    }



    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (selectedColour != Color.white)
            {

                //Debug.Log("Pressed");
                //Debug.Log(Camera.main.ScreenPointToRay(Input.GetTouch(0).position));


                RaycastHit2D rayHit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            

                // If it hits something...
                if (rayHit.collider != null)
                {
                    GameObject hitObj = rayHit.transform.gameObject;
                    //if (hitObj.tag == "Colours")
                    //{
                    //    selectedColour = hitObj.GetComponent<SpriteRenderer>().color;
                    //}
                    //else 
                    if (hitObj.tag == "Pixels")
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
                            
                            UpdatePixel(posX, posY, selectedColour);
                        }

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
    void UpdatePixel(int x, int y, Color newColour, bool start = false)
    {
        spriteRenderers[x + y * 16].color = selectedColour;
        if (!audioSource.isPlaying && !start) audioSource.PlayOneShot(paintSounds[Random.Range(0, paintSounds.Count)]);
        targetTexture.SetPixel(x, y, newColour);
        targetTexture.Apply();
        WriteTextureToFile();
    }


    public void PickImage()
    {
        if (true)
        //if (permission == NativeGallery.Permission.Granted)
        {
            NativeGallery.GetImageFromGallery((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    // Create Texture from selected image
                    Texture2D texture = NativeGallery.LoadImageAtPath(path, -1, false);
                    if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }

                    targetTexture = texture;
                    WhiteEdges();
                    WriteTextureToFile(texture);
                    LoadTextures();
                }
            });
        }
    }

}
