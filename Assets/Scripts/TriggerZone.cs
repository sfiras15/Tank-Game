using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private int roomIndex;
    [SerializeField] RoomText_SO roomTextSO;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            if (roomTextSO != null)
            {
                roomTextSO.roomIndex = this.roomIndex;
                roomTextSO.ChangeText();
            }
        }
    }
}
