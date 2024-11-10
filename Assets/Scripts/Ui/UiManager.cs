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

    [SerializeField] private Image _greenWheel,_redWheel, _staminaWheel;
    

    [SerializeField ]private Image healthBar;


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
        
        if (_greenWheel.fillAmount >= 0.99)
        {
            staminaBarCD+= Time.deltaTime;
        }
        else
        {
            _staminaWheel.gameObject.SetActive(true);
            staminaBarCD = Time.deltaTime;
        }
            
        if (staminaBarCD >staminaBarShow)
        {
            _staminaWheel.gameObject.SetActive(false);
        }
        else
            _staminaWheel.gameObject.SetActive(true);
    }

    public void PlayerStamina(PlayerStamina stamina)
    {
        staminaInstance = stamina;
        staminaInstance.onStaminaRegeneration += StaminaRegeneration;
        staminaInstance.onStaminaDecrease += StaminaDecrease;

    }
    public void PlayerHealth(PlayerHealth Health)
    {
        healthInstance = Health;
        healthInstance.onHealthchange = HealtChange;
        healthInstance.onDead += DoOnDead;
    }
    
    private void StaminaDecrease(float stamina, float maxStamina)
    {
        _staminaWheel.enabled = true;
        _redWheel.fillAmount = (stamina / maxStamina);
        _greenWheel.fillAmount = (stamina/ maxStamina-0.05f);
    }
    private void StaminaRegeneration(float stamina, float maxStamina)
    {
        if(_greenWheel.fillAmount < 0.05) 
        { 
            _greenWheel.enabled = false;
        }
        if (_greenWheel.fillAmount >= 1)
        {
            _greenWheel.enabled = true;
        }


        _redWheel.fillAmount = (stamina / maxStamina);
        _greenWheel.fillAmount = (stamina / maxStamina);
    }
    private void DoOnDead()
    {
        staminaInstance.onStaminaRegeneration -= StaminaRegeneration;
        staminaInstance.onStaminaDecrease -= StaminaDecrease;
        healthInstance.onDead -= DoOnDead;
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
