using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] string _sceneToLoad = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            UIManager.Instance.LoadScene(_sceneToLoad);
        }
    }
}
