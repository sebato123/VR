using UnityEngine;
using UnityEngine.InputSystem;

public class VRGunInput : MonoBehaviour
{
    [Header("Referencias")]
    public GunVR gun;                       // Script del arma
    public InputActionProperty fireAction;  // Acción del gatillo (mano derecha)

    private void OnEnable()
    {
        if (fireAction.action != null)
            fireAction.action.Enable();
    }

    private void OnDisable()
    {
        if (fireAction.action != null)
            fireAction.action.Disable();
    }

    private void Update()
    {
        if (gun == null || fireAction.action == null)
            return;

        if (fireAction.action.WasPressedThisFrame())
        {
            gun.Fire();
        }
    }
}
