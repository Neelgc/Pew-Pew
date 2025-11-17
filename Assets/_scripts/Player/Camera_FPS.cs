
using Unity.VisualScripting;
using UnityEngine;

public class Camera_FPS : MonoBehaviour
{
    public Transform targetRotate;
    public float sensitivity;

    InputManager _inputManager;
    Quaternion _startRotation;

    Vector2 rotation;

    public void Start()
    {
        _inputManager = InputManager.Instance;
        sensitivity = SavingLocal.Instance.LocalSave.Sensitivity;
        _startRotation = transform.rotation;
    }

    public void Init()
    {
        transform.rotation = _startRotation;
    }

    internal void UpdateCam()
    {
        Vector2 look = _inputManager.PlayerLook();

        rotation.x += look.x * sensitivity * Time.deltaTime;
        rotation.y += look.y * sensitivity * Time.deltaTime;
        rotation.y = Mathf.Clamp(rotation.y, -90, 90);

        Quaternion newRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);

        targetRotate.rotation = newRotation;
    }
}
