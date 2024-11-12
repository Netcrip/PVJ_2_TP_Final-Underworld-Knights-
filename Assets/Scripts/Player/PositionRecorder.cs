using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRecorder : MonoBehaviour
{
    public Vector3 lastSafePosition; // Última posición segura del jugador
    public float waitSeconds = 5f;

    // Start is called before the first frame update
    void Start()
    {
        lastSafePosition = transform.position;
        StartCoroutine(UpdatePositionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdatePositionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            lastSafePosition = transform.position;
        }
    }
}
