using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Config Player")]
    private CharacterController controller;

    [SerializeField] private float speedMovement = 3f;
    [SerializeField] private float gravity = -9.81f;

    private Vector2 moveInput;
    private Vector3 velocity;

    [Header("Animação")]
    private Animator anim;
    public bool IsWalk;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnAttack()
    {
        anim.SetTrigger("triggerAttack");
        {

        }
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector3 direction =
           new Vector3(moveInput.x, 0, moveInput.y);
        if (direction.magnitude > 0.1f)
        {
            IsWalk = true;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calcula o ângulo baseado na direção do jogador
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0); //suaviza a rotação do jogador

            //jogador rotaciona baseado na movimentação.
            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            IsWalk = false;
        }
        controller.Move(
            direction *
            speedMovement *
            Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        anim.SetBool("IsWalk", IsWalk);
    }

        private void PlayerAttack()
    {
        anim.SetTrigger("triggerAttack");
        }
}

