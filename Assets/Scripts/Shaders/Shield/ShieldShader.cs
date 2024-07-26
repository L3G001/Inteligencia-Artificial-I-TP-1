using System.Collections;
using UnityEngine;

public class ShieldShader : MonoBehaviour
{
    Camera _cam = default;
    Renderer _renderer = default;
    [SerializeField] AnimationCurve _displacementCurve = default;
    [SerializeField] float _displacementMagnitude = default;
    [SerializeField] float _lerpSpeed = default;
    [SerializeField] float _dissolveSpeed = default;
    bool _shieldOn = default;
    Coroutine _disolveCoroutine = default;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _cam = Camera.main;
        _renderer.material.SetFloat("_Disolve_Value", -0.05f);
    }
    void Update()
    {
        #region Always face the camera
        transform.forward = _cam.transform.position - transform.position;
        #endregion
        #region Zoom Adjustment
        Vector3 screenPoint = _cam.WorldToScreenPoint(transform.position);
        screenPoint.x = screenPoint.x / Screen.width;
        screenPoint.y = screenPoint.y / Screen.height;
        _renderer.material.SetVector("_Object_Screen_Position", screenPoint);
        #endregion
        #region Shield Toggle
        if (_renderer.material.GetFloat("_Disolve_Value") >= 0.65f) { gameObject.GetComponent<Collider>().enabled = false; }
        else { gameObject.GetComponent<Collider>().enabled = true; }
        #endregion
    }

    public void HitShield(Vector3 hitPoint)
    {
        _renderer.material.SetVector("_Hit_Position", hitPoint);
        StopAllCoroutines();
        StartCoroutine(Coroutine_HitDisplacement());
    }
    //Llamar a esta función para abrir el escudo
    public void OpenCloseShield()
    {
        float target = _renderer.material.GetFloat("_Disolve_Value") + 0.25f;
        if (_disolveCoroutine != null) { StopCoroutine(_disolveCoroutine); }
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(target));
    }

    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Displacement_Strength", _displacementCurve.Evaluate(lerp) * _displacementMagnitude);
            lerp += Time.deltaTime * _lerpSpeed;
            yield return null;
        }
    }

    IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _renderer.material.GetFloat("_Disolve_Value");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve_Value", Mathf.Lerp(start, target, lerp));
            lerp += Time.deltaTime * _dissolveSpeed;
            yield return null;
        }
    }
}
