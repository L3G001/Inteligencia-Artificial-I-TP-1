using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaguageButton : MonoBehaviour
{
    public static SystemLanguage language = SystemLanguage.Spanish;

    void Start()
    {
        
        if (language == SystemLanguage.Slovenian) { gameObject.GetComponent<TMP_Dropdown>().value = 0; }
        else if (language == SystemLanguage.English) { gameObject.GetComponent<TMP_Dropdown>().value = 1; }
        var dropdownValue = gameObject.GetComponent<TMP_Dropdown>().value;
        LanguageValue(dropdownValue);
        ChangeLanguage();
        Debug.Log(language);
        
    }

    public void ChangeLanguage()
    {
        LocalizationManager.instance.ChangeLeguange(language);
        Debug.Log(language);
    }

    public void LanguageValue(int value)
    {
        Debug.LogWarning(gameObject.GetComponent<TMP_Dropdown>().value);
        language = value switch
        {
            0 => SystemLanguage.Slovenian,// por alguna razon desconosida si ponemos Spanish no funciona y tira swidish que es el sigiente y si ponemos slovenian tira spanish
            1 => SystemLanguage.English,
            _ => SystemLanguage.Spanish,
        };
    }
}
