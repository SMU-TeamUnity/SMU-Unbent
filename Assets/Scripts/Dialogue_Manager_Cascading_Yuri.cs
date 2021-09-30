using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager_Cascading_Yuri : MonoBehaviour
{

    //declare NPC objcet
    public NPC_Cascading npc;
    bool isTalking = false;
    private int CurrentGroup = 0;
    private int CurrentItem = 0;
    private int MaxnumItem = 5;


    float distance; //can't talk to someone if you're too far away
    int curResponseTracker = 0; //Index into second dimension of player_array (ie: the 3rd response of response set 1)
    int npcResponseIndex = 0; //Index into first dimension of player dialogue array (ie: the 1st set of responses)

    public GameObject player;       //anchors
    public GameObject dialogueUI;   //

    public Text npcName;            //
    public Text npcDialogueBox;     //
    public Text playerResponse;     //


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false); //want the UI to not be there at startup- only when talking
        CreateDialogue();
    }


    void Update()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position); //distance between the player and NPC
        if (distance <= 4f)
        {
            if (Input.GetKeyDown(KeyCode.E) && isTalking == false && npc.isDoneTalking == false)
            {
                StartConversation();
            }
            MainDialogue();
            Debug.Log("Finished");
        }
    }

    
    //private void OnMouseOver() //triggers when you mouse over the object
   // {
     //   npcResponseIndex = npc.score; //set index for npc responses before any text is drawn to the screen

        //only want the method to trigger if you are close enough to the NPC
       // distance = Vector3.Distance(player.transform.position, this.transform.position); //distance between the player and NPC
       // if (distance <= 4f)
       // {
            //scroll through responses via the scroll wheel
            /*
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                //find where to get responses from with array

                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogue[npcResponseIndex].Length() - 1)
                {
                    curResponseTracker = npc.playerDialogue[npcResponseIndex].Length() - 1; //making it so you can't scroll past all valid responses
                }
            }

            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0; //making it so you can't scroll past all valid responses
                }
            } */


            //trigger dialogue is on click (done with GetMouseButtonDown(0)
            //can also be done with keys using Input.GetKeyDown(KeyCode.E) --> can substitute with anything else
        //    if (Input.GetKeyDown(KeyCode.E) && isTalking == false && npc.isDoneTalking == false)
         //   {
           //     CreateDialogue();
         //       StartConversation();
         //   }





            /*
                        ///////////////////MAIN CONVERSATION DRIVER CODE////////////////////////////////////
                        //perhaps some sort of "while still isTalking"?

                        //dialogue option 1
                        if (npcResponseIndex == 0)
                        {
                            //dialogue option 1, response A- Correct
                            if (curResponseTracker == 0 && npc.playerDialogue[npcResponseIndex].Length() >= 0)
                            {
                                playerResponse.text = npc.playerDialogue[0].InnerString[0]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    npcDialogueBox.text = npc.npcDialogue[1]; //correct answer, so moves on
                                }
                            }

                            //dialogue option 1, response B- Incorrect
                            else if (curResponseTracker == 1 && npc.playerDialogue[npcResponseIndex].Length() >= 1)
                            {
                                playerResponse.text = npc.playerDialogue[0].InnerString[1]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    EndDialogue();
                                }
                            }

                            //dialogue option 1, response C- Incorrect
                            else if (curResponseTracker == 2 && npc.playerDialogue[npcResponseIndex].Length() >= 2)
                            {
                                playerResponse.text = npc.playerDialogue[0].InnerString[2]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    EndDialogue();
                                }
                            }

                        } //end dialogue option 1


                        //dialogue option 2
                        else if (npcResponseIndex == 1)
                        {
                            //dialogue option 2, response A- Correct
                            if (curResponseTracker == 0 && npc.playerDialogue[npcResponseIndex].Length() >= 0)
                            {
                                playerResponse.text = npc.playerDialogue[1].InnerString[0]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    npcDialogueBox.text = npc.npcDialogue[2]; //correct answer, so moves on
                                }
                            }

                            //dialogue option 2, response B- Correct
                            else if (curResponseTracker == 1 && npc.playerDialogue[npcResponseIndex].Length() >= 1)
                            {
                                playerResponse.text = npc.playerDialogue[1].InnerString[1]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    npcDialogueBox.text = npc.npcDialogue[2]; //correct answer, so moves on
                                }
                            }

                            //dialogue option 2, response C- Incorrect
                            else if (curResponseTracker == 2 && npc.playerDialogue[npcResponseIndex].Length() >= 2)
                            {
                                playerResponse.text = npc.playerDialogue[1].InnerString[2]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    EndDialogue();
                                }
                            }

                        } //end dialogue option 2

                        //dialogue option 3
                        else if (npcResponseIndex == 2)
                        {
                            //dialogue option 3, response A- Correct
                            if (curResponseTracker == 0 && npc.playerDialogue[npcResponseIndex].Length() >= 0)
                            {
                                playerResponse.text = npc.playerDialogue[2].InnerString[0]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    npcDialogueBox.text = npc.npcDialogue[3]; //correct answer, so moves on
                                }
                            }

                            //dialogue option 3, response B- Correct
                            else if (curResponseTracker == 1 && npc.playerDialogue[npcResponseIndex].Length() >= 1)
                            {
                                playerResponse.text = npc.playerDialogue[2].InnerString[1]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    npcDialogueBox.text = npc.npcDialogue[3]; //correct answer, so moves on
                                }
                            }

                            //dialogue option 3, response C- Incorrect
                            else if (curResponseTracker == 2 && npc.playerDialogue[npcResponseIndex].Length() >= 2)
                            {
                                playerResponse.text = npc.playerDialogue[2].InnerString[2]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    EndDialogue();
                                }
                            }

                        } //end dialogue option 3

                        //dialogue option 4
                        else if (npcResponseIndex == 3)
                        {
                            //dialogue option 4, response A- Correct
                            if (curResponseTracker == 0 && npc.playerDialogue[npcResponseIndex].Length() >= 0)
                            {
                                playerResponse.text = npc.playerDialogue[3].InnerString[0]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    dialogueOptionFive();
                                }
                            }

                            //dialogue option 4, response B- Correct
                            else if (curResponseTracker == 1 && npc.playerDialogue[npcResponseIndex].Length() >= 1)
                            {
                                playerResponse.text = npc.playerDialogue[3].InnerString[1]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    npc.score++;
                                    dialogueOptionFive();
                                }
                            }

                            //dialogue option 4, response C- Incorrect
                            else if (curResponseTracker == 2 && npc.playerDialogue[npcResponseIndex].Length() >= 2)
                            {
                                playerResponse.text = npc.playerDialogue[3].InnerString[2]; //set response to the right one
                                //if the player presses return on a highlighted option, select that option
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    EndDialogue();
                                }
                            }

                        } //end dialogue option 4

                        ///////////////////MAIN CONVERSATION DRIVER CODE////////////////////////////////////



            */
     //   }
   // } //end method
    



    void StartConversation()
    {
        isTalking = true;
        npcResponseIndex = 0;
        curResponseTracker = 0; //start the conversation with general introductory response
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.npcDialogue[0].InnerString[0]; //set it to greeting initial

    }



    void MainDialogue()
    {
        if (isTalking && CurrentGroup == 0)
        {
            npcDialogueBox.text = npc.npcDialogue[0].InnerString[0];
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CurrentGroup = 1;
                CurrentItem = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CurrentGroup = 2;
                CurrentItem = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CurrentGroup = 3;
                CurrentItem = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CurrentGroup = 4;
                CurrentItem = 0;
            }
            else
            {
                Debug.Log("Wrong Input");
                Debug.Log(Input.inputString);
            }
        }

        else if (isTalking)
        {
            SubDialogue();
        }

    }





    void SubDialogue()
    {

        if (CurrentItem != MaxnumItem)
        {
            npcDialogueBox.text = npc.npcDialogue[CurrentGroup].InnerString[CurrentItem];
            if (Input.GetKeyDown(KeyCode.Y))
            {
                CurrentItem++;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                CurrentItem = 0;
                CurrentGroup = 0;
            }
            else
            {
                Debug.Log("Wrong Input");
                Debug.Log(Input.inputString);
            }
        }
        else
        {
            npcDialogueBox.text = "Thank you for your response.\n" +
                "Do You want to know more about me? (Press Y for yes and N for No)";
            if (Input.GetKeyDown(KeyCode.N))
            {
                EndDialogue();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                CurrentItem = 0;
                CurrentGroup = 0;
            }
            else
            {
                Debug.Log("Wrong Input");
            }
        }
    }





    void EndDialogue()
    {
        isTalking = false;            //end current dialogue
        npc.isDoneTalking = true;     //make sure this NPC cannot be talked to again
        dialogueUI.SetActive(false);  //end current dialogue
        npc.score = npcResponseIndex; //assign score based on dialogue progression
    }

    void OnApplicationQuit()
    {
        //when the game is restarted, reset the values of the npc
        npc.isDoneTalking = false;
        npc.score = 0;
    }

 /*
    void dialogueOptionFive()
    {
            npcDialogueBox.text = npc.npcDialogue[4];
            playerResponse.text = "(Click to end dialogue)"; //hides this
        //final dialogue lasts for 4 seconds (average time to read it) at least and then can be clicked to exit
        Invoke("EndDialogue", 4);
    }
 */

    void CreateDialogue()
    {
        for (int i = 0; i < npc.npcDialogue.Length; i++)
        {
            npc.npcDialogue[i].InnerString.Clear();
        }
        npc.npcDialogue[0].InnerString.Add(
            "Welcome! Anything you want to know from me?\n" +
            "A(Press 1)\n" +
            "B(Press 2)\n" +
            "C(Press 3)\n" +
            "D(Press 4)\n"
        );

        using (var reader = new StreamReader("UnbentTest.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var newvalue = values[1] + " (Press Y for yes, N for No)";
                if (values[0][0] == 'A')
                {
                    npc.npcDialogue[1].InnerString.Add(newvalue);
                    Debug.Log(npc.npcDialogue[1].InnerString[0]);
                }
                else if(values[0][0] == 'B'){
                    npc.npcDialogue[2].InnerString.Add(newvalue);
                }
                else if (values[0][0] == 'C')
                {
                    npc.npcDialogue[3].InnerString.Add(newvalue);
                }
                else if (values[0][0] == 'D')
                {
                    npc.npcDialogue[4].InnerString.Add(newvalue);
                }
                else
                {
                    Debug.Log("Invalid Dialog");
                }
            }
        }
        CurrentGroup = 0;
        CurrentItem = 0;
        Debug.Log(npc.npcDialogue[1].InnerString[0]);
    }



} //end class
