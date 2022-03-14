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
    private GameObject GameOver;
    [SerializeField]
    private Image HealthSlider;


    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") 
        {
            DamagePlayer(20);
        }
    }


    private void DamagePlayer(int damage) 
    {
        CurrentHealth -= damage;
       

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            HealthSlider.fillAmount = CurrentHealth;

        }
        else 
        {
            HealthSlider.fillAmount = CurrentHealth / MaxHealth;
        }
        Debug.Log(CurrentHealth);
    }


    private void HealPlayer() 
    {
        if (CurrentHealth > 100) 
        {
            CurrentHealth = MaxHealth;
           
        }
    }
}
