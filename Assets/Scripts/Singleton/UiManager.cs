using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance => instance;
    private static UiManager instance;
    public PlayerStamina StaminaInstance => staminaInstance;
    private PlayerStamina staminaInstance;
    public PlayerHealth HealthInstance => healthInstance;
    private PlayerHealth healthInstance;
    [SerializeField] float staminaBarShow=5;
    private float staminaBarCD;

    [SerializeField] private Image greenWheel,redWheel, staminaWheel,healthBar;
    

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        HideStaminaBar();
    }
    private void HideStaminaBar()
    {
        
        if (greenWheel.fillAmount >= 0.99)
        {
            staminaBarCD+= Time.deltaTime;
        }
        else
        {
            staminaWheel.gameObject.SetActive(true);
            staminaBarCD = Time.deltaTime;
        }
            
        if (staminaBarCD >staminaBarShow)
        {
            staminaWheel.gameObject.SetActive(false);
        }
        else
            staminaWheel.gameObject.SetActive(true);
    }

    public void PlayerStamina(PlayerStamina stamina)
    {
        staminaInstance = stamina;
        staminaInstance.onStaminaRegeneration += StaminaRegeneration;
        staminaInstance.onStaminaDecrease += StaminaDecrease;

    }
    public void PlayerHealth(PlayerHealth Health)
    {     healthInstance = Health;
        healthInstance.onHealthchange += HealtChange;
        healthInstance.onDead += DoOnUnsuscribe;
        healthInstance.onUnsuscribe += DoOnUnsuscribe;
        SetCamvas();
    }
    
    private void StaminaDecrease(float stamina, float maxStamina)
    {
        staminaWheel.enabled = true;
        redWheel.fillAmount = (stamina / maxStamina);
        greenWheel.fillAmount = (stamina/ maxStamina-0.05f);
    }
    private void StaminaRegeneration(float stamina, float maxStamina)
    {
        if(greenWheel.fillAmount < 0.05) 
        { 
            greenWheel.enabled = false;
        }
        if (greenWheel.fillAmount >= 1)
        {
            greenWheel.enabled = true;
        }


        redWheel.fillAmount = (stamina / maxStamina);
        greenWheel.fillAmount = (stamina / maxStamina);
    }
    private void DoOnUnsuscribe()
    {
        staminaInstance.onStaminaRegeneration -= StaminaRegeneration;
        staminaInstance.onStaminaDecrease -= StaminaDecrease;
        healthInstance.onDead -= DoOnUnsuscribe;
        healthInstance.onUnsuscribe -= DoOnUnsuscribe;
    }

    private void SetCamvas(){
        greenWheel = GameObject.Find("GreenWheel")?.GetComponent<Image>();
        redWheel = GameObject.Find("RedWheel")?.GetComponent<Image>();
        staminaWheel = GameObject.Find("StaminaWheelBG")?.GetComponent<Image>();
        healthBar = GameObject.Find("HealthBar")?.GetComponent<Image>();
    }

    private void HealtChange(float healt,float maxHealt)
    {
        float healtBar = healt / maxHealt;
        healthBar.fillAmount = healtBar;
        if (healtBar > 0.75)
        {
            healthBar.color = new Color32(0, 166, 34, 255);
        }
        else if (healtBar > 0.25)
        {
            healthBar.color = new Color32(255, 161, 0, 255);
        }
        else
            healthBar.color = new Color32(255, 0, 0, 255);
    }
}
