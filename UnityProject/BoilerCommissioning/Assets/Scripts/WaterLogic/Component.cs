using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerLogic
{
    public class Component
    {
        public string m_name;
        //components connect to me
        public List<string> m_Connected;
        //fluid
        public int m_currentWater, m_maxWater;
        //can water go through?
        public bool m_canGo;
        //description
        public string m_desc;

        public Component(string name, int maxWater = 100)
        {
            m_name = name;
            m_Connected = new List<string>();
            m_currentWater = 0;
            m_maxWater = maxWater;
        }

        public void ShowMe()
        {
            Console.Write(" [{0}]{1}% ", m_name, (int)(m_currentWater * 100 / m_maxWater));
        }

        public virtual void WaterGo(ref int input, string source)
        {
            int orginInput = input;
            int left = input - (m_maxWater - m_currentWater);
            if (left >= 0)
            {
                m_currentWater = m_maxWater;
                input = left;
            }
            else
            {
                m_currentWater += input;
                input = 0;
            }

            if (orginInput != input && source != "GOD")
                Helper.DrawLine(source, m_name);
        }

        public virtual Component GetNext4WaterGo(string source)
        {
            return null;
        }

        public virtual String GetNameAsChecked(string source)
        { return m_name; }
        //public abstract OperateMe();
    }


    class Pipe : Component
    {
        public Pipe(string name, int maxWater = 100) : base(name, maxWater)
        {
            m_canGo = true;
        }
    }

    class Joint : Component
    {
        public Joint(string name, int maxWater = 100) : base(name, maxWater)
        {
            m_canGo = true;
        }
    }

    class Valve : Component
    {
        public Valve(string name, int maxWater = 10) : base(name, maxWater)
        {
            m_canGo = false;
            m_isOn = false;
        }

        //actully same as m_canGo
        public bool m_isOn;

        public void OperateMe()
        {
            m_isOn = !m_isOn;
            m_canGo = m_isOn;
        }

        public void OperateMe(bool isOn)
        {
            m_isOn = isOn;
            m_canGo = m_isOn;
        }
    }

    class Tank : Component
    {
        public Tank(string name, int maxWater = 500) : base(name, maxWater)
        {
            m_canGo = true;
        }
    }

    class Exchanger : Component   //two group of pipes
    {
        Pipe virtualPipe1, virtualPipe2;
        public Exchanger(string name, string p1_1, string p1_2, string p2_1, string p2_2, int maxWater = 100) : base(name, maxWater)
        {
            m_Connected = null;
            virtualPipe1 = new Pipe(name + "1");
            virtualPipe1.m_Connected.Add(p1_1);
            virtualPipe1.m_Connected.Add(p1_2);

            virtualPipe2 = new Pipe(name + "2");
            virtualPipe2.m_Connected.Add(p2_1);
            virtualPipe2.m_Connected.Add(p2_2);

            m_canGo = true;
        }

        public override void WaterGo(ref int input, string source)
        {
            if (virtualPipe1.m_Connected.Contains(source))
            {
                virtualPipe1.WaterGo(ref input, source);
            }
            else if (virtualPipe2.m_Connected.Contains(source))
            {
                virtualPipe2.WaterGo(ref input, source);
            }
            else
                throw (new Exception("Bad source for" + source));

            m_maxWater = virtualPipe1.m_maxWater + virtualPipe2.m_maxWater;
            m_currentWater = virtualPipe1.m_currentWater + virtualPipe2.m_currentWater;
        }

        public override Component GetNext4WaterGo(string source)
        {
            if (virtualPipe1.m_Connected.Contains(source))
            {
                return virtualPipe1;
            }
            else if (virtualPipe2.m_Connected.Contains(source))
            {
                return virtualPipe2;
            }
            else
                throw (new Exception("Bad source for" + source));
        }

        public override string GetNameAsChecked(string source)
        {
            if (virtualPipe1.m_Connected.Contains(source))
            {
                return m_name+"1";
            }
            else if (virtualPipe2.m_Connected.Contains(source))
            {
                return m_name+"2";
            }
            else
                throw (new Exception("Bad source for" + source));
        }
    }

    class Boiler : Component
    {
        public Boiler(string name, int maxWater = 500) : base(name, maxWater)
        {
            m_canGo = true;
            m_isOn = false;
        }

        public bool m_isOn;
        public void OperateMe()
        {
            m_isOn = !m_isOn;
        }

    }
}
