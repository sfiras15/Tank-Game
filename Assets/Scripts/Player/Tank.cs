using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Tank : MonoBehaviour
{
    [SerializeField] private int healthPoints = 1;
    [SerializeField] private GameObject vehiculeExplosionFx;
    [SerializeField] private Health_SO healthSO;
    // Start is called before the first frame update
    void Start()
    {
        healthSO.MaxHealth = healthPoints;
        healthSO.CurrentHealth = healthPoints;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            if (healthPoints > 0)
            {
                healthPoints--;
                healthSO.CurrentHealth = healthPoints;
                if (healthPoints == 0)
                {
                   
                    DestroyPlayer();
                    
                }
            }
        }
    }
    
    private void DestroyPlayer()
    {
        if (vehiculeExplosionFx != null)
        {
            // Instanciate the explosion effect at the same position as the door
            GameObject explosion = Instantiate(vehiculeExplosionFx, transform.position, Quaternion.identity);
            ParticleSystem explosionParticleSystem = explosion.GetComponent<ParticleSystem>();

            // Play the explosion effect
            explosionParticleSystem.Play();

           
        }
        if (gameObject.tag == "Player") GameManager.instance.Defeat();
        if (gameObject.tag == "Enemy") GameManager.instance.Victory();
        Destroy(gameObject);

    }
}
