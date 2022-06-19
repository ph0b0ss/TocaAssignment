using UnityEngine;
using UnityEngine.UI;

public class IndicatorArrow : MonoBehaviour
{
    private GameObject _player;
    private GameObject _gameObject;
    private GameRunner _gameRunner;
    private Image _image;
    private RectTransform _rectTransform;
    private Camera _camera;
    private Image _image1;

    private void Awake()
    {
        _image1 = GetComponent<Image>();
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _gameObject = GameObject.Find("GameRunner");
        _gameRunner = _gameObject.GetComponent<GameRunner>();
        _player = GameObject.Find("Sphere");
    }

    void Update()
    {
        if (_gameRunner.isOnAssignment)
        {
            _image.enabled = true;

            var goVector = _gameRunner.targetDestination.transform.position - _player.transform.position;
            goVector.y = 0f;
            float angle = Vector3.SignedAngle(Vector3.forward, goVector, Vector3.down);
            angle += _camera.gameObject.transform.rotation.eulerAngles.y;
            _rectTransform.rotation = Quaternion.Euler(0f,0f, angle);
        }
        else
        {
            _image1.enabled = false;
        }
    }
}
