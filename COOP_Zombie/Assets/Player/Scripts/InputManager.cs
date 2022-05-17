using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager _instance;
    private PlayerControls playerControls;
    

    public static InputManager Instance 
    {
        get 
        {
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            _instance = this;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerControls = new PlayerControls();

        //playerControls.Player.Shoot.performed += _ => gun.Shoot(); //removed this subscription cause it destroyed the purpose of a singleton.
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement() 
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped() 
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerOnClick()
    {
        return playerControls.Player.Shoot.triggered;
    }

    public bool PlayerReload() 
    {
        
        return playerControls.Player.Reload.triggered;
    }

   
}
