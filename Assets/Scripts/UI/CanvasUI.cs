using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roomText;
    [SerializeField] RoomText_SO roomTextSO;


    private void OnEnable()
    {
        if (roomTextSO != null) roomTextSO.onRoomTrigger += UpdateTextRoom;

    }
    private void OnDisable()
    {
        if (roomTextSO != null) roomTextSO.onRoomTrigger -= UpdateTextRoom;
    }

    public void UpdateTextRoom()
    {
        if (roomTextSO != null)
        {
            roomText.text = "Room " + roomTextSO.roomIndex.ToString(); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        roomText.text = "Room 1";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
