namespace VRTK.Highlighters
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using VRTK;

    public class BOutlineHightlight : VRTK_OutlineObjectCopyHighlighter
    {
        /// The Initialise method sets up the highlighter for use.
        /// Rewriting with new shader.
        public override void Initialise(Color? color = null, GameObject affectObject = null, Dictionary<string, object> options = null)
        {
            objectToAffect = (affectObject != null ? affectObject : gameObject);
            usesClonedObject = true;

            if (stencilOutline == null)
            {
                stencilOutline = Instantiate((Material)Resources.Load("BOutline"));
            }
            SetOptions(options);
            ResetHighlighter();
        }

        /// The Highlight method initiates the outline object to be enabled and display the outline colour.
        /// Rewriting with argument for new shader.
        public override void Highlight(Color? color, float duration = 0f)
        {
            if (highlightModels != null && highlightModels.Length > 0 && stencilOutline != null)
            {
                stencilOutline.SetFloat("_Thickness", thickness);
                stencilOutline.SetColor("_OutlineColor", (Color)color);

                for (int i = 0; i < highlightModels.Length; i++)
                {
                    if (highlightModels[i] != null)
                    {
                        highlightModels[i].gameObject.SetActive(true);
                        highlightModels[i].material = stencilOutline;
                    }
                }
            }
        }
    }
}
