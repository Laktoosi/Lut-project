using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacgroundScroll : MonoBehaviour
{
    public Material mat;
    public MeshRenderer mr;

    float parrallax = 2f;

    [SerializeField]
    private bool constantScroll;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mr = GetComponent<MeshRenderer>();

        mat = mr.material;

        Vector2 offset =  mat.mainTextureOffset;

        if (constantScroll == false)
        {
            offset.x = transform.position.x / transform.localScale.x / parrallax;
            offset.y = transform.position.y / transform.localScale.y / parrallax;
        }
        else
            offset.x += Time.deltaTime / 10f;

        mat.mainTextureOffset = offset;
    }
}
