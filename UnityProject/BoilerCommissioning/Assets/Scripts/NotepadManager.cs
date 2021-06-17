using System.Collections;
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
        m_intruduction["Stage_Intuduction"] = new NotepadData("What is this?", "Point Anything with right hand to see the indtrduction.");
        m_intruduction["Stage_Operation"] = new NotepadData("Pre-startup (1/2)", "Put away flammable things\n<color=yellow>?</color> Bleach\n<color=yellow>?</color> can\n<color=yellow>?</color> whatever");
        m_intruduction["Stage_Operation_Finished"] = new NotepadData("Finished!", "Good job! You finished all the steps!<sprite=0><sprite=0><sprite=0><sprite=0>");


        m_intruduction["Cabinet"] = new NotepadData("Cabinet", "Everything you put inside will disappear! Best place for flamable stuffs.");

        m_intruduction["Bleach"] = new NotepadData("Bleach", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");
        m_intruduction["AerosolCan"] = new NotepadData("AerosolCan", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");
        m_intruduction["PaintCan"] = new NotepadData("PaintCan", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");

        m_intruduction["CO-Detector"] = new NotepadData("CO-Detector", "Carbon monoxide (CO), a colorless, odorless, and tasteless gas. This detector has a green light on when it's safe to work around. When the CO exceeds the safe range, the light will turn red and the detector will sound an alarm.");
        m_intruduction["BallValveBody1"] = new NotepadData("Ball Valve 1", "This is BallValue 1, it controls balabla....");
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
