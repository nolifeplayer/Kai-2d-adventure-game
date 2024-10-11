using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal= 0.0f; 
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal= -1.0f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal= 1.0f;
        }
        Debug.Log(horizontal);
       
        float vertical= 0.0f;
        if(Keyboard.current.downArrowKey.isPressed)
        {
            vertical= -1.0f;
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical= 1.0f;
        }
        Debug.Log(vertical);
    
        Vector2 position = transform.position;
        position.y = position.y + 0.01f * vertical;
        position.x=position.x+ 0.01f * horizontal;
        transform.position = position;
        
    }
}
