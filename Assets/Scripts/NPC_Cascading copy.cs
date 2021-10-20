using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NPC file Cascading 2", menuName = "NPC Files Archive Cascading 2")]
public class NPC_Cascading_2 : ScriptableObject
{
    //define what we need our NPC to have
    public string name;
    [TextArea(3, 15)]
    public StringA[] npcDialogue; //npc's dialogue - is not nested because it's the same for responses as long as you advance
    //[TextArea(3, 15)]
    public StringA[] playerDialogue; //player responses - is nested because you have different responses to the 1st thing they say, the second, etc

    //this may not need to be used anymore
    public int score = 0; // used to figure out what responses for the player should be displayed (ie: 0 = their 1st message responses, 1 = their 2nd, etc)
    public bool isDoneTalking = false; //Used to determine if an character is still interactable (since after you "fail", you should not be able to "try again"?)




} //end class





[System.Serializable]
public class StringA_2
{
    [TextArea(3, 15)]
    //public string[] InnerString;
    public List<string> InnerString;

    public int Length()
    {
        return InnerString.Count;
    }
} //end class

