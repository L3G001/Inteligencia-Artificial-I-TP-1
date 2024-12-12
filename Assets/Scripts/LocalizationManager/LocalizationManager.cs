using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance = default;
    public event Action OnChangeLanguage;
    [SerializeField] private SystemLanguage _language = default;
    public SystemLanguage language
    {
        get => _language;
        set 
        {
            _language = value;
            OnChangeLanguage?.Invoke();
        }
    }

    [SerializeField] private DataLocalization[] _data = default;

    [SerializeField] private Dictionary<SystemLanguage, Dictionary<string, string>> _translate = new();

    private void Awake()
    {
        instance = this;
        _translate = LanguageU.LoadTranslate(_data);
        Debug.Log(language);
    }


    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(language))
            return "No lang";

        if (!_translate[SystemLanguage.English].ContainsKey(ID))
            return "No ID";

        Debug.Log(_translate[_language][ID]);
        return _translate[language][ID];
    }

    [ContextMenu("Translate")]
    public void Translate()
    {
       OnChangeLanguage?.Invoke();
    }
  
    public void ChangeLeguange(SystemLanguage language)
    {
        this.language = language;
    }
}