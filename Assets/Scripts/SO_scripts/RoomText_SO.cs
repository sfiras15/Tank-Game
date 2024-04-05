using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/roomText")]
public class RoomText_SO : ScriptableObject
{
    public int roomIndex;

    public event Action onRoomTrigger;

    public void ChangeText()
    {
        if (onRoomTrigger != null)
        {
            onRoomTrigger.Invoke();
        }
    }
}
