using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator Horse;
    [SerializeField] private Transform cam;
    [SerializeField] private AudioSource WalkAudioSource;

    private Horse horse;
    private Controls controls;

    public float speed = 5f;

    private Vector2 moveInput;
    private Vector3 dir;
    private Vector3 oldDir;

    [SerializeField] private bool grounded;

    public Actions curAction;
    public bool isInteracting;

    public enum Actions
    {
        Idle = 0,
        Walk = 1,
    }
    private void Awake()
    {
        controls = new Controls();

        controls.Player.Enable();

        controls.Player.Move.performed += Move;
        controls.Player.Move.canceled += Move;

        controls.Player.Interact.performed += ctx => Interact();
        controls.Player.Jump.performed += ctx => Jump();
    }

    void Start()
    {
        horse = FindFirstObjectByType<Horse>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Camera.main.farClipPlane = 5000f;
    }

    void Update()
    {
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();

        dir = moveInput.x * camRight + moveInput.y * camForward;

        rb.AddForce(dir * speed * Time.deltaTime, ForceMode.Impulse);

        if (dir.sqrMagnitude < 0.01f)
        {
            if (oldDir != Vector3.zero)
            {
                Quaternion idleRot = Quaternion.LookRotation(oldDir);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, idleRot, 5f * Time.deltaTime));
            }

            if (curAction != Actions.Idle)
            {
                curAction = Actions.Idle;
                animator.SetInteger("Action", (int)curAction);
                Horse.SetInteger("Action", (int)curAction);

                WalkAudioSource.Stop();
            }
        }
        else
        {
            oldDir = dir;
            Quaternion targetRot = Quaternion.LookRotation(dir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, 10f * Time.deltaTime));

            if (curAction != Actions.Walk)
            {
                curAction = Actions.Walk;
                animator.SetInteger("Action", (int)curAction);
                Horse.SetInteger("Action", (int)curAction);

                WalkAudioSource.Play();
            }
        }

        //horese speed adjustment
        if (horse.hasHorse == true)
        {
            speed = 300f;
        }
        else
        {
            speed = 150f;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void Interact()
    {
        Debug.Log("Interacted");
        isInteracting = true;
    }
    public void Jump()
    {
        if (grounded)
        {
            if (horse.hasHorse == true)
            {
                rb.AddForce(Vector3.up * 3500f);
            }
            else
            {
                rb.AddForce(Vector3.up * 3000f);
            }
            grounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //check grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
