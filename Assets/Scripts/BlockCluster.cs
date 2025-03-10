using System.Collections.Generic;
using UnityEngine;

public class BlockCluster : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private GameObject reset;
    [SerializeField] private bool generateOnStart = true;
    //[SerializeField] private bool isRandom = false;
    [SerializeField] private List<Sprite> spriteList = new List<Sprite>();
    private Vector2 resetNewPos;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks.Add(transform.GetChild(i).gameObject);
        }
        reset = GameObject.FindGameObjectWithTag("Reset");
        resetNewPos = new Vector2(reset.transform.position.x, 0);
        spriteList = gameManager.spriteList;
        if (generateOnStart)
        {
            GenerateCluster();
        }

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position+Vector3.left, Time.fixedDeltaTime*gameManager.speed);
        Vector2 newPos = new Vector2(transform.position.x, 0);
        if (Vector2.Distance(newPos, resetNewPos) <= 5f)
        {
            transform.position = new Vector2(23.26f, transform.position.y);
            GenerateCluster();
        }
    }
    void SetTexture(GameObject block)
    {
        SpriteRenderer spriteRenderer = block.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
    }

    void GenerateCluster()
    {
        for (int i = 0;i < blocks.Count; i++)
        {
            GameObject item = blocks[i];
            item.SetActive(true);
            SetTexture(item);
        }
        //Debug.Log(disabled.ToString()+ " disabled");

        int selected = Random.Range(0, 4);
        if (selected == 0)
        {
            // 2 Rows
            //Debug.Log("Run 0");
            int row = Random.Range(0, 5);
            for (int j = 0; j < blocks.Count; j++)
            {
                blocks[j].SetActive(false);
            }
            for (int i = 0; i < 5; i++)
            {
                GameObject item = blocks[row + i * 5];
                item.SetActive(true);
                SetTexture(item);
            }
        }
        else if (selected == 1)
        {

            //Debug.Log("Run 1");
            //int row = Random.Range(0, 5);
            for (int j = 0; j < blocks.Count; j++)
            {
                blocks[j].SetActive(false);
            }
            int amount = Random.Range(0, 5);
            for (int i = 0; i < amount; i++)
            {
                GameObject item = blocks[0 + i * 5];
                item.SetActive(true);
                SetTexture(item);
            }
            amount = Random.Range(0, 5);
            for (int i = 0; i < amount; i++)
            {
                GameObject item = blocks[4 + i * 5];
                item.SetActive(true);
                SetTexture(item);
            }
        }
        else if (selected == 2)
        {
            //Debug.Log("Run 2");
            //int row = Random.Range(0, 5);
            for (int j = 0; j < blocks.Count; j++)
            {
                blocks[j].SetActive(false);
            }
            int collumn = Random.Range(0, 5);
            for (int i = 0; i < 5; i++)
            {
                if (Random.Range(0, 2) == 1)
                {
                    GameObject item = blocks[collumn + i];
                    item.SetActive(true);
                    SetTexture(item);
                }
            }
            for (int i = 0; i < 5; i++)
            {
                GameObject item = blocks[4 + i * 5];
                item.SetActive(true);
                SetTexture(item);
            }
        }
        else if (selected == 3)
        {
            //Debug.Log("Run 3");
            int row = Random.Range(1, 3);
            for (int j = 0; j < blocks.Count; j++)
            {
                blocks[j].SetActive(false);
            }
            int amount = Random.Range(0, 5);
            for (int i = 0;i < amount; i++)
            {
                GameObject item = blocks[row + i * 5];
                item.SetActive(true);
                SetTexture(item);
            }
            row += 1;
            amount = Random.Range(0, 5);
            for (int i = 0; i < amount; i++)
            {
                GameObject item = blocks[row + i * 5];
                item.SetActive(true);
                SetTexture(item);
            }
        }
        else
        {
            int toDisable = blocks.Count - 1 - (Random.Range(1, 6));
            //Debug.Log("Run Random");
            int disabled = 0;
            while (disabled < toDisable)
            {
                GameObject item = blocks[Random.Range(0, blocks.Count)];
                if (item.activeSelf)
                {
                    item.SetActive(false);
                    disabled += 1;
                }
            }

        }
    }
}
