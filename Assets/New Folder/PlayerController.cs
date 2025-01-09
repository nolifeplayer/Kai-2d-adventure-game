using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public GameObject projectilePrefab;
    
    
    Animator animator;
    Vector2 moveDirection = new Vector2(1,0);
    //public InputAction LeftAction;
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed= 3.0f;
     
     // Variables related to the player character movement
     public int maxHealth = 5;
     int currentHealth;
     public int health { get { return currentHealth; }}

    //Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Start is called before the first frame update
    void Start()
    {
     //LeftAction.Enable();  
     MoveAction.Enable();
     rigidbody2d = GetComponent<Rigidbody2D>();
     currentHealth= maxHealth;
     animator = GetComponent<Animator>();
     //QualitySettings.vSyncCount = 0;
    //Application.targetFrameRate = 380; 
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
      UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
      
      if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
          {
             moveDirection.Set(move.x, move.y);
             moveDirection.Normalize();
          }

          animator.SetFloat("Look X", moveDirection.x);
          animator.SetFloat("Look Y", moveDirection.y);
          animator.SetFloat("Speed", move.magnitude);
        
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <0)
            {
                isInvincible = false;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.C))
            {
                Launch();
            }
      }
 
  // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        
    }
    
    public void ChangeHealth(int amount)
    {
        if (amount<0)
        {
            if (isInvincible)
            {
                return;
            }
        
            isInvincible = true;
            damageCooldown = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);
        
        animator.SetTrigger("Launch");
    }
}
