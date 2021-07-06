using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BChecker
{
    public string targetGameobject;     //Same name as Gameobjects' name.example: valve1
    public string targetName;   //Used to show in the check list
    public eCheckAction targtAction;         //example: on/off
    public bool isFinished;
    public int arg;     //some step need a arguement

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
    public BChecker(string target, string name, eCheckAction action, int arg_input)
    {
        targetGameobject = target;
        targetName = name;
        targtAction = action;
        isFinished = false;
        arg = arg_input;
    }
    public enum eCheckAction
    {
        ECA_Valve_on,
        ECA_Valve_off,
        ECA_Object_put_away,
        ECA_Object_on,
        ECA_Object_off,
        ECA_Indicator_Num,
        ECA_Fix,
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
                ActionString += string.Format("Find and make sure <u>{0}</u> is <b>working</b>.\n(Touch it to test it.  If a <color=green><b>green<b>" +
                    "</color> light turns on, the detector is properly working)", targetName);
                break;
            case eCheckAction.ECA_Object_off:
                ActionString += string.Format("Find and make sure <u>{0}</u> is <b>stop working</b>.", targetName);
                break;
            case eCheckAction.ECA_Indicator_Num:
                ActionString += string.Format("Check <u>{0}</u> and make sure the Number is <b>{1}</b> PSI", targetName, arg);
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
    public delegate void LamdaFun();
    public LamdaFun AfterDone;

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

    public UnityEngine.UI.Button NextStepBtn;

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
        step.description = "Ensure the area around the boiler is clear of hazardous materials and that they are safely stored. (1/2)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("Bleach", BChecker.eCheckAction.ECA_Object_put_away));
        step.checklist.Add(new BChecker("PaintCan", "Paint Can", BChecker.eCheckAction.ECA_Object_put_away));
        step.checklist.Add(new BChecker("AerosolCan (1)", "Aerosol Can", BChecker.eCheckAction.ECA_Object_put_away));
        m_allSteps.Add(step);

        //Pre-startup procedure(2/2)
        //b.Confirm location and function of the CO detector (test button to confirm batteries are present and fresh.  This step is required (maybe an option to replace batteries)) 
        step = new BStep();
        step.title = "Pre-startup Procedure";
        step.description = "Confirm the location and function of the CO detector. (2/2)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("CO-Detector", BChecker.eCheckAction.ECA_Object_on));
        m_allSteps.Add(step);

        //Filling the system with water(1/n)
        
        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Begin priming the central heating system lines. (1/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-4", "Diaphragm Expansion Tank Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-5", "Boiler Feed Station Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-6", "Boiler Feed Station Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-13", "Cold Domestic Inlet Valve (Supply)", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-3", "System Main Bypass Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-23", "Auto Air Vent Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Fill the boiler system. (2/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-1", "Boiler Isolation Supply Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-2", "Boiler Isolation Return Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Fill the Domestic Water Heating Tank. (3/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-7", "DHW Zone, Return side, Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-8", "DHW Zone, Supply side, Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-9", "Indirect DHW Circulator Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Prime the In-floor Heating Manifold. (4/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-18", "In-floor Heating Mixing Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-11", "In-floor Heating Isolation Valve (Supply)", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Prime Infloor heating line 1 and vent the air out of Air vent 3. (5/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-21", "In-floor Heating Return Drain", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-17", "In-floor Heating 1 Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Prime Infloor heating line 2 and vent the air out of Air vent 3. (6/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-16", "In-floor Heating 2 Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Prime Infloor heating line 3 and vent the air out of Air vent 3. (7/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-15", "In-floor Heating 3 Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Prime Infloor heating line 4 and vent the air out of Air vent 3. (8/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-14", "In-floor Heating 4 Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Filling the system with water";
        step.description = "Open BV 12 and let the air escape Air Vent 3. Close BV - 21 and open BV - 10. (9/9)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-10", "In-floor Heating 3-way Mixing Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-12", "In-floor Heating Isolation Valve (Return)", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-21", "In-floor Heating Return Drain", BChecker.eCheckAction.ECA_Valve_off));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Firing the boiler system.";
        step.description = "Repair any leaking pipes using the appropriate tool. (1/4)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("BV-10", "In-floor Heating 3-way Mixing Isolation Valve", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Firing the boiler system.";
        step.description = "Boiler to Exchanger water heating. (2/4)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("AquaStat", "Locate the AquaStat.", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("AquaStat", "Set the tempurature to 140°F.", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("BV-20", "Open the Gas Valve.", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Firing the boiler system.";
        step.description = "In-floor heating startup. (3/4)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("AquaStat", "Set tempurature on thermostat to 140°F to activate In-floor Heating Pump 3.\nIn-floor heating will continuously run until the tempurature set has been reached.", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Firing the boiler system.";
        step.description = "Open the cover to the boiler to enure a neutral and stable flame has been initiated. (4/4)\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("AquaStat", "Remove cover to the boiler.", BChecker.eCheckAction.ECA_Valve_on));
        step.checklist.Add(new BChecker("AquaStat", "Observe Flame.", BChecker.eCheckAction.ECA_Valve_on));
        //?step.checklist.Add(new BChecker("DiaphragmValve", BChecker.eCheckAction.ECA_Valve_on));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Test_Aquastat";
        step.description = "this is for testing of new function, and set Temp.Indicator to \"777\"\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("AquaStat", "AquaStat XXX", BChecker.eCheckAction.ECA_Indicator_Num, 50));
        step.AfterDone = () => { GameObject.Find("Temp.Indicator").GetComponent<BTIndicator>().setNum(777); };
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Test_Pressure_Indicator";
        step.description = "this is for testing of new function\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("Indicator", "Presure Indicator", BChecker.eCheckAction.ECA_Indicator_Num, 30));
        m_allSteps.Add(step);

        step = new BStep();
        step.title = "Test_indicator";
        step.description = "this is for testing of new function\n";
        step.checklist = new List<BChecker>();
        step.checklist.Add(new BChecker("Indicator (2)", "Idicator XXX", BChecker.eCheckAction.ECA_Indicator_Num, 1000));
        m_allSteps.Add(step);
    }

    public void StartStepFromBeginning()
    {
        m_currentStep = 0;
        m_allSteps[0].PrintToNotpad();
        NextStepBtn.gameObject.SetActive(true);
        NextStepBtn.enabled = false;
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
                if (m_currentStep +1 == m_allSteps.Count) //all steps are finished
                {
                    NotepadManager.instance.SetNotepadContext("Stage_Operation_Finished");
                    sound = "finish";
                }
                else
                {
                    m_allSteps[m_currentStep].PrintToNotpad();
                    sound = "step";
                    NextStepBtn.enabled = true;

                    if (m_allSteps[m_currentStep].AfterDone != null)
                        m_allSteps[m_currentStep].AfterDone();
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
    public BChecker GetCheckerByNameFromCurStep(string name)
    {
        if (m_currentStep >= m_allSteps.Count)
            return null;
        BStep currentStep = m_allSteps[m_currentStep];
        foreach (BChecker bc in currentStep.checklist)
        {
            if (bc.targetGameobject == name)
            {
                if (!bc.isFinished)
                    return bc;
                else
                    return null;
            }
        }
        return null;
    }

    public void nextStep()
    {
        m_currentStep += 1;
        m_allSteps[m_currentStep].PrintToNotpad();
        NextStepBtn.enabled = false;
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
