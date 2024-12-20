using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject _pauseMenu = default;
    public bool isPaused = default;
    [SerializeField] private Material _dither, _outline, _portalEffect;
    [SerializeField] private ScriptableRendererFeature _outlineRenderFeature, _ditherRenderFeature;
    [SerializeField] private TMP_Dropdown _colorDepthDropdown, _outlineStyleDropdown;
    [SerializeField] private Toggle _ditherEffectToggle, _borderAnimationToggle;
    [SerializeField] private GameObject _portal;
    [SerializeField] private float _maxDistance = default;

    private Vector2 _playerPosition, _targetPosition;

    void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
        Time.timeScale = 1;
        SetInitialValues();
    }

    private void OnDisable()
    {
        GameManager.instance.inputReader.PauseEvent -= HandlePause;
        GameManager.instance.inputReader.ResumeEvent -= HandleResume;
    }

    private void Start()
    {
        GameManager.instance.inputReader.PauseEvent += HandlePause;
        GameManager.instance.inputReader.ResumeEvent += HandleResume;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "End" || SceneManager.GetActiveScene().name == "IA1") 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
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
                GameManager.instance.inputReader.SetGameplay();
            }
        }
        if (_portal != null)
        {
            _playerPosition = new Vector2(GameManager.instance.playerPosition.position.x, GameManager.instance.playerPosition.position.z);
            _targetPosition = new Vector2(_portal.transform.position.x, _portal.transform.position.z);
            if (Vector2.Distance(_playerPosition, _targetPosition) < _maxDistance) { PortalEffect(); }
            else { PortalEffectOut(); }
        }
        else { if (_portalEffect.GetFloat("_VignetteAmount") >= 0) { PortalEffectOut(); } }
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

    void PortalEffect()
    {
        float distance = Vector2.Distance(_playerPosition, _targetPosition);
        float normalizedDistance = Mathf.Clamp01(distance / _maxDistance);
        float invert = 1 - normalizedDistance;
        float effectValue = invert * 1.04f;
        _portalEffect.SetFloat("_VignetteAmount", effectValue + 0.2f);
    }

    void PortalEffectOut()
    {
        float newValue = Mathf.Lerp(_portalEffect.GetFloat("_VignetteAmount"), 0, Time.deltaTime * 0.7f);
        if (newValue < 0.15f) { newValue = 0; }
        _portalEffect.SetFloat("_VignetteAmount", newValue);
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
