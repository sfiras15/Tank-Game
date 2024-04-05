using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 360f;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Contr�les de d�placement avant/arri�re et rotation
        float horizontalInput = Input.GetAxis("Horizontal1");
        float verticalInput = Input.GetAxis("Vertical1");

        // D�placement avant/arri�re
        Vector3 forwardMovement = transform.forward * verticalInput * movementSpeed * Time.deltaTime;
        characterController.Move(forwardMovement);

        // Rotation
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }
}
