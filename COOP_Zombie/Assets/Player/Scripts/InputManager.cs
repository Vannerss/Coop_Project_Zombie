using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager _instance;

    private PlayerControls playerControls;

    [SerializeField] Gun gun;

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
        playerControls = new PlayerControls();
        Cursor.visible = false;

       //playerControls.Shoot.performed += _ => gun.Shoot();
       playerControls.Player.Shoot.performed += _ => gun.Shoot();
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
}
