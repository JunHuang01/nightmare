using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float Speed;
    public float turnSmoothing;
    public float speedDampTime = 0.1f;

    private Animator anim;
    private Vector3 moveDirection = Vector3.zero;

    void Awake() {
        anim = GetComponent<Animator>();
        //GetComponent<Animation>().wrapMode = WrapMode.Loop;

    }

    void FixedUpdate() {
        float horMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        MovementManagement(horMovement, verMovement);
    }

    void MovementManagement(float horMovement, float verMovement) {
        CharacterController controller = GetComponent<CharacterController>();
        if (horMovement != 0f || verMovement != 0f)
        {
            transform.Rotate(0, horMovement * turnSmoothing, 0);
            moveDirection = new Vector3(0, 0, verMovement);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= 5.5f;
            //Rotating(horMovement, verMovement);
            anim.SetFloat("Speed", 5.5f, speedDampTime, Time.deltaTime);
        }
        else {
            anim.SetFloat("Speed", 0);
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    /*
    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        rigidbody.MoveRotation(newRotation);
    }
    */
}
