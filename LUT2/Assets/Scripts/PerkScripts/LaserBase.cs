using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBase : MonoBehaviour //Used to be PerkBase
{
    public Transform target;
    public float rotSpeed = 5f;

    public GameObject laserContainer;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);
    }
}
