using UnityEngine;

public class VRCEjectorControl : MonoBehaviour
{
    public VRCButton EjectorButton;
    public CharacterController CharController;

    Vector3 velocity;

    private void Eject()
    {
        velocity = Vector3.up * 10;
    }

    private void Update()
    {
        CharController.Move(velocity * Time.deltaTime);

        if (CharController.isGrounded)
        {
            velocity = Vector3.zero;
        }
        else
        {
            velocity += Physics.gravity * Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        EjectorButton.OnPress += Eject;
    }

    private void OnDisable()
    {
        EjectorButton.OnPress -= Eject;
    }
}
