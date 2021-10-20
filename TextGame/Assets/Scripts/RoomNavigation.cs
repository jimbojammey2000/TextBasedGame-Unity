using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour
{
    public Room currentRoom;

    gameController controller;


    private void Awake()
    {
        controller = GetComponent<gameController>();
    }

    public void UnpackExitsInRoom()
    {
        for(int i =0; i < currentRoom.exits.Length; i++)
        {
            controller.interactionDescriptionsInRoom.Add(currentRoom.exits[i].exitDescription);
        }
    }


}


   