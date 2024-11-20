using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] string scene;// Start is called before the first frame update
    private void OnTriggerStay(Collider other) {

    if(Input.GetKey(KeyCode.G) && other.CompareTag("Player"))
    {    
        other.GetComponent<PlayerHealth>().Unsuscribe();
        GameManager.Instance.LoadScene(scene,true);
        
    }
   }
}
