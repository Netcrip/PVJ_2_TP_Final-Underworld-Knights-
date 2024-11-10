using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSet : MonoBehaviour
{
    [SerializeField] private GameObject KnightA,KnightB,KnightC,KnightD,KnightE;


    void Start()
    {
        switch (GameManager.Instance.avatarSelection){
            case "KnightA":              
                KnightA.SetActive(true);        
            break;
            case "KnightB":            
                KnightB.SetActive(true);
                break;
            case "KnightC":           
                KnightC.SetActive(true);
                break;
            case "KnightD":             
                KnightD.SetActive(true);  
                break;
            case "KnightE":            
                KnightE.SetActive(true);
                break;
        }
    }

   
}
