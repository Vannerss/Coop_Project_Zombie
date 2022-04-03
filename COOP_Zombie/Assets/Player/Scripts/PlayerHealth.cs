using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth = 100f;
    private float CurrentHealth;
    private int Heal = 5;
    public GameObject GameOver;
    [SerializeField]
    private Image HealthSlider;


    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        HealPlayer();
        UpdateHealthSlider();
    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            DamagePlayer(20);
        }
    }

    private void UpdateHealthSlider() 
    {
        if (CurrentHealth <= 0)
        {
            
            HealthSlider.fillAmount = CurrentHealth;
        }
        else
        {
            HealthSlider.fillAmount = CurrentHealth / MaxHealth;
        }
    }


    private void DamagePlayer(int damage) 
    {
        CurrentHealth -= damage;
       

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Died();

        }
       
        Debug.Log(CurrentHealth);
    }


    private void HealPlayer() 
    {
       
        if (CurrentHealth < MaxHealth) 
        {
            CurrentHealth += .5f * Time.deltaTime;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }
            

        
    }

    
    private void Died()
    {
        
      Time.timeScale = 0;
      GameOver.SetActive(true);
      Cursor.lockState = CursorLockMode.Confined;
        
        
    }

}
