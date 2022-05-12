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

        public Animator anim;
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private InputManager inputManager;
        private Transform cameraTransform;
    private Vector2 initialPositionRange = new Vector2(-3, 3);



    private void Start()
        {
            controller = GetComponent<CharacterController>();
            inputManager = InputManager.Instance;
            cameraTransform = Camera.main.transform;
        if (IsOwner && IsClient)
            transform.position = new Vector3(Random.Range(initialPositionRange.x, initialPositionRange.y), 0f, Random.Range(initialPositionRange.x, initialPositionRange.y));
    }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            /*if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }*/

            // Changes the height position of the player..
            if (inputManager.PlayerJumped() && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
           Animate();
        }
    void Animate() 
    {
        if (inputManager.GetPlayerMovement() != Vector2.zero) 
        {
            anim.SetFloat("Horizontal", inputManager.GetPlayerMovement().x);

            anim.SetFloat("Vertical", inputManager.GetPlayerMovement().y);
        }
        
        anim.SetFloat("Speed", inputManager.GetPlayerMovement().sqrMagnitude);
    }
    
}

/*
 * using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    
    
        [SerializeField]
        private float playerSpeed = 2.0f;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;

        public Animator anim;
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private InputManager inputManager;
        private Transform cameraTransform;


       
        private void Start()
        {
            controller = GetComponent<CharacterController>();
            inputManager = InputManager.Instance;
            cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            
if (inputManager.PlayerJumped() && groundedPlayer)
{
    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
}

playerVelocity.y += gravityValue * Time.deltaTime;
controller.Move(playerVelocity * Time.deltaTime);
Animate();
        }
    void Animate()
{
    if (inputManager.GetPlayerMovement() != Vector2.zero)
    {
        anim.SetFloat("Horizontal", inputManager.GetPlayerMovement().x);

        anim.SetFloat("Vertical", inputManager.GetPlayerMovement().y);
    }

    anim.SetFloat("Speed", inputManager.GetPlayerMovement().sqrMagnitude);
}
    
}

 */
