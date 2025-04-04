using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    bool broken = true;
    Animator animator;
    AudioSource audioSource;
    //Public variables
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    
    //Private variables
    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;

    int health = 5;


    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent <AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if ( timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
    }
    
    //FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if(!broken)
        {
            return;
        }
        
        Vector2 position = rigidbody2d.position;
    
        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("MOve Y", direction);
        }   
        else
        {
            position.x = position.x + speed *  direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2d.MovePosition(position);

    
    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       PlayerController player = other.gameObject.GetComponent<PlayerController>();
    
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        
        health--;
        if(health < 1 )
        {
            Destroy(gameObject);
        }
        audioSource.Stop();
    }
}
