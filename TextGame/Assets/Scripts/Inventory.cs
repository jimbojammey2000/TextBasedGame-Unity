using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/InputActions/Inventory")]
public class Inventory : InputAction
{
    public override void RespondToInput(gameController controller, string[] seperatedInputWords)
    {
        controller.interactableItems.DisplayInventory();
    }
}
