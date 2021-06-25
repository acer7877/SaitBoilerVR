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
        m_intruduction["Stage_Introduction"] = new NotepadData("Component Information", "Point at an object to pull up its decription on the Clipboard.");
        m_intruduction["Stage_Operation"] = new NotepadData("1) Pre-Startup Procedure (1/2)", "Put away flammable things\n<color=yellow>?</color> Bleach\n<color=yellow>?</color> can\n<color=yellow>?</color> whatever");
        m_intruduction["Stage_Operation_Finished"] = new NotepadData("Commissioning Steps Complete!", "Great work! You have successfully commissioned the boiler!<sprite=0>");

        m_intruduction["Cabinet"] = new NotepadData("Storage Cabinet", "This is a great place to safely store flammable and combustible contents.");

        m_intruduction["Bleach"] = new NotepadData("Bleach", "<color=red>Danger!</color>\n\nVolatile solvents such as bleach are corrosive and toxic. Mishandling of these chemicals can release harmful and sometimes lethal gases.\n\nProper storage procedures must be adhered to.");
        m_intruduction["AerosolCan (1)"] = new NotepadData("Aerosol Can", "<color=red>Danger!</color>\n\nAerosols contain potentially dangerous substances stored under pressure and are capable of exploding if in the presence of high temperatures.\n\nProper storage procedures must be adhered to.");
        m_intruduction["PaintCan"] = new NotepadData("Paint Can", "<color=red>Danger!</color>\n\nPaints are considered flammable and combustible materials.\n\nProper storage procedures must be adhered to.");

        m_intruduction["CO-Detector"] = new NotepadData("CO Detector", "Carbon monoxide (CO) is a colorless, odorless, and tasteless gas. If the detector's <b>green light</b> is on, the work area is deemed <u>safe</u>. If the CO <u>exceeds the safe range</u>, the light will turn <b>red</b> and the detector will sound an alarm.");
        m_intruduction["BallValveBody1"] = new NotepadData("Main Boiler Valve", "The main boiler valve is responsible for...");
        m_intruduction["BypassValve"] = new NotepadData("Bypass Valve", "The main boiler valve is responsible for...");
        m_intruduction["MainBoilerValve1"] = new NotepadData("Boiler Isolation Supply Valve", "Allows the boiler to be isolated for service or during system filling.");
        m_intruduction["MainBoilerValve2"] = new NotepadData("Boiler Isolation Return Valve", "Allows the boiler to be isolated for service or during system filling.");

        m_intruduction["BoilerVent"] = new NotepadData("Boiler Chimney", "Ventilates Harmful products of combustion from the building using \"Natural Draft\". The heat from the burning of fuel carries the harmful gases up through the narrow chimney.");
        m_intruduction["Boiler"] = new NotepadData("Boiler", "Atmospheric, Low mass Boiler with a finned steel water tube heat exchanger.");
        m_intruduction["DiaphragmTank"] = new NotepadData("Diaphragm Expansion Tank", "This trim component is responsible for regulating the  boiler system pressure that has been determined by the Pressure Reducing Valve setting.");
        m_intruduction["HotWaterTank"] = new NotepadData("Indirect Domestic Water Heater (DHW)", "This Domestic water heater is heated by a coil type heat exchanger contained inside the tank that courses hot boiler water through it instead of using a burner or electricity.");

        m_intruduction["IDHWCIValve"] = new NotepadData("Cold Domestic Water Inlet Valve", "This Valve allows fresh domestic water to enter the tank.");
        m_intruduction["CDWFIValve"] = new NotepadData("Supply side DHW zone Isolation valve", "This valve (when used in conjunction with other valves) can be used to isolate either, the heat exchanger coil inside of the DHW tank, or the circulator.");

        m_intruduction["unknown1"] = new NotepadData("Unknown", "Unknown");
        m_intruduction["PressureGauge"] = new NotepadData("Pressure Reducing Valve (PRV)", "The PRV reduces domestic water pressures from the city to the pressure required to adequately elevate the boiler water in the system to it's highest point with an additional 4-6 psi at the top of the system but not higher than the boiler pressure rating, in this case is 30 psi.");
        m_intruduction["AirReleaseValve"] = new NotepadData("Air Separator with Auto Air Vent", "The Air separator or \"Air scoop\" is used to gather condensable and non-condensable gases from the heated boiler water which then get released through the auto air-vent.");
        m_intruduction["DiaphragmValve"] = new NotepadData("Diaphragm Expansion Tank Isolation Valve", "This valve can be used to isolate the expansion tank for removal or servicing");
        m_intruduction["IDHWIValve_Return"] = new NotepadData("DHW Zone, Return side, Isolation Valve", "This valve (when used in conjunction with the DHW Zone, Supply side, Isolation Valve) can be used to isolate the heat exchanger coil inside of the DHW tank.");
        m_intruduction["GaugeIsolationValve"] = new NotepadData("Boiler Feed Station Isolation Valve", "These valves can be used to isolate the PRV for servicing or to shut off fresh water supply to the boiler.");
        m_intruduction["GaugeIsolationValve1"] = new NotepadData("Boiler Feed Station Isolation Valve", "These valves can be used to isolate the PRV for servicing or to shut off fresh water supply to the boiler.");
        m_intruduction["IDHWIValve_Supply"] = new NotepadData("Unknown", "Unknown");

        m_intruduction["IHIValve1"] = new NotepadData("In-floor Return Mixing Isolation Valve", "This valve (when used in conjunction with other valves) can be used to isolate either, the In-floor manifolds,  or the In-floor heating circulator.");
        m_intruduction["IHIValve2"] = new NotepadData("In-floor Return Mixing Isolation Valve", "This valve (when used in conjunction with other valves) can be used to isolate either, the In-floor manifolds,  or the In-floor heating circulator.");
        m_intruduction["IHRIValve"] = new NotepadData("In-floor Heating Return Isolation Valve", "Unknown");
        m_intruduction["IDHWIValve_Supply"] = new NotepadData("Indirect Domestic Hot Water Isolation Valve Supply", "Unknown");


        m_intruduction["Manifold Panel"] = new NotepadData("Manifold", "[?22]");
        m_intruduction["Boiler"] = new NotepadData("Boiler", "Atmospheric, Low mass Boiler with a finned steel water tube heat exchanger.");
        m_intruduction["Exchanger"] = new NotepadData("Heat Exchanger", "[?24]");
        m_intruduction["Vent"] = new NotepadData("Boiler Vent", "[?25]");
        m_intruduction["Tank"] = new NotepadData("Diaphragm Tank", "[?26]");
        //m_intruduction["TestCube"] = new NotepadData("TestCube just for test", "√ <sprite=0> hahaha <color=red>Red</color> default");
    }
    




    //set text to notepad on the left hand
    public void SetNotepadContext(string Title, string Context)
    {
        //ControllerManager.instance.SetLeftHandModel("Notepad");
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
