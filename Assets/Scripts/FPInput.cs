using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Gamekit3D;


public class FPInput : MonoBehaviour
{
    public static FPInput Instance
    {
        get { return s_Instance; }
    }

    protected static FPInput s_Instance;

    [HideInInspector]
    public bool playerControllerInputBlocked;

    protected bool m_Pause;
    protected bool m_ExternalInputBlocked;

    public double Totalscore = 0;
    public List<List<double>> branchscores=new List<List<double>>();
    public string playername;
 


    public bool Pause
    {
        get { return m_Pause; }
    }

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one FPInput script.  The instances are " + s_Instance.name + " and " + name + ".");

        LoadName();
        
    
    
    }


    void Update()
    {
        m_Pause = Input.GetButtonDown ("Pause");
        if(m_Pause) Debug.Log("Pause");
    }

    public bool HaveControl()
    {
        return !m_ExternalInputBlocked;
    }

    public void ReleaseControl()
    {
        m_ExternalInputBlocked = true;
    }

    public void GainControl()
    {
        m_ExternalInputBlocked = false;
    }

    public void InitializeScore(int NPCs, int size)
    {
        Totalscore = 0;
        Debug.Log("NPCs"+NPCs);
        for(int j=0;j<3;j++){
        List<double> branch=new List<double>();
        branchscores.Add(branch);}
        for(int j=0;j<3;j++){
        for (int i = 0; i < size; i++)
        {
            branchscores[j].Add(0);
        }
       
    }
    }

    void LoadName()
    {
        using (var reader = new StreamReader("data/data.csv"))
        {
            playername = reader.ReadLine();
        }
    }



    public void IncScore(int npc, int index, double addscore)
    {
        Totalscore = Totalscore + addscore;
        branchscores[npc-1][index] = branchscores[npc-1][index] + addscore;
    }
}
