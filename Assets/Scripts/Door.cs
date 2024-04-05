using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // References
    private Renderer renderer;
    [SerializeField] private Material destructableMaterial;
    [SerializeField] private Door_State_SO door_StateSO;

    //Flashing 
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private GameObject doorExplosionFx;
    private Color originalColor;

    // DoorState
    [SerializeField] private int hitPoints = 3;
    private bool vulnerable;

    private void OnEnable()
    {
        if (door_StateSO != null)
        {
            door_StateSO.onTurretKilled += ChangeDoorMat;
        }
    }
    private void OnDisable()
    {
        if (door_StateSO != null)
        {
            door_StateSO.onTurretKilled -= ChangeDoorMat;
        }
    }
    private void ChangeDoorMat()
    {
        vulnerable = true;
        //doorMaterial.color = destructableMaterial.color;
        originalColor = destructableMaterial.color;
        renderer.material = destructableMaterial;
        //doorMaterial = destructableMaterial;
    }

    private void Start()
    {
        // if it's a destructableDoor we don't initialize this variable
        vulnerable = destructableMaterial == null;
    }
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            //doorMaterial = renderer.material;
            originalColor = renderer.material.color; // Enregistrer la couleur d'origine
        }
        else
        {
            Debug.LogError("No renderer found on the door object!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Shell") && vulnerable)
        {
            if (hitPoints > 0)
            {
                StartCoroutine(Flash(flashDuration));
                hitPoints--;
                if (hitPoints == 0) DestroyDoor();
            }
            
        }
    }

    private IEnumerator Flash(float duration)
    {
        // Changer la couleur du matériau de la porte en blanc
        renderer.material.color = flashColor;

        // Attendre une petite période
        yield return new WaitForSeconds(duration); // Vous pouvez ajuster la durée du flash selon vos besoins

        // Restaurer la couleur d'origine du matériau de la porte
        renderer.material.color = originalColor;
    }

    private void DestroyDoor()
    {
        if (doorExplosionFx != null)
        {
            // Instanciate the explosion effect at the same position as the door
            GameObject explosion = Instantiate(doorExplosionFx, transform.position, Quaternion.identity);
            ParticleSystem explosionParticleSystem = explosion.GetComponent<ParticleSystem>();

            // Play the explosion effect
            explosionParticleSystem.Play();

        }

        // Destroy the door
        Destroy(gameObject);
    }

}
