using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

    private InputManager inputManager;

   [SerializeField] private float damage = 10f;
   [SerializeField] private float range = 100f;

    private void Start()
    {
        inputManager = InputManager.Instance; 
        cam = Camera.main.transform;
    }

    private void Update()
    {
        if (inputManager.PlayerOnClick())
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            print(hit.collider.name);
        }
    }
}
