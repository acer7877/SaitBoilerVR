using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VRTK;

//use/touch function for the interactable objects
public class BGameObject : MonoBehaviour
{
   
    protected VRTK_InteractableObject VRTKIO;
    [Tooltip("Outline highlight will draw new mesh out of the base mesh. This is the distance between base and new-drawing mesh. The bigger for the distance, the thicker the outline is.")]
    public float HighlightThickness = 0.5f;
    protected virtual void Awake()
    {
        //ObjectManager.instance.RegistGameObject(this.name, this.GetComponentInParent<GameObject>());

        VRTKIO = BAddComponent<VRTK_InteractableObject>(gameObject);

        VRTK_InteractObjectHighlighter highlighter = BAddComponent<VRTK_InteractObjectHighlighter>(gameObject);
        highlighter.touchHighlight = Color.green;
        highlighter.touchHighlight = Color.red;

        VRTK.Highlighters.BOutlineHightlight outlineDrawer = BAddComponent<VRTK.Highlighters.BOutlineHightlight>(gameObject);
        outlineDrawer.thickness = HighlightThickness;
        outlineDrawer.enableSubmeshHighlight = true;
    }

    protected virtual void OnEnable()
    {
        VRTKIO.InteractableObjectTouched += BObjectTouch;
        VRTKIO.InteractableObjectUntouched += BObjectUnTouch;
    }

    protected virtual void OnDisable()
    {
        if (VRTKIO == null)
            return;
        VRTKIO.InteractableObjectTouched -= BObjectTouch;
        VRTKIO.InteractableObjectUntouched -= BObjectUnTouch;

    }

    protected virtual void BObjectTouch(object sender, InteractableObjectEventArgs e)
    {
        //In intuducing mode, just show the intudaction onto the notepad
        if (StageManager.instance.CurrentStage == StageManager.EnumStage.Intrudce)
            NotepadManager.instance.SetNotepadContext(this.name);

        //In Operating and Openword mode
        //if(StageManager.instance.CurrentStage == StageManager.EnumStage.Operatie)

    }

    protected virtual void BObjectUnTouch(object sender, InteractableObjectEventArgs e)
    {
    }

    //public static T BAddComponent_<T>(GameObject GO) where T : Component
    //{
    //    T t = GO.AddComponent<T>();
        
        
    //    SerializedObject so = new SerializedObject(t);
    //    so.Update();
    //    return t;
    //}

    public static T BAddComponent<T>(GameObject GO) where T : Component
    {
        T t = GO.AddComponent<T>();
        return t;
    }
}
