using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerLogic
{
    public class MainLoop
    {
        public Dictionary<string, Component> m_allCom;
        public void initSystem()
        {
            m_allCom = new Dictionary<string, Component>();

            Component n;
            n = new Pipe("Pipe 28.003", 101);
            n.m_Connected.Add("Tee.008");
            n.m_desc = "water incoming pipe";
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.008");
            n.m_Connected.Add("Pipe 28.003");
            n.m_Connected.Add("Pipe 28.002+Elbow.019");
            n.m_Connected.Add("Pipe 28.001+Elbow.020");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 28.002+Elbow.019");
            n.m_Connected.Add("Tee.008");
            n.m_Connected.Add("BV-13+Pipe 29.001");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-13+Pipe 29.001");
            n.m_Connected.Add("Pipe 28.002+Elbow.019");
            n.m_Connected.Add("Exchanger");
            n.m_desc = "valve into the exchanger";
            m_allCom.Add(n.m_name, n);

            n = new Exchanger("Exchanger",
                "BV-13+Pipe 29.001", "Pipe 29.002+Elbow.002",
                "Elbow.016+Pipe 24.001", "Pipe 24.002");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 29.002+Elbow.002");
            n.m_Connected.Add("Exchanger");
            n.m_Connected.Add("BV-19+Pipe 28.004");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-19+Pipe 28.004");
            n.m_Connected.Add("Pipe 29.002+Elbow.002");
            n.m_Connected.Add("Elbow.023+Pipe 29.003+Elbow.022+Pipe 29.004+Elbow.024+Pipe 28.005");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.023+Pipe 29.003+Elbow.022+Pipe 29.004+Elbow.024+Pipe 28.005");
            n.m_Connected.Add("BV-19+Pipe 28.004");
            n.m_desc = "pipe out for the house-using";
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 28.001+Elbow.020");
            n.m_Connected.Add("Tee.008");
            n.m_Connected.Add("BV-6");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-6");
            n.m_Connected.Add("Pipe 28.001+Elbow.020");
            n.m_Connected.Add("BV-5+Pipe 29");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-5+Pipe 29");
            n.m_Connected.Add("BV-6");
            n.m_Connected.Add("Elbow.013+Pipe 28+Elbow.014+Pipe 27");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.013+Pipe 28+Elbow.014+Pipe 27");
            n.m_Connected.Add("BV-5+Pipe 29");
            n.m_Connected.Add("Tee.007+Tank Pipe.003");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.007+Tank Pipe.003");
            n.m_Connected.Add("Elbow.013+Pipe 28+Elbow.014+Pipe 27");
            n.m_Connected.Add("Elbow.025+Tank Pipe.002+Elbow.026");
            n.m_Connected.Add("Air Seperator+Main Pipe Delete Pump");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.025+Tank Pipe.002+Elbow.026");
            n.m_Connected.Add("Tee.007+Tank Pipe.003");
            n.m_Connected.Add("BV-4+Tank Inlet Pipe");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-4+Tank Inlet Pipe");
            n.m_Connected.Add("Elbow.025+Tank Pipe.002+Elbow.026");
            n.m_Connected.Add("Tank");
            n.m_desc = "valve into the tank";
            m_allCom.Add(n.m_name, n);

            n = new Tank("Tank");
            n.m_Connected.Add("BV-4+Tank Inlet Pipe");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Air Seperator+Main Pipe Delete Pump");
            n.m_Connected.Add("Tee.007+Tank Pipe.003");
            n.m_Connected.Add("Elbow.009+Pipe 16+Elbow.008");
            n.m_Connected.Add("Tee.006");
            n.m_desc = "deal as a 3 joint, ignore 'Air Vent -1'";
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.009+Pipe 16+Elbow.008");
            n.m_Connected.Add("Air Seperator+Main Pipe Delete Pump");
            n.m_Connected.Add("Tee+Pipe 11");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee+Pipe 11");
            n.m_Connected.Add("Elbow.009+Pipe 16+Elbow.008");
            n.m_Connected.Add("BV-3+Pipe 10");
            n.m_Connected.Add("BV-1+Pipe 8");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-1+Pipe 8");
            n.m_Connected.Add("Tee+Pipe 11");
            n.m_Connected.Add("Elbow.005+Pipe 7+Elbow.007+Switch Valve Pipe+Reducing Valve");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.005+Pipe 7+Elbow.007+Switch Valve Pipe+Reducing Valve");
            n.m_Connected.Add("BV-1+Pipe 8");
            n.m_Connected.Add("Boiler");
            m_allCom.Add(n.m_name, n);

            n = new Boiler("Boiler");
            n.m_Connected.Add("Elbow.005+Pipe 7+Elbow.007+Switch Valve Pipe+Reducing Valve");
            n.m_Connected.Add("Elbow.003+Pipe 3+Elbow+Pipe 6+Elbow.004");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.003+Pipe 3+Elbow+Pipe 6+Elbow.004");
            n.m_Connected.Add("Boiler");
            n.m_Connected.Add("BV-2+Pipe 9");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-2+Pipe 9");
            n.m_Connected.Add("Tee.001");
            n.m_Connected.Add("Elbow.003+Pipe 3+Elbow+Pipe 6+Elbow.004");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-3+Pipe 10");
            n.m_Connected.Add("Tee+Pipe 11");
            n.m_Connected.Add("Tee.001");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.001");
            n.m_Connected.Add("BV-3+Pipe 10");
            n.m_Connected.Add("BV-2+Pipe 9");
            n.m_Connected.Add("Tee.005+Pipe 12");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.005+Pipe 12");
            n.m_Connected.Add("Tee.001");
            n.m_Connected.Add("Pipe 17.001+Elbow.015");
            n.m_Connected.Add("Tee.002");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 17.001+Elbow.015");
            n.m_Connected.Add("BV-7+Pipe 17.002");
            n.m_Connected.Add("Tee.005+Pipe 12");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-7+Pipe 17.002");
            n.m_Connected.Add("Pipe 17.001+Elbow.015");
            n.m_Connected.Add("Elbow.016+Pipe 24.001");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Elbow.016+Pipe 24.001");
            n.m_Connected.Add("BV-7+Pipe 17.002");
            n.m_Connected.Add("Exchanger");
            n.m_desc = "upper wider pipe connected to Exchanger?";
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.002");
            n.m_Connected.Add("Tee.005+Pipe 12");
            n.m_Connected.Add("Pipe 17");
            n.m_Connected.Add("Pipe 13");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 13");
            n.m_Connected.Add("Tee.002");
            n.m_desc = "lower end with GV-2 on the right end";
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 17");
            n.m_Connected.Add("Tee.002");
            n.m_Connected.Add("Tee.003");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.006");
            n.m_Connected.Add("Air Seperator+Main Pipe Delete Pump");
            n.m_Connected.Add("Pipe 17.003+Elbow.017");
            n.m_Connected.Add("Pipe 15.002");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 17.003+Elbow.017");
            n.m_Connected.Add("Tee.006");
            n.m_Connected.Add("BV-8+Pipe 17.004");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-8+Pipe 17.004");
            n.m_Connected.Add("Pipe 17.003+Elbow.017");
            n.m_Connected.Add("BV-9+Pipe 17.005+Elbow.018");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-9+Pipe 17.005+Elbow.018");
            n.m_Connected.Add("BV-8+Pipe 17.004");
            n.m_Connected.Add("Pipe 24.002");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 24.002");
            n.m_Connected.Add("BV-9+Pipe 17.005+Elbow.018");
            n.m_Connected.Add("Exchanger");
            n.m_desc = "lower wider pipe connect to exchanger";
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 15.002");
            n.m_Connected.Add("Tee.006");
            n.m_Connected.Add("Tee.004");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.004");
            n.m_Connected.Add("Pipe 15.002");
            n.m_Connected.Add("Pipe 15");
            n.m_Connected.Add("BV-18+Pipe 18+Elbow.010");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 15");
            n.m_Connected.Add("Tee.004");
            n.m_desc = "upper end with GV-3 on the right end";
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-18+Pipe 18+Elbow.010");
            n.m_Connected.Add("Tee.004");
            n.m_Connected.Add("Pipe 21");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 21");
            n.m_Connected.Add("BV-18+Pipe 18+Elbow.010");
            n.m_Connected.Add("3 way Mixing Valve");
            m_allCom.Add(n.m_name, n);

            n = new Joint("3 way Mixing Valve");
            n.m_Connected.Add("Pipe 21");
            n.m_Connected.Add("BV-10+Pipe 20");
            n.m_Connected.Add("Pipe 22+Elbow.012");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-10+Pipe 20");
            n.m_Connected.Add("3 way Mixing Valve");
            n.m_Connected.Add("Tee.003");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 22+Elbow.012");
            n.m_Connected.Add("3 way Mixing Valve");
            n.m_Connected.Add("BV-11+Pipe 23+Pipe 24");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-11+Pipe 23+Pipe 24");
            n.m_Connected.Add("Pipe 22+Elbow.012");
            n.m_Connected.Add("Manifold Body");
            m_allCom.Add(n.m_name, n);

            n = new Joint("Tee.003");
            n.m_Connected.Add("Pipe 17");
            n.m_Connected.Add("BV-10+Pipe 20");
            n.m_Connected.Add("Pipe 19+Elbow.011");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Pipe 19+Elbow.011");
            n.m_Connected.Add("Tee.003");
            n.m_Connected.Add("BV-12+Pipe 25");
            m_allCom.Add(n.m_name, n);

            n = new Valve("BV-12+Pipe 25");
            n.m_Connected.Add("Pipe 19+Elbow.011");
            n.m_Connected.Add("Manifold Body.001");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Manifold Body");
            n.m_Connected.Add("Manifold Body.001");
            n.m_Connected.Add("BV-11+Pipe 23+Pipe 24");
            m_allCom.Add(n.m_name, n);

            n = new Pipe("Manifold Body.001");
            n.m_Connected.Add("Manifold Body");
            n.m_Connected.Add("BV-12+Pipe 25");
            m_allCom.Add(n.m_name, n);
        }


        public bool Update(int inputWater = 10)
        {
            int WaterLeft = inputWater;
            HashSet<string> checkedNode = new HashSet<string>();
            WaterGo("Pipe 28.003", "GOD", ref checkedNode, ref WaterLeft);
            return WaterLeft == inputWater;
        }

        public void WaterGo(string currentNodeName, string source, ref HashSet<string> checkedNode, ref int WaterLeft)
        {
            Component currentCom = m_allCom[currentNodeName];
            WaterGo(currentCom, source, ref checkedNode, ref WaterLeft);
        }

        public void WaterGo(Component currentCom, string source, ref HashSet<string> checkedNode, ref int WaterLeft)
        {
            //can get through, return
            if (!currentCom.m_canGo)
                return;

            //fill this component
            currentCom.WaterGo(ref WaterLeft, source);
            //not water left, return
            if (WaterLeft == 0)
                return;

            checkedNode.Add(currentCom.m_name);

            if (currentCom.m_Connected == null)//Something like Exchanger, need a function to decide what to update next
            {
                WaterGo(currentCom.GetNext4WaterGo(source), currentCom.m_name, ref checkedNode, ref WaterLeft);
            }
            else
            {
                foreach (string nextNode in currentCom.m_Connected)
                {
                    if (!checkedNode.Contains(nextNode))
                        WaterGo(nextNode, currentCom.m_name, ref checkedNode, ref WaterLeft);
                }
            }
        }
    }

}
