using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VRCButton : MonoBehaviour, InputManager.IClickable3D
{
    public string ButtonID;
    bool hovering;
    (Material, Color)[] modelMaterials;
    Color highlightedColor = Color.white;

    public virtual void Click(InputManager.InputRaycastResult clickInfo)
    {
        Debug.Log($"ButtonID {ButtonID} clicked!");
    }

    public virtual void Hover(InputManager.InputRaycastResult hoverInfo)
    {
        hovering = true;
    }

    public virtual void Start()
    {
        // grabs the materals of all objects, and keeps track of their default colors
        // This is so we can hightlight the objects, and have them revert back to their original colors
        var renderers = GetComponentsInChildren<MeshRenderer>(includeInactive: true);
        modelMaterials = renderers.Where(x => x.material.HasColor("_Color")).Select(x => (x.material, x.material.color)).ToArray();

        refreshHighlight();
    }

    private void Update()
    {
        refreshHighlight();

        hovering = false;
    }

    private void refreshHighlight()
    {
        if (modelMaterials == null)
            return;

        if (hovering)
        {
            foreach (var mat in modelMaterials)
            {
                mat.Item1.color = highlightedColor;
            }
        }
        else
        {
            // revert to original color
            foreach (var mat in modelMaterials)
            {
                mat.Item1.color = mat.Item2;
            }
        }
    }
}
