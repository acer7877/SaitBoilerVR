using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoilerLogic;

public class OpenWorldMgr : MonoBehaviour
{
    private OpenWorldMgr() { }
    public static OpenWorldMgr instance;
    private void Awake()
    {
        instance = this;
        //waterAnimations = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject waterAmModel;
    List<GameObject> waterAnimations;
    // Update is called once per frame
    float timer;
    public float OpenWorldFrame = 0.5f;
    void Update()
    {
        if (StageManager.instance.CurrentStage != StageManager.EnumStage.OpenWorld)
            return;

        //animations for water move
        if (waterAnimations != null)
        {
            foreach (GameObject WA in waterAnimations)
            {
                if (WA.GetComponent<MoveObject>().isArrived)
                {
                    waterAnimations.Remove(WA);
                    Destroy(WA);
                    return;//one loop only destroy one
                }
            }
        }

        timer += Time.deltaTime;
        if (timer < OpenWorldFrame)
            return;
        timer -= OpenWorldFrame;
        
        ml.Update(10);
        Helper.DrawHasWater(ml);

    }

    public Material DefaultMat;
    public MainLoop ml;
    public void InitOpenWrold()
    {
        ml = new MainLoop();
        Helper.InitModel(ml, DefaultMat);
    }

    public void OperateValve(string GOname, bool isOn)
    {
        Helper.OperateValve(ml, GOname, isOn);
    }

    public GameObject addWaterAnimation(Vector3 initPos)
    {
        if (waterAnimations == null)
            waterAnimations = new List<GameObject>();
        GameObject WA = Instantiate(waterAmModel, initPos, Quaternion.identity);
        waterAnimations.Add(WA);
        return WA;
    }
}
