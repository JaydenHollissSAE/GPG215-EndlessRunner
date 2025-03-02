using System.Collections.Generic;
using UnityEngine;

public class BlockCluster : MonoBehaviour
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            blocks.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("ButtonPressed");
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
        Debug.Log(toDisable.ToString()+" toDisable");
        int disabled = 0;
        Debug.Log(disabled.ToString()+ " disabled");
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
