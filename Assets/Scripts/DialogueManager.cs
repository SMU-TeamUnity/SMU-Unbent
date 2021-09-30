using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //declare NPC objcet
    public NPC npc;
    bool isTalking = false;

    float distance; //can't talk to someone if you're too far away
    float curResponseTracker = 0; //for NPC to respond accordingly based on what is said

    public GameObject player;       //anchors
    public GameObject dialogueUI;   //

    public Text npcName;            //
    public Text npcDialogueBox;     //
    public Text playerResponse;     //

    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false); //want the UI to not be there at startup- only when talking
    }

    private void OnMouseOver() //triggers when you mouse over the object
    {

        //only want the method to trigger if you are close enough to the NPC
        distance = Vector3.Distance(player.transform.position, this.transform.position); //distance between the player and NPC
        if (distance <= 4f)
        {
            //scroll through responses via the scroll wheel
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1; //making it so you can't scroll past all valid responses
                }
            }

            else if (Input.GetAxis("Mouse ScrollWheel") > 0f) 
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0; //making it so you can't scroll past all valid responses
                }
            }


            //trigger dialogue is on click (done with GetMouseButtonDown(0)
            //can also be done with keys using Input.GetKeyDown(KeyCode.E) --> can substitute with anything else
            if (Input.GetMouseButtonDown(0) && isTalking == false)
            {
                StartConversation();
            }
            else if (Input.GetMouseButtonDown(0) && isTalking) { //if already talking, we can end the dialogue with a click- it might be better to set this as a "end dialogue" moniker
                EndDialogue();
            }

            //dialogue option 1, response 1
            if(curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                playerResponse.text = npc.playerDialogue[0];
                //if the player presses return on a highlighted option, selection that option
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }

            //dialogue option 1, response 2
            else if (curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];
                //if the player presses return on a highlighted option, selection that option
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                }
            }

            //dialogue option 1, response 3
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                //if the player presses return on a highlighted option, selection that option
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }


        }
    }


    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0; //start the conversation with general introductory response
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0]; //set it to greeting initial
    }



    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }


}
