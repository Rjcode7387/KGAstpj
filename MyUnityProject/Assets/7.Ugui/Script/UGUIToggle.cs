using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUIToggle : MonoBehaviour
{
    public void OnValueChange(bool ison)
    {
        print($"{name}토글 눌림 :{ison}");
    }
}
