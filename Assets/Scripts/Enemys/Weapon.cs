using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float timeToDestroy = 2f;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damageAttack;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioClip rockSFX;
    [SerializeField] private AudioSource sfx;
    private bool oneExplosion=false;

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
      
        if (timeToDestroy <= 0)
        {
            //Destroy
            //instanciar efectos de explosion
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //sfx.PlayOneShot(rockSFX);
        Collider[] collidedObjects = Physics.OverlapSphere(transform.position, explosionRadius, layerMask);
        if(collidedObjects.Length > 0) { 
             foreach (Collider collidedObject in collidedObjects)
             {          
                if (collidedObject is CapsuleCollider){
                    //Debug.Log(collidedObject);
                    PlayerHealth isplayer = collidedObject.GetComponent<PlayerHealth>();
                    if (isplayer != null)
                    {

                        Vector3 diffVector=isplayer.transform.position - transform.position;
                        isplayer.Damage(damageAttack/diffVector.magnitude);
                    }
                }
                
             }

        }
        if (!oneExplosion)
        {
            //GameObject explosionFX = Instantiate(explosion, transform.position, Quaternion.identity);
            //Destroy(explosionFX, 1f);
            oneExplosion = true;
        }
       
        BoxCollider sCollider = gameObject.GetComponent<BoxCollider>();
        MeshRenderer mRenderer = GetComponent<MeshRenderer>();
        mRenderer.enabled = false;
        sCollider.enabled = false;
        Destroy(gameObject,2f);
    }

    private void OnDrawGizmosSelected()    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
