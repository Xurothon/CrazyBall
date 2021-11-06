using UnityEngine;

public class BodyPartsContainer : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _bodyParts;
    [SerializeField] private float _buffForce;

    public void ActiveBodyParts(Vector3 ballPosition)
    {
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].gameObject.SetActive(true);
            Vector3 direction = _bodyParts[i].transform.position - ballPosition;
            _bodyParts[i].AddForce(direction * _buffForce, ForceMode.Impulse);
        }
    }

    private void DisableBodyParts()
    {
        for (int i = 0; i < _bodyParts.Length; i++)
        {
            _bodyParts[i].gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        DisableBodyParts();
    }
}
