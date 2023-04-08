using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float speed = 10f;
    private float baseSpeed = 10f;
    float speedMultiplier = 1f;

    bool pickedUp = false;

    Rigidbody2D rb;
    public Transform target;

    public delegate void OnDisableCallback(Pickup Instance);
    public OnDisableCallback Disable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target != null && pickedUp)
        {
            //speed up over time
            speed = speed + speedMultiplier * Time.deltaTime;

            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            Move(direction);
            float dist = Vector2.Distance(target.transform.position, transform.position);

            if(dist <= 1f)
            {
                speed = baseSpeed;
                Disable?.Invoke(this);
            }
        }
    }

    public void Move(Vector2 direction)
    {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pickedUp = true;
            target = collision.gameObject.transform;
        }
    }
}
