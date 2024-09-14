using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectActive : MonoBehaviour
{
    public VRCToggleSwitch ToggleSwitch;
    public GameObject TargetObject;

    private void OnEnable()
    {
        ToggleSwitch.OnToggle += OnToggle;
    }

    private void OnDisable()
    {
        ToggleSwitch.OnToggle -= OnToggle;
    }

    private void OnToggle(bool toggleState)
    {
        TargetObject.SetActive(toggleState);
    }
}
