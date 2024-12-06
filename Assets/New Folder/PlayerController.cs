using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
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
     //QualitySettings.vSyncCount = 0;
    //Application.targetFrameRate = 380; 
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
      UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown <0)
            {
                isInvincible = false;
            }
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
}
