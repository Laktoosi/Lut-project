using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public delegate void OnDisableCallback(Bullet Instance);
    public OnDisableCallback Disable;

    private Rigidbody2D rb;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.dead == true) Disable?.Invoke(this);

        if (gameObject.activeSelf)
            StartCoroutine(disableBullet());

        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            Disable?.Invoke(this);
        }

        if (collision.gameObject.tag == "Border")
        {
            Disable?.Invoke(this);
        }
    }

    IEnumerator disableBullet()
    {
        yield return new WaitForSeconds(3f);
        Disable?.Invoke(this);
    }
}
