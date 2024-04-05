using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TankBarrel : MonoBehaviour
{
    [SerializeField] private bool isPlayerControlled = false;
    [Header("Aim")]
    [SerializeField] private GameObject tankTurret;
    [SerializeField] private float rotationSpeed = 180f; // Vitesse de rotation fixe du canon en degrés par seconde
    private Quaternion targetRotation;

    [Header("QuickFire")]
    [SerializeField] private KeyCode fireButton;
    [SerializeField] private float fireRate;
    private bool firing = false;
    [Header("ChargedAttack")]
    
    [SerializeField] private KeyCode chargeAttackButton;
    [SerializeField] private GameObject aimArrow;
    private SpriteRenderer aimArrowRenderer;
    private bool charging = false;
    private float currentCharge = 0f;
    private float chargeTime = 2f;


    [SerializeField] private GameObject tankBarrel;
    [SerializeField] private GameObject shell;


    private Camera cam;
   

    private void Awake()
    {
        if (isPlayerControlled)
        {
            cam = Camera.main;
            if (aimArrow != null)
            {
                aimArrowRenderer = aimArrow.GetComponentInChildren<SpriteRenderer>();
            }
        }
        
    }
    private void Start()
    {
        if (isPlayerControlled) aimArrowRenderer.enabled = false;

    }

    void Update()
    {
        if (isPlayerControlled)
        {
            Aim();
            if (Input.GetKey(chargeAttackButton) && !charging && !firing)
            {

                charging = true;
                currentCharge = 0f;
                aimArrowRenderer.enabled = true;
            }
            if (charging)
            {
                currentCharge += Time.deltaTime;

                // Handles the arrow lengthning logic
                float height = Mathf.Lerp(2.56f, 2.56f * 2f, Mathf.Clamp01(currentCharge / chargeTime) /** arrowChargeSpeed*/);
                aimArrowRenderer.size = new Vector2(aimArrowRenderer.size.x, height);
            }
            if ((currentCharge >= chargeTime || Input.GetKeyUp(chargeAttackButton)))
            {
                //Debug.Log("charged attack : " + currentCharge);
                charging = false;
                Fire(currentCharge);
                aimArrowRenderer.enabled = false;
            }
            if (Input.GetKeyDown(fireButton))
            {
                Fire();
            }

        }

    }
    public void Fire(float duration = -1f)
    {
        if (!firing)
        {
            firing = true;

            // Créer et initialiser l'obus
            GameObject newShell = Instantiate(shell, tankBarrel.transform.position + tankBarrel.transform.forward, Quaternion.identity);
            Rigidbody shellRigidbody = newShell.GetComponent<Rigidbody>();
            if (duration < 0f)
            {
                shellRigidbody.velocity = tankBarrel.transform.forward * 20f;
            }
            else
            {
                //interpolate the force based on the duration of the charge
                float speed = Mathf.Lerp(20f, 40f, Mathf.Clamp01(duration / chargeTime));
                //Debug.Log(speed);
                shellRigidbody.velocity = tankBarrel.transform.forward * speed;
            }
            

            // Démarrer la coroutine pour réinitialiser le tir après un certain délai
            StartCoroutine(ResetAttack(fireRate));
        }
    }


    private IEnumerator ResetAttack(float duration)
    {
        yield return new WaitForSeconds(duration);
        firing = false;
    }
    private void Aim()
    {
        // Convertir la position de la souris en un rayon dans le monde
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // Plan horizontal à l'altitude Y=0
        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            // Obtenir le point d'intersection du rayon avec le plan
            Vector3 targetPoint = ray.GetPoint(distance);

            // Orienter le canon vers le point d'intersection
            RotateBarrelTowards(targetPoint);
        }
    }
    public void RotateBarrelTowards(Vector3 targetPoint)
    {
        // Orienter le canon vers le point d'intersection
        Vector3 direction = targetPoint - tankTurret.transform.position;
        direction.y = 0f; // Assurer que la rotation reste dans le plan horizontal
        targetRotation = Quaternion.LookRotation(direction);

        // Appliquer une interpolation linéaire entre la rotation actuelle et la rotation souhaitée
        tankTurret.transform.rotation = Quaternion.RotateTowards(tankTurret.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
