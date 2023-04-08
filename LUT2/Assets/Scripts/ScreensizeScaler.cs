using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensizeScaler : MonoBehaviour
{
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height; // basically height * screen aspect ratio
        gameObject.transform.localScale = Vector3.one * width / scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
