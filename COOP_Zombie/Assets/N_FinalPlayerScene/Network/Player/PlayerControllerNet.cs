using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControllerNet : NetworkBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        ReverseWalk
    }

    [SerializeField]
    private float speed = 0.005f;

    [SerializeField]
    private float rotationSpeed = 0.5f;

    private Vector2 initialPositionRange = new Vector2(-3, 3);

    [SerializeField]
    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<Vector3> networkRotationDirection = new NetworkVariable<Vector3>(); // esta

    [SerializeField]
    private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();

    private Vector3 oldInputPosition = Vector3.zero;
    private Vector3 oldInputRotation = Vector3.zero;
    private CharacterController controller;
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(IsOwner && IsClient)
        {
            transform.position = new Vector3(Random.Range(initialPositionRange.x, initialPositionRange.y), 0f, Random.Range(initialPositionRange.x, initialPositionRange.y));
            PlayerCamera.instance.FollowPlayer(transform.Find("FollowMe"));
        }
        
    }

    private void Update()
    {
        if (IsOwner && IsClient)
            ClientInput();

        ClientMoveAndRotate();
        ClientVisuals();
    }

    private void ClientInput()
    {
        Vector3 inputRotation = new Vector3(0f, Input.GetAxis("Horizontal"), 0f);
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        float zInput = Input.GetAxis("Vertical");

        Vector3 inputPosition = direction * zInput;

        if(oldInputPosition != inputPosition || oldInputRotation != inputRotation)
        {
            oldInputPosition = inputPosition;
            oldInputRotation = inputRotation;

            UpdateClientPositionAndRotationServerRpc(inputPosition * speed, inputRotation * rotationSpeed);
        }
        if (zInput > 0)
            UpdatePlayerStateServerRpc(PlayerState.Walk);
        else if (zInput < 0)
            UpdatePlayerStateServerRpc(PlayerState.ReverseWalk);
        else
            UpdatePlayerStateServerRpc(PlayerState.Idle);
    }

    private void ClientMoveAndRotate()
    {
        if (networkPositionDirection.Value != Vector3.zero)
            controller.Move(networkPositionDirection.Value);

        if (networkRotationDirection.Value != Vector3.zero)// esta
            transform.Rotate(networkRotationDirection.Value);// esta
    }

    private void ClientVisuals()
    {
        if (networkPlayerState.Value == PlayerState.Walk)
            animator.SetFloat("Walk", 1f);
        else if(networkPlayerState.Value == PlayerState.ReverseWalk)
            animator.SetFloat("Walk", -1f);
        else 
            animator.SetFloat("Walk", 0f);
    }

    [ServerRpc]
    public void UpdateClientPositionAndRotationServerRpc(Vector3 newPositionDirection, Vector3 newRotationDirection)
    {
        networkPositionDirection.Value = newPositionDirection;
        networkRotationDirection.Value = newRotationDirection;
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState newState)
    {
        networkPlayerState.Value = newState;
    }

    /*
    #endregion OldMovement

         [SerializeField]
         private float speed = 0.005f;

         private Vector2 defaultPositionRange = new Vector2(-3, 3);

         [SerializeField]
         private NetworkVariable<float> zPosition = new NetworkVariable<float>();

         [SerializeField]
         private NetworkVariable<float> xPosition = new NetworkVariable<float>();

         private float oldZPosition, oldXPosition;

         private void Start()
         {
             transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y), 0f, Random.Range(defaultPositionRange.x, defaultPositionRange.y));
         }

         private void Update()
         {
             if(IsServer)
                 UpdateServer();

             if (IsLocalPlayer)
                 UpdateClient();
         }

         private void UpdateServer()
         {
             transform.position = new Vector3(transform.position.x + xPosition.Value, transform.position.y, transform.position.z + zPosition.Value);
         }

         private void UpdateClient()
         {
             float zInput = 0f;
             float xInput = 0f;

             if (Input.GetKey(KeyCode.W))
                 zInput += speed;
             if (Input.GetKey(KeyCode.A))
                 xInput -= speed;
             if (Input.GetKey(KeyCode.S))
                 zInput -= speed;
             if (Input.GetKey(KeyCode.D))
                 xInput += speed;

             if(oldZPosition != zInput || oldXPosition != xInput)
             {
                 oldZPosition = zInput;
                 oldXPosition = xInput;

                 UpdateClientPositionServerRpc(zInput, xInput);
             }
         }

         [ServerRpc]
         public void UpdateClientPositionServerRpc(float zInput, float xInput)
         {
             xPosition.Value = xInput;
             zPosition.Value = zInput;
         }

    #endregion
    */
}
