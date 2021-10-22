using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour
{
    public List<InteractableObject> useableItemList;

    public Dictionary<string, string> examineDictonary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictonary = new Dictionary<string, string>();
   

    [HideInInspector] public List<string> nounsInRoom = new List<string>();

    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

    List<string> nounsInInventory = new List<string>();

    gameController controller;

    private void Awake()
    {
        controller = GetComponent<gameController>();
    }

    public string GetObjectsNotInInventory(Room currentRoom,int i)
    {
        InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom[i];

        if (!nounsInInventory.Contains(interactableInRoom.noun))
        {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.descriptiion;
        }

        return null;
    }

    public void AddActionResponsesToUseDictionary() //adds items to invenroty so you can use them afte rpickign them up
    {
        for (int i = 0; i < nounsInInventory.Count; i++)
        {
            string noun = nounsInInventory[i];

            InteractableObject interactableObjectInInventory = GetInteractableObjectFromUseableList(noun);
            if(interactableObjectInInventory == null)
                continue;
            for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++)
            {
                Interaction interaction = interactableObjectInInventory.interactions[j];
                if (interaction.actionResponse == null)
                    continue;

                if (!useDictionary.ContainsKey(noun))
                {
                    useDictionary.Add(noun, interaction.actionResponse);
                }
            }

        }
    }

    InteractableObject GetInteractableObjectFromUseableList(string noun)
    {
        for (int i = 0; i < useableItemList.Count; i++)
        {
            if(useableItemList[i].noun == noun)
            {
                return useableItemList[i];
            }
          
        }
        return null;
    }

    public void DisplayInventory()
    {
        controller.LogStringWithReturn("you rumage through your bags and pockets... You find the following:");
            for (int i = 0; i < nounsInInventory.Count; i++)
        {
            controller.LogStringWithReturn(nounsInInventory[i]);
        }
    }

    public void ClearCollections()
    {
        examineDictonary.Clear();
        takeDictonary.Clear();
        nounsInRoom.Clear();
        
    }

    public Dictionary<string,string> Take (string[] seperatedInputWords)
    {
        string noun = seperatedInputWords[1];

        if (nounsInRoom.Contains(noun))
        {
            nounsInInventory.Add(noun);
            nounsInRoom.Remove(noun);
            AddActionResponsesToUseDictionary();
            return takeDictonary;
        }
        else
        {
            controller.LogStringWithReturn("There is no " + noun + " here to take.");
            return null;
        }
    }

    public void useItem(string[] seperatedInputWords)
    {
        string nounToUse = seperatedInputWords[1];

        if (nounsInInventory.Contains(nounToUse))
        {
            if (useDictionary.ContainsKey(nounToUse))
            {
                bool actionResult = useDictionary[nounToUse].DoActionResponse(controller);
                if (!actionResult) //if the action fails
                {
                    controller.LogStringWithReturn("Hmm. Nothing Happens");
                }
            }
            else
            {
                controller.LogStringWithReturn("You Can't use the " + nounToUse);//if the action can not be done
            }
        }
        else
        {
            controller.LogStringWithReturn ("there is no " + nounToUse + " in your inventory to use");
        }
    }

}
//This inventory system is very simple. Items can either be in the inventory or in the room it belongs to.
//Will need to make it more complex if i want to place items, drop items, sell items, eat items. destroy mand so on.