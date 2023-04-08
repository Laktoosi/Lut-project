using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool rotate;
    public float rotSpeed = 100;

    public bool seeker; //enemy type that occasionally changes direction to player, name could be better

    bool coroutineActivated;

    public int scoreAmount = 10;

    public float baseHealth = 1f;
    public float health = 1f;
    public float moveSpeed = 5f;

    bool dead = false;

    public Transform target;

    private Rigidbody2D rb;

    public delegate void OnDisableCallback(Enemy Instance);
    public OnDisableCallback Disable;

    public Collider2D hurtbox;

    bool canMove = true;

    public ParticleSystem explosion;

    public Score score;

    Vector3 direction;

    public AudioClip deathSound;
    public AudioSource audioSource;
    //Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        score = GameObject.Find("ScoreText").GetComponent<Score>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) die();
    }

    private void FixedUpdate()
    {
        //rotating enemy change dir towards player periodically
        if (seeker && !coroutineActivated && gameObject.activeSelf == true)
        {
            StartCoroutine(ChangeDir());
            coroutineActivated = true;
        }

        if (health <= 0 && !dead)
        {
            score.score += scoreAmount; //update score
            dead = true;
        }

        else if (health <= 0 || target.GetComponent<PlayerController>().dead == true) //if dead or player is dead then disable enemy
        {
            die();
        }

        if (target != null)
        {
            //canMove = true;
            if(seeker == false) {
                direction = target.position - transform.position;
            }

            //Rotate towards player
            if (canMove)
            {
                //rb.rotation = angle;
                direction.Normalize();
                Move(direction);
            }

            if (rotate)
            {
                transform.Rotate(new Vector3(0, 0, rotSpeed) * Time.deltaTime);
            }
        }
    }

    public void die()
    {
        audioSource.pitch = Random.Range(0.5f, 1f);
        audioSource.PlayOneShot(deathSound);
        //AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Instantiate(explosion, transform.position, Quaternion.identity);
        health = baseHealth;
        dead = false;
        coroutineActivated = false;
        Disable?.Invoke(this);
    }

    public void changeDir()
    {
        
    }

    public void Move(Vector2 direction)
    {
        //Move towards player
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private IEnumerator ChangeDir()
    {
        direction = target.position - transform.position;
        yield return new WaitForSeconds(3);
        StartCoroutine(ChangeDir());
    }
}
