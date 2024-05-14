using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
/**
    basic temp controller that handles players movement
*/
public class MovementController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    private Vector3 movementDirection;
    public void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody>();
        this.movementDirection = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    public void Update()
    {   
        this.movementDirection.x = Input.GetAxisRaw("Horizontal");
        this.movementDirection.z = Input.GetAxisRaw("Vertical");
        if(this.movementDirection.magnitude > 0) {
            this.movementDirection = this.movementDirection.normalized;
        }
    }
    public void FixedUpdate() {
        this.rigidBody.velocity = this.movementDirection * this.moveSpeed;
        if(this.movementDirection.magnitude != 0) {
            this.transform.LookAt(this.transform.position + movementDirection);
        }
    }
}
