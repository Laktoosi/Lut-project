using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] public int damage = 10;

    public bool isPlayer = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isPlayer == false)
        {
            //kill player
            collision.gameObject.GetComponent<PlayerController>().die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && isPlayer == true)
        {
            Debug.Log("Enemy");
            collision.gameObject.GetComponent<Enemy>().health -= damage;
            StartCoroutine("damageTimer");
        }
    }

    IEnumerator damageTimer()
    {
        damage = 1;
        yield return new WaitForSeconds(1);
        damage = 0;
    }
}
