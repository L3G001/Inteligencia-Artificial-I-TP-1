using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaguageButton : MonoBehaviour
{
    public SystemLanguage language = SystemLanguage.Spanish;

    void Start()
    {
        /*LocalizationManager.instance.OnChangeLanguage += () =>
        {
            if (language == LocalizationManager.instance.language)
            {
                GetComponent<Button>().image.color = Color.green;
            }
            else
            {
                GetComponent<Button>().image.color = Color.red;
            }
        };*/
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
