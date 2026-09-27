using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light m_Light;
    public bool isOn;

    void Start()
    {
        m_Light.enabled = isOn;
    }

    public string GetDescription()
    {
        if (isOn) return "Press [E] to turn <color=red>off</color> the light";
        return "Press [E] to turn <color=green>on</color> the light";
    }

    public void Interact()
    {
        isOn = !isOn;
        m_Light.enabled = isOn;
    }
}
