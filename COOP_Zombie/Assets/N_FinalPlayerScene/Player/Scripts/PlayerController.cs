using UnityEngine;

using Unity.Netcode;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private NetworkVariable<Vector3> move = new NetworkVariable<Vector3>();
    [SerializeField]
    private NetworkVariable<Vector3> Rotate = new NetworkVariable<Vector3>();

    //public Animator anim;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    public Transform cameraTransform;
    private Vector2 initialPositionRange = new Vector2(-2, 2);
    private Vector3 oldRotation = Vector3.zero;
    private Transform cameraRefTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        //cameraTransform = Camera.main.transform;
        cameraRefTransform = new GameObject().transform;

        if (IsOwner && IsClient)
            transform.position = new Vector3(Random.Range(initialPositionRange.x, initialPositionRange.y), 0f, Random.Range(initialPositionRange.x, initialPositionRange.y));
    }

    private void Update()
    {
        
        cameraRefTransform.eulerAngles = new Vector3(0f, cameraTransform.eulerAngles.y, 0f);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        if (IsOwner && IsClient)
        {
            HandleRotationServerRpc();
            HandleMovementServerRpc();
        }


        // Changes the height position of the player
        if (inputManager.PlayerJumped() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        //Makes the player Jump
        controller.Move(playerVelocity * Time.deltaTime);
    }
    [ServerRpc]
   public void HandleRotationServerRpc()
   {
            //Gets the current rotation of the player
            Rotate.Value = this.transform.eulerAngles; 
            // Places the location of the player and it's camera into a new Vector
            Vector3 rotationDirection = new Vector3(this.transform.eulerAngles.x, cameraRefTransform.eulerAngles.y, this.transform.eulerAngles.z);
            //Turns the Vector from before into a NetworkVariable
            Rotate.Value = rotationDirection; 
            //Rotates the player
            this.transform.eulerAngles = Rotate.Value; 
   }
    [ServerRpc]
    public void HandleMovementServerRpc() 
    {
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 direction = new Vector3(movement.x, 0f, movement.y);
        direction = cameraRefTransform.forward * direction.z + cameraRefTransform.right.normalized * direction.x;
        direction.y = 0f;
        move.Value = direction; //Permite que los otros jugadores vean el movimiento
        controller.Move(direction * Time.deltaTime * playerSpeed);
    }
}
