using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    float xRange = 2.4f;

    Rigidbody2D rb;
    bool gameOver;
    bool canMove;
    bool ignoreTrigger;
    bool ignoreCollision;

    public AudioSource boxCollisionSound;
    public AudioSource boxExplosionSound;
    public AudioSource dropSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        GamePlayController.instance.currentBox = this;

        ignoreCollision = false;
        ignoreTrigger = false;

        if (Random.Range(0, 2) > 0)
        {
            moveSpeed *= -1f;
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 pos = transform.position;
            pos.x += moveSpeed * Time.deltaTime;

            if (pos.x > xRange)
            {
                moveSpeed *= -1f;
            }else if (pos.x < -xRange)
            {
                moveSpeed *= -1f;
            }

            transform.position = pos;
        }
    }

    public void DropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
        dropSound.Play();
    }

    void Land()
    {
        if (gameOver) return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GamePlayController.instance.SpawnNewBox();
        GamePlayController.instance.MoveCamera();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (ignoreCollision)
        {
            return;
        }
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag=="Box")
        {
            boxCollisionSound.Play();
            ignoreCollision = true;
            Invoke("Land", 2f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ignoreTrigger || gameOver) { return; }

        if(collision.gameObject.tag == "GameOver")
        {
            CancelInvoke("Land");
            gameOver = true;
            //ignoreTrigger = true;
            boxExplosionSound.Play();
            Invoke("Restart", 2f);
        }
    }

    void Restart()
    {
        GamePlayController.instance.RestartGame();
    }
}
