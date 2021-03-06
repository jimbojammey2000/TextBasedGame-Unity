using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TextAdventure/Interactable Object")]

public class InteractableObject : ScriptableObject
{

    public string noun = "Name";
    [TextArea]
    public string descriptiion = "Description in room";
    public Interaction[] interactions;
}
