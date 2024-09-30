using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCLightsController : MonoBehaviour
{
    public VRCToggleSwitch ToggleSwitch;
    public VRCKnob IntensityKnob;
    public Light TargetLight;

    private void OnEnable()
    {
        ToggleSwitch.OnToggle += OnToggle;
        IntensityKnob.OnValueChanged += OnIntensityChanged;
    }

    private void OnDisable()
    {
        ToggleSwitch.OnToggle -= OnToggle;
        IntensityKnob.OnValueChanged -= OnIntensityChanged;
    }

    private void OnIntensityChanged(float value)
    {
        TargetLight.intensity = Mathf.Lerp(0.2f, 0.5f, value);
    }

    private void OnToggle(bool toggleState)
    {
        TargetLight.gameObject.SetActive(toggleState);
    }
}
