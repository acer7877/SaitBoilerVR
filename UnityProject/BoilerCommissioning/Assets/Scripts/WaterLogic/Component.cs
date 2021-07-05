using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerLogic
{
    public class BWater
    {
        public int m_amount;
        public int m_tmp;

        public BWater(int a, int t)
        {
            m_amount = a;
            m_tmp = t;
        }

        public BWater Copy()
        {
            return new BWater(m_amount, m_tmp);
        }

        public BWater Mix(BWater otherOne)
        {
            int allAmount = m_amount + otherOne.m_amount;
            if (allAmount == 0)
                return null;
            int allTem = (m_tmp * m_amount + otherOne.m_tmp * otherOne.m_amount) / allAmount;
            return new BWater(allAmount, allTem);
        }
    }
    public class Component
    {
        public string m_name;
        //components connect to me
        public List<string> m_Connected;
        //fluid
        public int m_maxWater;
        public BWater m_currentWater;
        //can water go through?
        public bool m_canGo;
        //description
        public string m_desc;
        //current water temperature
        public int m_tmp;

        public Component(string name, int maxWater = 100)
        {
            m_name = name;
            m_Connected = new List<string>();
            m_currentWater = new BWater(0, 0);
            m_maxWater = maxWater;
        }

        public void ShowMe()
        {
            Console.Write(" [{0}]{1}% ", m_name, (int)(m_currentWater.m_amount * 100 / m_maxWater));
        }

        public virtual void WaterGo(ref BWater input, string source)
        {
            BWater orginInput = input.Copy();
            BWater all = input.Mix(m_currentWater);
            int left = all.m_amount - m_maxWater;
            if (left >= 0)
            {
                m_currentWater.m_amount = m_maxWater;
                m_currentWater.m_tmp = all.m_tmp;
                input.m_amount = left;
                input.m_tmp = all.m_tmp;
            }
            else
            {
                m_currentWater.m_amount = all.m_amount;
                m_currentWater.m_tmp = all.m_tmp;
                input.m_amount = 0;
            }

            if (orginInput.m_amount != input.m_amount && source != "GOD")
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
        public Joint(string name, int maxWater = 20) : base(name, maxWater)
        {
            m_canGo = true;
        }
    }

    class Valve : Component
    {
        public Valve(string name, int maxWater = 20) : base(name, maxWater)
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

        public override void WaterGo(ref BWater input, string source)
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
            BWater ExchangerNewWater = virtualPipe1.m_currentWater.Copy();
            m_currentWater = ExchangerNewWater.Mix(virtualPipe2.m_currentWater);
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
            //m_isOn = true;
        }

        public bool m_isOn;
        public void OperateMe()
        {
            m_isOn = !m_isOn;
        }

        public override void WaterGo(ref BWater input, string source)
        {
            if (m_isOn)
                m_currentWater.m_tmp = 90;
            base.WaterGo(ref input, source);

        }

    }
}
