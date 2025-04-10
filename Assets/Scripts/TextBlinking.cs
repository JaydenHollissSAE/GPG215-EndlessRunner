using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinking : MonoBehaviour
{
    public TextMeshProUGUI blinkingText;
    public float blinkspeed = 0.5f;
    private Coroutine blinkCoroutine;

    private void Start()
    {
        if(blinkCoroutine == null)
        {
            blinkCoroutine = StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            blinkingText.enabled = ! blinkingText.enabled;

            yield return new 
            WaitForSeconds(blinkspeed);
        }
    }
}
