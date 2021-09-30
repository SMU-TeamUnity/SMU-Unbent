using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC Files Archive")]
public class NPC : ScriptableObject
{
    //define what we need our NPC to have
    public string name;
    [TextArea(3, 15)]
    public string[] dialogue; //his/her lines of speech
    [TextArea(3, 15)]
    public string[] playerDialogue; //player responses
    public float score = 0; // used to figure out where in the chain the NPC should be talking (ie: 0 = their 1st message, 1 = their 2nd, etc)

}
