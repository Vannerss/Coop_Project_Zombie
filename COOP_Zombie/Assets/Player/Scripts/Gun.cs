using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

   [SerializeField] public float damage = 10f;
   [SerializeField] public float range = 100f;
    


    private void Awake()
    {
        cam = Camera.main.transform;
    }

    public void Shoot() 
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range)) 
        {
            print(hit.collider.name);
        }
    }
    // Update is called once per frame

}
