using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ButtonTranslate : MonoBehaviour
{
    public string ID = default;
    public TextMeshProUGUI textUI = default;

    void Start()
    {
       
        LocalizationManager.instance.OnChangeLanguage += TranslateButton;
        TranslateButton();
    }
    [ContextMenu("translateButton")]
    public void TranslateButton()
    {
        textUI.text = LocalizationManager.instance.GetTranslate(ID);
    }
}