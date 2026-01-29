using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 dir;
    [SerializeField] private Vector3 oldDir;
    [SerializeField] private Animator animator;
    public float speed = 5;
    public Actions curAction;

    public enum Actions
    {
        Idle = 0,
        Walk = 1
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(dir * speed * Time.deltaTime, ForceMode.Impulse);
        
        if(dir == Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(oldDir);

            if(curAction != Actions.Idle)
            {
                curAction = Actions.Idle;
                animator.SetInteger("Action", (int)curAction);
            }
            
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(dir);
            oldDir = dir;

            if(curAction != Actions.Walk)
            {
                curAction = Actions.Walk;
                animator.SetInteger("Action", (int)curAction);
            }
        }
    }

    public void Move(InputAction.CallbackContext context){
        Vector2 orDir = context.ReadValue<Vector2>();

        dir.x  = orDir.x;
        dir.z = orDir.y;
    }

}
