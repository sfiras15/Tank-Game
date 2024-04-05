using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] LayerMask whatIsCollision;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float lifeTime = 1.5f;

    private float currentLifeTime = 0f;

    private void Start()
    {
        currentLifeTime = 0f;
    }
    private void Update()
    {
        currentLifeTime+= Time.deltaTime;
        if (currentLifeTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object's layer is included in the whatIsCollision layer mask
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players") || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")
            
            || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Bounds bounds = collision.collider.bounds;
            Vector3 impactPoint = bounds.ClosestPoint(transform.position);

            // Instantiate the explosion effect
            GameObject explosion = Instantiate(explosionPrefab, impactPoint, Quaternion.identity);

            // Rotate the explosion effect to match the surface normal
            if (collision.contacts.Length > 0)
            {
                Vector3 normal = collision.contacts[0].normal;
                explosion.transform.rotation = Quaternion.LookRotation(normal);
            }


            // Play the explosion effect
            ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem>();
            particleSystem.Play();

            // Destroy the explosion effect after it finishes playing
           
            Destroy(explosion, particleSystem.main.duration);

            // Destroy the shell GameObject
            Destroy(gameObject);
        }
    }
}
