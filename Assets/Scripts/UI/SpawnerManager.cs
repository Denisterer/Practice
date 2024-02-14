using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnerManager : MonoBehaviour
{
    public TMP_Dropdown armor;
    public TMP_Dropdown bullet;
    public event Action<string,string> OnButtonClick;

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void ButtonClick()
    {
        string value1 = armor.options[armor.value].text;
        string value2 = bullet.options[bullet.value].text;
        OnButtonClick?.Invoke(value1, value2);
        
    }
}
