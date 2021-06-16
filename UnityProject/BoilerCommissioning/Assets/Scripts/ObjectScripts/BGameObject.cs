using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

//use/touch function for the interactable objects
public class BGameObject : MonoBehaviour
{
    [Tooltip("We will set the VRTK_InteractableObject from same Gameobject into this if leave it NULL")]
    public VRTK_InteractableObject VRTKIO;
    private void Awake()
    {
        //ObjectManager.instance.RegistGameObject(this.name, this.GetComponentInParent<GameObject>());

    }

    protected virtual void OnEnable()
    {
        VRTKIO = (VRTKIO == null ? GetComponent<VRTK_InteractableObject>() : VRTKIO);
        if (VRTKIO == null)
            return;
        VRTKIO.InteractableObjectTouched += BObjectUsed;
        VRTKIO.InteractableObjectUntouched += BObjectUnused;
    }

    protected virtual void OnDisable()
    {
        if (VRTKIO == null)
            return;
        VRTKIO.InteractableObjectTouched -= BObjectUsed;
        VRTKIO.InteractableObjectUntouched -= BObjectUnused;

    }

    protected virtual void BObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        //In intuducing mode, just show the intudaction onto the notepad
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Intrudce)
            NotepadManager.instance.SetNotepadContext(this.name);

        //In Operating and Openword mode
        //if(StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)

    }

    protected virtual void BObjectUnused(object sender, InteractableObjectEventArgs e)
    {
    }

}
