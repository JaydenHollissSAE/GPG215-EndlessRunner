using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetGameVersion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
        text.text = "Ver " + Application.version.ToString();
    }

}
