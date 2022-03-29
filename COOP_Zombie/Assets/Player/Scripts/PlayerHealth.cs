using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 100f;
    [SerializeField] private float HealAmount = .5f;

    private float CurrentHealth;
    
    //Needed Objects.
    public GameObject GameOver;
    private Image HealthSlider;


    private void Start()
    {
        GameOver = GameObject.Find("GameOverMenu"); //The Game over gameobject will always need to be called GameOverMenu or it will not work.
        HealthSlider = GameObject.Find("Bar").GetComponent<Image>(); //The healthslider gameobject always need to be called Bar or it will not work.
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
            CurrentHealth += HealAmount * Time.deltaTime;
            if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        }
    }

    private void Died()
    {
      Time.timeScale = 0; //This is a problem, you cannot set timescale to 0 while on multiplayer. NEEDS FIX.
      GameOver.SetActive(true);
      Cursor.lockState = CursorLockMode.Confined; //This will lock player mouse even when he enters spectator mode. we need to remember to give access back on spectator mode.
    }
}
