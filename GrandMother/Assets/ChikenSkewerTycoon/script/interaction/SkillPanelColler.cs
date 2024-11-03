using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelColler : MonoBehaviour
{
    public SkillPanel skillPanel;
    public void ToggleSkillPanel()
    {
        if (skillPanel != null)
        {
            bool isActive = skillPanel.gameObject.activeSelf;
            skillPanel.gameObject.SetActive(!isActive);
        }
    }
}
