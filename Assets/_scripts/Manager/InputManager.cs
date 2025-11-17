using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;

    InputSystem_Actions _inputActions;

    public static InputManager Instance {  get { return _instance; } }


    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        //_inputActions.Disable();
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        _inputActions = new InputSystem_Actions();
    }

    public Vector2 PlayerMovement()
    {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 PlayerLook()
    {
        return _inputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpThisFrame()
    {
        return _inputActions.Player.Jump.WasPressedThisFrame();
    }
    public bool PlayerStartGame()
    {
        return _inputActions.Player.Jump.WasPressedThisFrame();
    }

    public bool PlayerShootThisFrame()
    {
        return _inputActions.Player.Attack.WasPressedThisFrame();
    }

    public bool PlayerShootHeld()
    {
        return _inputActions.Player.Attack.IsPressed();
    }

    public bool PlayerReloadThisFrame()
    {
        if (Keyboard.current != null)
        {
            return Keyboard.current.rKey.wasPressedThisFrame;
        }
        return false;
    }
    public bool OpenMenu()
    {
        return _inputActions.Player.OpenMenu.WasPressedThisFrame();
    }
    public bool PlayerSprinting()
    {
        return _inputActions.Player.Sprint.IsPressed();
    }
    public bool Scope()
    {
        return _inputActions.Player.Scope.IsPressed();
    }

}
