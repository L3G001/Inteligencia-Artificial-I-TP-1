using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Material _dither, _outline;
    [SerializeField] private GameObject _pauseMenu = default;
    [SerializeField] private ScriptableRendererFeature _outlineRenderFeature, _ditherRenderFeature;
    [SerializeField] private TMP_Dropdown _colorDepthDropdown, _outlineStyleDropdown;
    [SerializeField] private Toggle _ditherEffectToggle, _borderAnimationToggle;
    public bool isPaused = default;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this); }
        Time.timeScale = 1;
        SetInitialValues();
    }

    private void Start()
    {
        GameManager.instance._inputReader.PauseEvent += HandlePause;
        GameManager.instance._inputReader.ResumeEvent += HandleResume;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu") { Debug.Log(SceneManager.GetActiveScene().name); return; }
        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.instance._inputReader.SetGameplay();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Reset()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void HandlePause()
    {
        if (_pauseMenu != null) { _pauseMenu.SetActive(true); }
        isPaused = true;
    }

    private void HandleResume()
    {
        if (_pauseMenu != null) { _pauseMenu.SetActive(false); }
        isPaused = false;
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ColorDepthDropdown(int index)
    {
        switch (index)
        {
            case 0: _dither.SetFloat("_Color_Resolution", 8); break;
            case 1: _dither.SetFloat("_Color_Resolution", 16); break;
            case 2: _dither.SetFloat("_Color_Resolution", 32); break;
            case 3: _dither.SetFloat("_Color_Resolution", 64); break;
        }
    }
    public void DitherEffectState()
    {
        if (_ditherRenderFeature.isActive) { _ditherRenderFeature.SetActive(false); }
        else { _ditherRenderFeature.SetActive(true); }
    }

    public void BorderAnimation()
    {
        if (_dither.GetFloat("_Animated") == 1) { _dither.SetFloat("_Animated", 0); }
        else { _dither.SetFloat("_Animated", 1); }
    }

    public void OutlineStyleDropdown(int index)
    {
        switch (index)
        {
            case 0:
                _outlineRenderFeature.SetActive(false);
                break;
            case 1:
                _outlineRenderFeature.SetActive(true);
                _outline.SetFloat("_Use_Normal", 1f);
                _outline.SetFloat("_Use_Combined_Input", 0f);
                break;
            case 2:
                _outlineRenderFeature.SetActive(true);
                _outline.SetFloat("_Use_Normal", 0f);
                _outline.SetFloat("_Use_Combined_Input", 0f);
                break;
            case 3:
                _outlineRenderFeature.SetActive(true);
                _outline.SetFloat("_Use_Normal", 0f);
                _outline.SetFloat("_Use_Combined_Input", 1f);
                break;
        }
    }
    void SetInitialValues()
    {
        #region Dither
        if (_ditherRenderFeature.isActive) { _ditherEffectToggle.isOn = true; }
        else { _ditherEffectToggle.isOn = false; }

        if (_dither.GetFloat("_Color_Resolution") == 8) { _colorDepthDropdown.value = 0; }
        else if (_dither.GetFloat("_Color_Resolution") == 16) { _colorDepthDropdown.value = 1; }
        else if (_dither.GetFloat("_Color_Resolution") == 32) { _colorDepthDropdown.value = 2; }
        else if (_dither.GetFloat("_Color_Resolution") == 64) { _colorDepthDropdown.value = 3; }

        if (_dither.GetFloat("_Animated") == 1) { _borderAnimationToggle.isOn = true; }
        else { _borderAnimationToggle.isOn = false; }
        #endregion
        #region Outline
        if (!_outlineRenderFeature.isActive) { _outlineStyleDropdown.value = 0; }
        else if (_outline.GetFloat("_Use_Normal") == 1) { _outlineStyleDropdown.value = 1; }
        else if (_outline.GetFloat("_Use_Combined_Input") == 0 && _outline.GetFloat("_Use_Normal") == 0) { _outlineStyleDropdown.value = 2; }
        else if (_outline.GetFloat("_Use_Combined_Input") == 1) { _outlineStyleDropdown.value = 3; }
        #endregion
    }
}
