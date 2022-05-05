using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControllerNet : NetworkBehaviour
{
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
}
