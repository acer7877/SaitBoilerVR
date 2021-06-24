using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BChecker
{
    public string targetGameobject;     //Same name as Gameobjects' name.example: valve1
    public string targetName;   //Used to show in the check list
    public eCheckAction targtAction;         //example: on/off
    public bool isFinished;

    public BChecker(string target, eCheckAction action)
    {
        targetGameobject = target;
        targetName = target;
        targtAction = action;
        isFinished = false;
    }
    public BChecker(string target, string name, eCheckAction action)
    {
        targetGameobject = target;
        targetName = name;
        targtAction = action;
        isFinished = false;
    }
    public enum eCheckAction
    {
        ECA_Valve_on,
        ECA_Valve_off,
        ECA_Object_put_away,
        ECA_Object_on,
        ECA_Object_off,
    }

    //make context string for the notepad
    public string PrintContext()
    {
        //?for unfinished and √for finished
        string ActionString = isFinished ? "<color=green>√</color> ": "☐ ";

        switch (targtAction)
        {
            case eCheckAction.ECA_Object_put_away:
                ActionString += string.Format("Put <u>{0}</u> away.", targetName);
                break;
            case eCheckAction.ECA_Valve_on:
                ActionString += string.Format("Find and switch <b>on</b> <u>{0}</u>.", targetName);
                break;
            case eCheckAction.ECA_Valve_off:
                ActionString += string.Format("Find and switch <b>off</b> <u>{0}</u>.", targetName);
                break;
            case eCheckAction.ECA_Object_on:
                ActionString += string.Format("Find and make sure <u>{0}</u> is <b>working</b>.\n(Touch it to test it.  If a green light turns on, the detector is properly working)", targetName);
                break;
            case eCheckAction.ECA_Object_off:
                ActionString += string.Format("Find and make sure <u>{0}</u> is <b>stop working</b>.", targetName);
                break;
            default:
                throw new System.Exception("unknow check action!" + targtAction.ToString());
        }
        ActionString += "\n";

        return ActionString;
    }
}
public class BStep
{
    public string title;
    public string description;
    public List<BChecker> checklist;

    public void PrintToNotpad()
    {
        string context = description + "\n";
        foreach (BChecker c in checklist)
            context += c.PrintContext();
        NotepadManager.instance.SetNotepadContext(title, context);
    }
}

public class StepManager : MonoBehaviour
{
    //this is a sigleton
    private StepManager() { }
    public static StepManager instance;

    private void Awake()
    {
        instance = this;
        initAllSteps();
    }


    //operation steps
    List<BStep> m_allSteps;
    int m_currentStep;
    void initAllSteps()
    {
        m_currentStep = 0;
        m_allSteps = new List<BStep>();
        BStep step;


        //Pre-startup procedure(1/2)
        //a.Observe area around the boiler to ensure there are no combustible materials, gases or other flammable liquids or vapors (paint cans, aerosol containers, solvents) (Removal of correct items is required to move onto the next step) 
        step = new BStep();
        step.title = "Pre-startup Procedure";
        step.description = "Ensure the area around the boiler is clear of hazardous materials and that they are safely stored. (1/2)";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("Bleach", BChecker.eCheckAction.ECA_Object_put_away));
        step.checklist.Add(new BChecker("PaintCan", "Paint Can", BChecker.eCheckAction.ECA_Object_put_away));
        step.checklist.Add(new BChecker("AerosolCan (1)", "Aerosol Can", BChecker.eCheckAction.ECA_Object_put_away));
        m_allSteps.Add(step);

        //Pre-startup procedure(2/2)
        //b.Confirm location and function of the CO detector (test button to confirm batteries are present and fresh.  This step is required (maybe an option to replace batteries)) 
        step = new BStep();
        step.title = "Pre-startup Procedure";
        step.description = "Confirm the location and function of the CO detector. (2/2)";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("CO-Detector", BChecker.eCheckAction.ECA_Object_on));
        m_allSteps.Add(step);

        //Filling the system with water(1/n)
        
        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Confirm the Valves are configured prior to filling the heating system with water. (1/1)";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("DiaphragmValve", "Diaphragm Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("GaugeIsolationValve1", "Gauge Isolation Valve 1", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("MainBoilerValve1", "Main Boiler Valve 1", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);




    }

    public void StartStepFromBeginning()
    {
        m_currentStep = 0;
        m_allSteps[0].PrintToNotpad();
    }
    public void Action(string name, BChecker.eCheckAction type)
    {
        if (m_currentStep >= m_allSteps.Count)
            return;
        BStep currentStep = m_allSteps[m_currentStep];
        bool anythingChaged = false;
        bool isAllFinished = true;
        string sound = null;
        foreach(BChecker c in currentStep.checklist)
        {
            if (c.targetGameobject == name)
            {
                bool isFinished = c.targtAction == type;
                if (c.isFinished != isFinished)
                {
                    c.isFinished = isFinished;
                    anythingChaged = true;
                }
                    
            }

            isAllFinished &= c.isFinished;
        }

        if (anythingChaged)
        {
            if (isAllFinished)//all steps are finished and go next
            {
                m_currentStep += 1;
                if (m_currentStep == m_allSteps.Count) //all steps are finished
                {
                    NotepadManager.instance.SetNotepadContext("Stage_Operation_Finished");
                    sound = "finish";
                }
                else
                {
                    m_allSteps[m_currentStep].PrintToNotpad();
                    sound = "step";
                }
            }
            else//show the current step
            {
                currentStep.PrintToNotpad();
                sound = "check";
            }
        }

        if (sound != null)
            SoundManager.instance.Play(sound);
    }

    //To show bubble at current operatable objects
    List<GameObject> HintList;
    public Transform HintTransfor;
    public void CreateHint()
    {
        if (HintList == null)
            HintList = new List<GameObject>();

        if (m_currentStep >= m_allSteps.Count)
            return;

        BStep currentStep = m_allSteps[m_currentStep];
        foreach (BChecker c in currentStep.checklist)
        {
            if (c.isFinished) 
                continue;

            GameObject Target = GameObject.Find(c.targetGameobject);
            if (Target == null)
                continue;

            HintList.Add(Instantiate(HintTransfor, Target.transform.position, Quaternion.identity).gameObject);
        }
    }
    public void DeleteHint()
    {
        if (HintList == null || HintList.Count == 0)
            return;
        foreach (GameObject o in HintList)
            Destroy(o);

        HintList.Clear();
    }
}
