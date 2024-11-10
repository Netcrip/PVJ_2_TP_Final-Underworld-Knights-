using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] float _maxStamina,_staminaRegeneration;
    [SerializeField] float _staminaRegenerationCD,_staminaDecreaseCD;
    public bool CanStaminaRegeneration { get; set; } = true;
    [SerializeField] float stamina;
    bool canUseStamina = true;


    //Eventos
    public Action<float, float> onStaminaRegeneration;
    public Action<float, float> onStaminaDecrease;

    // Start is called before the first frame update
    void Start()
    {
        stamina=_maxStamina;
        UiManager.Instance.PlayerStamina(this);
        GameObject chilObj= GameObject.FindGameObjectWithTag("CanvaStamina");
        chilObj.transform.SetParent(this.gameObject.transform,true);
    }

    private void Update()
    {
        StaminaRegeneration();
        CanUseStamina();
    }

    void StaminaRegeneration()
    {
        if (CanStaminaRegeneration && stamina<100) {
   
            if (stamina > _maxStamina)
            {
                stamina=_maxStamina;
                onStaminaRegeneration?.Invoke(stamina, _maxStamina);
            }
            else if(stamina < _maxStamina)
            {
                stamina += _staminaRegeneration * Time.deltaTime;
                onStaminaRegeneration?.Invoke(stamina, _maxStamina);
            }
            
        }
        else if( stamina>_maxStamina ) 
        {
            stamina = _maxStamina;
            onStaminaRegeneration?.Invoke(stamina, _maxStamina);
        }
    }

    void StaminaDecrease(float drecreaseAmount)
    {
        stamina -= drecreaseAmount;
        onStaminaDecrease?.Invoke(stamina, _maxStamina);
            
        
    }
    private void CanUseStamina()
    {
        if (stamina <= 5)
            canUseStamina = false;
        else if(stamina == _maxStamina)
            canUseStamina = true;
    }
    public bool StaminaUse(float drecreaseAmount)
    {
        float decrease = drecreaseAmount * Time.deltaTime;
        if (stamina - decrease >= 0 && canUseStamina)
        {
            StaminaDecrease(decrease);
            return true;
        }
        else
            return false;
    }
}
