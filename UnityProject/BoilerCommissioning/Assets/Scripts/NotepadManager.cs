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
        m_intruduction["Stage_Stage_Operation"] = new NotepadData("Water fall operation steps", "Please follow the script...");
        m_intruduction["Bleach"] = new NotepadData("Bleach", "Easy to catch fire, put it away from boiler!\n<color=red>!Danger!</color>");
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
