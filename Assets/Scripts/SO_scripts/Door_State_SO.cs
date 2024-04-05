using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/DoorState")]
public class Door_State_SO : ScriptableObject
{
    public bool turretKilled = false;

    public event Action onTurretKilled;

    public void OpenDoor()
    {
        if (onTurretKilled != null)
        {
            onTurretKilled.Invoke();
        }
    }
}
