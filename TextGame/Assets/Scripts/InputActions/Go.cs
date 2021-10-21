using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName="TextAdventure/InputActions/go")]
public class Go : InputAction
{
    public override void RespondToInput(gameController controller, string[] seperatedInputWords)
    {
        controller.roomNavigation.AttemptToChangeRooms(seperatedInputWords[1]); //second word is 1. "go NORTH"  " go SOUTH"
    }
}
