using UnityEngine;
using UnityEngine.UI;


public class ButtonFloatingAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float amplitude = 5f;
    public float frequency = 1f;
    private RectTransform rectTransform;
    private Vector2 startPos;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(rectTransform ==  null) return;

        float newY = Mathf.Sin(Time.time * frequency) * amplitude;

        rectTransform.localPosition = startPos + new Vector2(0f, newY);
    }
}
