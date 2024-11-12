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
     Debug.Log(move);
       
        
    }


    void FixedUpdate()
    {
    Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
    rigidbody2d.MovePosition(position);
        
    }
    void ChangeHealth (int amount)
    {
    currentHealth = Mathf.Clamp(currentHealth+ amount, 0,maxHealth);
    Debug.Log(currentHealth + "/" + maxHealth);
    }
}
