using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Transform cam;

    private InputManager inputManager;

   [SerializeField] private float damage = 10f;
   [SerializeField] private float range = 100f;
    public Text Ammo;
    public Text TxtPoints;
    public GameObject reloadPrompt, MuzzleFlash;
    public Transform attackPoint;
    public int bulletsLeft = 35;
    public int magazineSize = 35;

    private int points = 0;
    
    

    private void Start()
    {
        inputManager = InputManager.Instance;
        //Ammo = GameObject.Find("BulletUI").GetComponent<Text>();
        //reloadPrompt = GameObject.Find("Reload");
        cam = Camera.main.transform;
    }

    private void Update()
    {
        UpdateUI();
        if (bulletsLeft > 0)
        {
            if (inputManager.PlayerOnClick())
            {
                Shoot();
                bulletsLeft--;
            }
            else if (inputManager.PlayerReload()) 
            {
                bulletsLeft = magazineSize;
            }
        }
        else 
        {
            reloadPrompt.SetActive(true);
            if (inputManager.PlayerReload()) 
            {
                reloadPrompt.SetActive(false);
                bulletsLeft = magazineSize;
            }
        }

        UpdatePoints();
       
    }
    private void UpdateUI() 
    {
        Ammo.text = bulletsLeft + " / " + magazineSize;
    }

    public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            print(hit.collider.name);
            if(hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().DamageEnemy();
                points += 55;
            }
        }

        GameObject flash = Instantiate(MuzzleFlash, attackPoint.position, Quaternion.identity);
        Destroy(flash, 0.1f);
    }
     public void ReloadFeedBack() 
     {

        reloadPrompt.SetActive(true);
            
       
     }

    private void UpdatePoints()
    {
        TxtPoints.text = points.ToString();
    }

}
