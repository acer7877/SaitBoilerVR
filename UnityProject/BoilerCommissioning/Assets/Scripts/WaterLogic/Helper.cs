using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoilerLogic
{
    class Helper
    {
        public static void InitModel(MainLoop ml, Material DefaultMat)
        {
            ml.initSystem();

            foreach (string nameString in ml.m_allCom.Keys)
            {
                string[] nameSplited = nameString.Split('+');
                foreach (string nameOne in nameSplited)
                {
                    //Debug.LogError(">>>>>>>>>> " + nameOne);
                    GameObject target = GameObject.Find(nameOne);
                    if (target != null)
                    {
                        //TODO:deal with child
                        Renderer r = target.transform.GetComponent<Renderer>();
                        if (r != null)
                        {
                            r.materials[0].CopyPropertiesFromMaterial(DefaultMat);
                        }

                    }
                    else
                        Debug.LogError("Bad name from OpenWorldMgr[" + nameOne + "]");
                }
            }
        }

        public static void PrintHasWater(MainLoop ml)
        {
            foreach(string n in ml.m_allCom.Keys)
            {
                if (ml.m_allCom[n].m_currentWater > 0)
                    ml.m_allCom[n].ShowMe();
            }
            Console.Write("\n");

        }

        public static void DrawHasWater(MainLoop ml)
        {
            foreach (string nameString in ml.m_allCom.Keys)
            {
                if (ml.m_allCom[nameString].m_currentWater == 0)
                    continue;

                float RG = (1f - ((float)ml.m_allCom[nameString].m_currentWater / (float)ml.m_allCom[nameString].m_maxWater));

                string[] nameSplited = nameString.Split('+');
                foreach (string nameOne in nameSplited)
                {
                    GameObject target = GameObject.Find(nameOne);
                    if (target != null)
                    {
                        //TODO:deal with child
                        MeshRenderer r = target.transform.GetComponent<MeshRenderer>();
                        if (r != null)
                        {
                            Color c = new Color(RG, RG, 1);
                            r.material.color = new Color(RG, RG, 255);
                        }

                    }
                    else
                        Debug.LogError("Bad name from OpenWorldMgr[" + nameOne + "]");
                }
            }

        }

        public static void PrintAllValve(MainLoop ml)
        {
            int i = 0;
            foreach (string n in ml.m_allCom.Keys)
            {
                Valve v = ml.m_allCom[n] as Valve;
                if (v != null)
                {
                    Console.Write("\t{0}\t{2}\t[{1}]\n", i++, v.m_name, v.m_isOn);
                }
            }
            Console.Write("\n");
        }

        public static void OperateValve(MainLoop ml, string targetName, bool isOnArg)
        {
            foreach (string nameString in ml.m_allCom.Keys)
            {
                if (nameString.Contains(targetName))
                {
                    Valve v = ml.m_allCom[nameString] as Valve;
                    if (v != null)
                        v.OperateMe(isOnArg);
                }
            }
        }

        public static void DrawLine(Vector3 from, Vector3 to)
        {
            GameObject newWater = OpenWorldMgr.instance.addWaterAnimation(from);
            newWater.GetComponent<MoveObject>().from = from;
            newWater.GetComponent<MoveObject>().to = to;
            //Debug.LogError(">>>>>>>>>>>" + from.ToString() + "-" + to.ToString());
        }

        public static void DrawLine(string from, string to)
        {
            DrawLine(getPstFromComName(from), getPstFromComName(to));
        }
        static Vector3 getPstFromComName(string comName)
        {
            string targetName = comName;
            if (comName.Contains("Exchanger"))
                targetName = "Exchanger";
            string[] names = targetName.Split('+');
            GameObject go = GameObject.Find(names[0]);
            if (go == null)
                return new Vector3(0, 0, 0);
            return go.GetComponent<Renderer>().bounds.center;
        }

    }

}
