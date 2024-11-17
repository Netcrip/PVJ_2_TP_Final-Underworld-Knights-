using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] string scene;// Start is called before the first frame update
   private void OnTriggerEnter(Collider other) {
      if (other.gameObject.CompareTag("Player"))
    {
        GameManager.Instance.LoadScene(scene);
    }
   }
}
