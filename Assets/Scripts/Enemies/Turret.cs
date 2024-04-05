using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int healthPoints = 4;
    [SerializeField] private Health_SO healthSO;


    [SerializeField] private Door_State_SO doorStateSO;


    [Header("Aim")]
    [SerializeField] private float rotationSpeed = 180f; // Vitesse de rotation fixe du canon en degrés par seconde
    [SerializeField] private GameObject turretPivot;
    [SerializeField] private GameObject turretBarrel;
    private Quaternion targetRotation;

    [Header("Fire")]
    [SerializeField] private float attackDistance = 25f;
    [SerializeField] private float fireRate = 1f;
    private bool firing = false;
    [SerializeField] private GameObject shellPrefab;

    private Transform player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        healthSO.MaxHealth = healthPoints;
        healthSO.CurrentHealth = healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && turretPivot != null)
        {
            RotateBarrelTowards(player.position);

            if (Vector3.Distance(transform.position, player.position) <= attackDistance)
            {
                Fire();
            }
        }
       
    }
    private void Fire()
    {
        if (!firing)
        {
            firing = true;

            // Créer et initialiser l'obus
            if (shellPrefab != null)
            {
                GameObject newShell = Instantiate(shellPrefab, turretBarrel.transform.position + turretBarrel.transform.forward, Quaternion.identity);
                if (newShell != null)
                {
                    Rigidbody shellRigidbody = newShell.GetComponent<Rigidbody>();
                    shellRigidbody.velocity = turretBarrel.transform.forward * 15f;

                }
                // Démarrer la coroutine pour réinitialiser le tir après un certain délai
                StartCoroutine(ResetAttack(fireRate));
            } 
        }
    }
    private IEnumerator ResetAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        firing = false;
    }
    void RotateBarrelTowards(Vector3 targetPoint)
    {
        // Orienter le canon vers le point d'intersection
        Vector3 direction = targetPoint - turretPivot.transform.position;
        direction.y = 0f; // Assurer que la rotation reste dans le plan horizontal
        targetRotation = Quaternion.LookRotation(direction);

        // Appliquer une interpolation linéaire entre la rotation actuelle et la rotation souhaitée
        turretPivot.transform.rotation = Quaternion.RotateTowards(turretPivot.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
                    if (doorStateSO != null)
                    {
                        //doorStateSO.turretKilled = true;
                        doorStateSO.OpenDoor();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
