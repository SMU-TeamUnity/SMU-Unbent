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
    FPInput newplayer;

    public Text npcName;            //
    public Text npcDialogueBox;     //
    public Text playerResponse;     //
    public int npcNum;
    public int numNPCs;

    public List<bool> branchfinished;


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false); //want the UI to not be there at startup- only when talking
        CreateDialogue();
        newplayer = player.GetComponent<FPInput>();
        newplayer.InitializeScore(2,4);
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


    void StartConversation()
    {
        isTalking = true;
        npcResponseIndex = 0;
        curResponseTracker = 0; //start the conversation with general introductory response
        dialogueUI.SetActive(true);
        npcName.text = npc.name;

        string tempstr = npc.npcDialogue[0].InnerString[0];

        for(int i = 0; i < branchfinished.Count; i++)
        {
            if (!branchfinished[i])
                tempstr = tempstr + npc.npcDialogue[0].InnerString[i + 1];
        }
        tempstr = tempstr + "I'll need to go. See u next time! (Press N)\n";


        npcDialogueBox.text = tempstr;
        playerResponse.text = "";
    }



    void MainDialogue()
    {
        if (isTalking && CurrentGroup == 0)
        {
            string tempstr = npc.npcDialogue[0].InnerString[0];

            for (int i = 0; i < branchfinished.Count; i++)
            {
                if (!branchfinished[i])
                    tempstr = tempstr + npc.npcDialogue[0].InnerString[i + 1];
            }
            tempstr = tempstr + "I'll need to go. See u next time! (Press N)\n";

            npcDialogueBox.text = tempstr;

            if (Input.GetKeyDown(KeyCode.Alpha1) && branchfinished[0] == false)
            {
                CurrentGroup = 1;
                CurrentItem = 0;
                branchfinished[0] = true;
                newplayer.IncScore(npcNum,CurrentGroup - 1, 0.2);
                Debug.Log("this is " + newplayer.Totalscore);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && branchfinished[1] == false)
            {
                CurrentGroup = 2;
                CurrentItem = 0;
                branchfinished[1] = true;
                newplayer.IncScore(npcNum,CurrentGroup - 1, 0.2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && branchfinished[2] == false)
            {
                CurrentGroup = 3;
                CurrentItem = 0;
                branchfinished[2] = true;
                newplayer.IncScore(npcNum,CurrentGroup - 1, 0.2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && branchfinished[3] == false)
            {
                CurrentGroup = 4;
                CurrentItem = 0;
                branchfinished[3] = true;
                newplayer.IncScore(npcNum,CurrentGroup - 1, 0.2);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                EndDialogue();
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
            playerResponse.text = "Oh! I'd like to know more about it! (Press Y)\n"
            + "I've got everything I want, thank you! (Press N)";
            if (Input.GetKeyDown(KeyCode.Y))
            {
                newplayer.IncScore(npcNum,CurrentGroup - 1, 0.2);
                CurrentItem++;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                CurrentItem = 0;
                CurrentGroup = 0;
                playerResponse.text = "";
            }
            else
            {
                Debug.Log("Wrong Input");
                Debug.Log(Input.inputString);
            }
        }
        else
        {
            playerResponse.text = "";
            npcDialogueBox.text = "Do You want to know more about me? (Press Y for yes and N for No)";
            if (Input.GetKeyDown(KeyCode.N))
            {
                newplayer.IncScore(npcNum,CurrentGroup - 1, -0.2);
                EndDialogue();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                newplayer.IncScore(npcNum,CurrentGroup - 1, -0.2);
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
        PrintResult();
    }

    void OnApplicationQuit()
    {
        //when the game is restarted, reset the values of the npc
        npc.isDoneTalking = false;
        npc.score = 0;
    }


    void CreateDialogue()
    {
        for (int i = 0; i < npc.npcDialogue.Length; i++)
        {
            npc.npcDialogue[i].InnerString.Clear();
        }

        npc.npcDialogue[0].InnerString.Add("Anything you want to know from me?\n\n");
        
        npc.npcDialogue[0].InnerString.Add("My Educational Background(Press 1)\n");
        branchfinished.Add(false);

        npc.npcDialogue[0].InnerString.Add("My Family(Press 2)\n");
        branchfinished.Add(false);

        npc.npcDialogue[0].InnerString.Add("Job Specific(Press 3)\n");
        branchfinished.Add(false);

        npc.npcDialogue[0].InnerString.Add("The Culture(Press 4)\n");
        branchfinished.Add(false);


        using (var reader = new StreamReader("data/input2.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                var newvalue = values[npcNum];
                if (values[0][0] == 'A')
                {
                    npc.npcDialogue[1].InnerString.Add(newvalue);
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
    }


    void PrintResult()
    {
        if(newplayer.Totalscore > 0)
        {
            char initialchar = 'A';
            using (var writer = new StreamWriter("data/output.csv"))
            {
                writer.WriteLine(newplayer.playername);
                writer.WriteLine(newplayer.Totalscore);
                for (int i = 0; i < newplayer.branchscores.Count; i++)
                {
                	string line=(char)(initialchar + i)+",";
                	for(int j=0; j<numNPCs;j++){
                		line=line+ newplayer.branchscores[j][i];
                		if(j+1<=numNPCs){
                			line=line+",";
                		}
                	}

                    writer.WriteLine(line);
                }
            }
        }
    }


} //end class
