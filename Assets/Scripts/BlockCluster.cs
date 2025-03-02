using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockCluster : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    [SerializeField] private GameObject reset;
    [SerializeField] private bool generateOnStart = true;
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
        if (generateOnStart )
        {
            GenerateCluster();
        }

    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position+Vector3.left, Time.fixedDeltaTime*gameManager.speed);
        //Debug.Log(Vector2.Distance(transform.position, reset.transform.position));
        Vector2 newPos = new Vector2(transform.position.x, 0);
        if (Vector2.Distance(newPos, resetNewPos) <= 5f)
        {
            transform.position = new Vector2(23.26f, transform.position.y);
            GenerateCluster();
        }
    }

    void GenerateCluster()
    {
        for (int i = 0;i < blocks.Count; i++)
        {
            blocks[i].SetActive(true);
        }

        int toDisable = blocks.Count - 1 -(Random.Range(1, 6));
        //Debug.Log(toDisable.ToString()+" toDisable");
        int disabled = 0;
        //Debug.Log(disabled.ToString()+ " disabled");
        while (disabled < toDisable)
        {
            GameObject item = blocks[Random.Range(0, blocks.Count)];
            if (item.activeSelf)
            {
                item.SetActive(false);
                disabled+=1;
            }
        }
    }
}
