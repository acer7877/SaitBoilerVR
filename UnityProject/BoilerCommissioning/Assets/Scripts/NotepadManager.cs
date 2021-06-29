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

        m_intruduction["Cabinet"] = new NotepadData("Safety Cabinet", "This is a safety cabinet used to safely store flammable and combustible contents.");

        m_intruduction["Bleach"] = new NotepadData("Bleach", "<color=red>Danger!</color>\n\nVolatile solvents such as bleach are corrosive and toxic. Mishandling of these chemicals can release harmful and sometimes lethal gases.\n\nProper storage procedures must be adhered to.");
        m_intruduction["AerosolCan (1)"] = new NotepadData("Aerosol Can", "<color=red>Danger!</color>\n\nAerosols contain potentially dangerous substances stored under pressure and are capable of exploding if in the presence of high temperatures.\n\nProper storage procedures must be adhered to.");
        m_intruduction["PaintCan"] = new NotepadData("Paint Can", "<color=red>Danger!</color>\n\nPaints are considered flammable and combustible materials.\n\nProper storage procedures must be adhered to.");

        m_intruduction["CO-Detector"] = new NotepadData("CO Detector", "Carbon monoxide (CO) is a colorless, odorless, and tasteless gas. If the detector's <color=green><b>green</color> light</b> is on, the work area is deemed <u>safe</u>. If the CO <u>exceeds the safe range</u>, the light will turn <color=red><b>red</b></color> and the detector will sound an alarm.");
        m_intruduction["BV-3"] = new NotepadData("System Main Bypass Isolation Valve", "This valve can be used to isolate the main Supply and Return piping during service.");
        m_intruduction["BV-1"] = new NotepadData("Boiler Isolation Supply Valve", "Allows the boiler to be isolated for service or during system filling.");
        m_intruduction["BV-2"] = new NotepadData("Boiler Isolation Return Valve", "Allows the boiler to be isolated for service or during system filling.");

        m_intruduction["Vent"] = new NotepadData("Boiler Chimney", "Ventilates Harmful products of combustion from the building using \"Natural Draft\". The heat from the burning of fuel carries the harmful gases up through the narrow chimney.");
        m_intruduction["Boiler"] = new NotepadData("Boiler", "Atmospheric, low mass boiler with a finned steel water tube heat exchanger.");
        m_intruduction["Tank"] = new NotepadData("Diaphragm Expansion Tank", "Manages the pressure of the fluid in the heating system. Prevents the compression of air in the event there is mixing within the line.");
        m_intruduction["Exchanger"] = new NotepadData("Indirect Domestic Water Heater (DHW)", "This Domestic water heater is heated by a coil type heat exchanger contained inside the tank that courses hot boiler water through it instead of using a burner or electricity.");

        m_intruduction["BV-13"] = new NotepadData("Cold Domestic Water Inlet Valve (Supply)", "This Valve allows fresh domestic water to enter the tank.");
        m_intruduction["BV-9"] = new NotepadData("Indirect DHW Circulator Isolation Valve", "This valve (when used in conjunction with other valves) can be used to isolate either the heat exchanger coil inside of the DHW tank, or the circulator.");

        m_intruduction["unknown1"] = new NotepadData("Unknown", "Unknown");
        m_intruduction["PRV"] = new NotepadData("Pressure Reducing Valve (PRV)", "The PRV reduces domestic water pressures from the city to the pressure required to adequately elevate the boiler water in the system to it's highest point with an additional 4-6 psi at the top of the system but not higher than the boiler pressure rating, in this case is 30 psi.");
        m_intruduction["BV-23"] = new NotepadData("Air Separator with Auto Air Vent", "The Air separator or \"Air Scoop\" is used to gather condensable and non-condensable gases from the heated boiler water which then get released through the auto air-vent.");
        m_intruduction["BV-4"] = new NotepadData("Diaphragm Expansion Tank Isolation Valve", "This valve can be used to isolate the expansion tank for removal or servicing");
        m_intruduction["BV-7"] = new NotepadData("DHW Zone, Return side, Isolation Valve", "This valve (when used in conjunction with the DHW Zone, Supply side, Isolation Valve) can be used to isolate the heat exchanger coil inside of the DHW tank, or the circulator.");
        m_intruduction["BV-5"] = new NotepadData("Boiler Feed Station Isolation Valve", "These valves can be used to isolate the PRV for servicing or to shut off fresh water supply to the boiler.");
        m_intruduction["BV-6"] = new NotepadData("Boiler Feed Station Isolation Valve", "These valves can be used to isolate the PRV for servicing or to shut off fresh water supply to the boiler.");
        m_intruduction["BV-8"] = new NotepadData("DHW Zone, Supply side, Isolation Valve", "This valve (when used in conjunction with the DHW Zone, Return side, Isolation Valve) can be used to isolate the heat exchanger coil inside of the DHW tank, or the circulator.");

        m_intruduction["BV-10"] = new NotepadData("In-floor Heating 3-way Mixing Isolation Valve", "This valve (when used in conjunction with other valves) can be used to isolate either the In-floor manifolds, or the In-floor heating circulator.");
        m_intruduction["BV-18"] = new NotepadData("In-floor Heating Mixing Isolation Valve", "This valve (when used in conjunction with other valves) can be used to isolate either the In-floor manifolds, or the In-floor heating circulator.");
        m_intruduction["BV-11"] = new NotepadData("In-floor Heating Isolation Valve (Supply)", "This valve controls the flow leading to the Hot Water Supply manifold. This valve is used during servicing to the supply Manifold.");
        m_intruduction["BV-12"] = new NotepadData("In-floor Heating Isolation Valve (Return)", "This valve is used to vent out any air in the In-floor heating system and is closed during the boiler filling process. This prevents the air from returning back to the central boiler system.");

        m_intruduction["BV-14"] = new NotepadData("In-floor Heating 4 Valve","Can be opened individually to fill the in-floor loops.");
        m_intruduction["BV-15"] = new NotepadData("In-floor Heating 3 Valve","Can be opened individually to fill the in-floor loops.");
        m_intruduction["BV-16"] = new NotepadData("In-floor Heating 2 Valve","Can be opened individually to fill the in-floor loops.");
        m_intruduction["BV-17"] = new NotepadData("In-floor Heating 1 Valve","Can be opened individually to fill the in-floor loops.");

        m_intruduction["BV-19"] = new NotepadData("Domestic Hot Water Supply Valve", "Can be shut off to service the indirect domestic hot water heater.");
        m_intruduction["BV-20"] = new NotepadData("Boiler Natural Gas Supply Valve", "Can be turned on anytime prior to firing the boiler.");
        m_intruduction["BV-21"] = new NotepadData("In-floor Heating Return Drain", "Controls the discharge of water during air releif.");
        m_intruduction["BV-22"] = new NotepadData("In-floor Heating Supply Drain", "Controls the discharge of water during air releif.");

        m_intruduction["S Check Valve"] = new NotepadData("Boiler Feed Station Supply Check Valve", "This check valve reduces pressure and prevents the backflow of water.");
        m_intruduction["L Check Valve"] = new NotepadData("Boiler Return Check Valve", "This valve prevents the backflow of hot water.");

        m_intruduction["GV-1"] = new NotepadData("Drain", "This is a drain.");
        m_intruduction["GV-2"] = new NotepadData("Future Take-off Return", "Future expansion return zone.");
        m_intruduction["GV-3"] = new NotepadData("Future Take-off Supply", "Future expansion supply zone.");

        m_intruduction["PSV"] = new NotepadData("Indirect DHW Safetly Relief Valve", "Reliefs the pressure in the Indirect DHW Heater when the pressure exceeds preset system limits.");
        m_intruduction["PSV.001"] = new NotepadData("Boiler Safetly Relief Valve", "Reliefs the pressure in the Boiler when the pressure exceeds preset safety limits.");

        m_intruduction["Air Vent -1"] = new NotepadData("Diaphragm Expansion Tank Automatic Air Vent", "Vents air out of the system during the boiler filling process.");
        //m_intruduction["Air Vent -1"] = new NotepadData("Boiler Automatic Air Vent", "---");//
        m_intruduction["Air Vent - 3"] = new NotepadData("In-floor Heating Return Automatic Air Vent", "Vents displaced air isolated at the manifold during the boiler filling process.");
        m_intruduction["Air Vent - 4"] = new NotepadData("In-floor Heating Supply Automatic Air Vent", "Vents displaced air isolated at the manifold during the boiler filling process.");

        m_intruduction["Manifold Panel"] = new NotepadData("Manifold Panel", "Wall-mounted panel used to organize the supply and return lines from the central system.");
        m_intruduction["Boiler"] = new NotepadData("Boiler", "The central heating system that acts as a furnace to continuously supply hot water to the heating system and the heat exchanger.");
        m_intruduction["Exchanger"] = new NotepadData("Heat Exchanger", "This uses the heat generated from the boiler system to \"exchange\" heat to be circulated through the heating system.");
        m_intruduction["Vent"] = new NotepadData("Boiler Vent", "Facilitates the ventilation of combustion gases.");
        m_intruduction["Tank"] = new NotepadData("Diaphragm Expansion Tank", "Manages the pressure of the fluid in the heating system. Prevents the compression of air in the event there is mixing within the line.");
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
