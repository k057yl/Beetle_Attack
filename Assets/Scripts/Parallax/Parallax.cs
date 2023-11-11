using UnityEngine;
using Zenject;

public class Parallax : MonoBehaviour
{
    [SerializeField, Range(Constants.ZERO, Constants.ONE)] private float[] _parallaxStrengths;
    [SerializeField] private float _parallaxStrengthY;

    private Vector3 _targetPreviousPosition;

    [Inject] private CharacterController _characterController;

    private void UpdatePreviousPosition(Transform targetTransform)
    {
        if (_characterController == null) return;

        _characterController.transform.position = targetTransform.position;
        _targetPreviousPosition = _characterController.transform.position;
    }

    private void Update()
    {
        if (_characterController == null) return;

        var delta = _characterController.transform.position - _targetPreviousPosition;

        _targetPreviousPosition = _characterController.transform.position;

        for (int i = 0; i < _parallaxStrengths.Length; i++)
        {
            var parallaxTransform = transform.GetChild(i);
            var parallaxStrength = _parallaxStrengths[i];

            parallaxTransform.position += new Vector3(delta.x * parallaxStrength, delta.y * _parallaxStrengthY, Constants.ZERO);
        }

        UpdatePreviousPosition(_characterController.transform);
    }
}
