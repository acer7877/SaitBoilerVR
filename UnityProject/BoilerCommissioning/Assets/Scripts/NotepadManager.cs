﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class NotepadData
{
    public NotepadData(string t, string c)
    {
        Title = t;
        Context = c;
    }
    string Title;
    string Context;
    
    public void Display()
    {
        NotepadManager.instance.SetNotepadContext(Title, Context);
    }

    //Force lefthand change to Notepad and display
    public void ForceDisplay()
    {
        ControllerManager.instance.SetLeftHandModel("Notepad");
        Display();
    }

}

public class NotepadManager : MonoBehaviour
{
    //this is a sigleton
    private NotepadManager() { }
    public static NotepadManager instance;
    Dictionary<string, NotepadData> m_intruduction;
    //Dictionary<string, string> m_step;

    private void Awake()
    {
        instance = this;
        initItruduction();
    }



    private void initItruduction()
    {
        if (m_intruduction == null)
            m_intruduction = new Dictionary<string, NotepadData>();

        m_intruduction["TestCube"] = new NotepadData("TestCube just for test", "√ <sprite=0> hahaha <color=red>Red</color> default");
        m_intruduction["Stage_Welcome"] = new NotepadData("Welcome", "<sprite=0> Welcome to Boiler VR Experience");
        m_intruduction["Stage_Introduction"] = new NotepadData("What is this?", "Point at anything with right hand to see the indtrduction.");
        m_intruduction["Stage_Operation"] = new NotepadData("Pre-startup (1/2)", "Put away flammable things\n<color=yellow>?</color> Bleach\n<color=yellow>?</color> can\n<color=yellow>?</color> whatever");
        m_intruduction["Stage_Operation_Finished"] = new NotepadData("Finished!", "Good job! You finished all the steps!<sprite=0><sprite=0><sprite=0><sprite=0>");


        m_intruduction["Cabinet"] = new NotepadData("Safety Cabinet", "This is a safety cabinet used to safely store flammable and combustible contents.");

        m_intruduction["Bleach"] = new NotepadData("Bleach", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");
        m_intruduction["AerosolCan"] = new NotepadData("Aerosol Can", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");
        m_intruduction["PaintCan"] = new NotepadData("Paint Can", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");

        m_intruduction["CO-Detector"] = new NotepadData("CO-Detector", "Carbon monoxide (CO) is a colorless, odorless, and tasteless gas. This detector has a <b>green light</b> on when it's <u>safe</u> to work around. When the CO <u>exceeds the safe range</u>, the light will turn <b>red</b> and the detector will sound an alarm.");
        m_intruduction["BallValveBody1"] = new NotepadData("Ball Valve 1", "This is BallValue 1, it controls balabla....");
        m_intruduction["BypassValve"] = new NotepadData("Bypass Valve", "[?1]");
        m_intruduction["MainBoilerValve1"] = new NotepadData("Main Boiler Valve1", "[?2]");
        m_intruduction["MainBoilerValve2"] = new NotepadData("Main Boiler Valve2", "[?3]");

        m_intruduction["BoilerVent"] = new NotepadData("Boiler Vent", "[?4]");
        m_intruduction["Boiler"] = new NotepadData("Boiler", "[?5]");
        m_intruduction["DiaphragmTank"] = new NotepadData("Diaphragm Tank", "[?6]");
        m_intruduction["HotWaterTank"] = new NotepadData("Hot Water Tank", "[?7]");

        m_intruduction["IDHWCIValve"] = new NotepadData("Iddirect Domestic Hot Water Circulator Isolation Valve", "[?8]");
        m_intruduction["CDWFIValve"] = new NotepadData("Cold Domestic Water Feed Isolation Valve", "[?9]");

        m_intruduction["unknown1"] = new NotepadData("[?10]", "[?11]");
        m_intruduction["PressureGauge"] = new NotepadData("Pressure Gauge", "[?12]");
        m_intruduction["AirReleaseValve"] = new NotepadData("Air Release Valve", "[?13]");
        m_intruduction["DiaphragmValve"] = new NotepadData("Diaphragm Valve", "[?14]");
        m_intruduction["IDHWIValve_Return"] = new NotepadData("Indirect Domestic Hot Water Isolation Valve Return", "[?15]");
        m_intruduction["GaugeIsolationValve"] = new NotepadData("Gauge Isolation Valves", "[?16]");
        m_intruduction["IDHWIValve_Supply"] = new NotepadData("Indirect Domestic Hot Water Isolation Valve Supply", "[?17]");

        m_intruduction["IHIValve1"] = new NotepadData("In-floor Heating Isolation Valve1", "[?18]");
        m_intruduction["IHIValve2"] = new NotepadData("In-floor Heating Isolation Valve2", "[?19]");
        m_intruduction["IHRIValve"] = new NotepadData("In-floor Heating Return Isolation Valve", "[?20]");
        m_intruduction["IDHWIValve_Supply"] = new NotepadData("Indirect Domestic Hot Water Isolation Valve Supply", "[?21]");
        m_intruduction["Manifold"] = new NotepadData("Manifold", "[?22]");
        //m_intruduction["TestCube"] = new NotepadData("TestCube just for test", "√ <sprite=0> hahaha <color=red>Red</color> default");
    }
    




    //set text to notepad on the left hand
    public void SetNotepadContext(string Title, string Context)
    {
        GameObject NP_Title = GameObject.Find("Title_np");
        NP_Title.GetComponent<TMP_Text>().text = Title;

        GameObject NP_Context = GameObject.Find("Context_np");
        NP_Context.GetComponent<TMP_Text>().text = Context;

    }
    public void SetNotepadContext(string _index)
    {
        m_intruduction[_index].Display();
    }
}
