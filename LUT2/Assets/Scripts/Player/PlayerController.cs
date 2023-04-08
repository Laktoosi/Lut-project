using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int lives = 3;
    public GameObject[] lifeObjects;

    public float moveSpeed = 8f;
    public float dashDistance = 300f;
    public float dashCooldown = 3f;
    public bool canDash = true;
    public bool canMove = true;

    public bool dead = false;

    public Vector2 moveInput;
    Vector2 lookPos;

    private float offset = 0; // offsets are old stuff
    private float gunOffset = -1;

    public bool isMoving;

    [SerializeField] private float fireRate = 0.5f;
    private bool canShoot = true;
    private bool fireDown = false;

    Rigidbody2D rb;
    Animator animator;
    public GameObject bulletPrefab;
    public Transform gunPoint;

    bool gamepad;

    Vector3 mouseWorldPosition;
    Vector3 targetDirection;

    private ObjectPool objPool;

    AudioSource audioData;

    public SpriteRenderer playerSprite;

    public ParticleSystem playerExplosion;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        //get the players bullet pool
        objPool = GameObject.FindGameObjectWithTag("ObjectPoolPlayer").GetComponent<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        //see if we are using a gamepad or keyboard & mouse
        var input = FindObjectOfType<PlayerInput>();
        
        if (input.currentControlScheme == "Gamepad")
        {
            gamepad = true;
        }

        else
            gamepad = false;
    }

    public void FixedUpdate()
    {
        if (dead == false)
        {
            Move();
            Fire();
        }
    }

    public void die()
    {
        if(dead == false) {
            dead = true;
            lives -= 1;

            Instantiate(playerExplosion, transform.position, Quaternion.identity);

            Destroy(lifeObjects[lives].gameObject);

            if (lives <= 0)
            {
                var gameOver = GameObject.Find("GameOver");
                gameOver.GetComponent<GameOver>().DisplayGameOverScreen();
                Destroy(gameObject);
            }
            else
            {
                transform.position = new Vector3(0, 0, 0);
                rb.velocity = new Vector2(0, 0);
                StartCoroutine(blink());
                Invoke("Respawn", 1);
            }
        }
    }

    //Moving
    #region
    public void Move()
    {

        float angle;
        //Vector2 lookDir;

        //Player rotation with gamepad
        if (gamepad == false)
        {
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(lookPos);
            //Player rotation with mouse
            targetDirection = mouseWorldPosition - transform.position;
            angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + offset));
        }
        else
        {
            //lookDir = lookPos;
            angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + offset));
        }

        //move player
        rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);

        //rotate gunpoint so the bullet go to the correct direction
        gunPoint.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - gunOffset);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        //If movement input is not nothing we are moving
        isMoving = moveInput != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && isMoving)
        {
            StartCoroutine(CanDash());
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (gamepad == false)
        {
            lookPos = context.ReadValue<Vector2>();
        }

        else if(gamepad == true && context.performed)
            lookPos = context.ReadValue<Vector2>();
    }

    IEnumerator CanDash()
    {
        canDash = false;
        moveSpeed = 16f;
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 8f;
        yield return new WaitForSeconds(1 / dashCooldown);
        canDash = true;
    }
    #endregion

    //shooting
    #region
    public void Fire()
    {

        if (fireDown) Shoot();
        else
            StopCoroutine(CanShoot());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        //check if we are holding the Fire button down
        var value = context.ReadValue<float>();
        fireDown = value >= 0.9f;
    }

    public void Shoot()
    {
        if (canShoot == false) return;

        //Let pool know the correct rotation
        objPool.gunPoint = gunPoint;

        StartCoroutine(CanShoot());

        objPool.getBulletPool();
        audioData.pitch = Random.Range(0.5f, 1f);
        audioData.Play(0);
        animator.SetTrigger("Shoot");
    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(1 / fireRate);
        canShoot = true;
    }
    #endregion

    IEnumerator blink()
    {
        while (dead) {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(.05f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(.05f);
        }
    }

    void Respawn()
    {
        playerSprite.enabled = true;
        dead = false;
        EnemySpawner enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        enemySpawner.startSpawn();
    }
}
