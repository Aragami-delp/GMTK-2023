using UnityEngine;
using Cinemachine;

public class CameraReturn : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private Transform objectToFollow;
    [SerializeField]
    private float returnSpeed = 5f;

    private CinemachineFramingTransposer transposer;

    private PlayerMovement movement;

    private float returnTime = 0;

    [SerializeField]
    private float deadzoneWithStart;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        objectToFollow = virtualCamera.Follow;

        transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        deadzoneWithStart = transposer.m_DeadZoneWidth;

        movement = objectToFollow.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement.IsMoving)
        {
            returnTime = 0;
            transposer.m_DeadZoneWidth = deadzoneWithStart;
        }
        else
        {
            returnTime += Time.deltaTime;

            returnTime = Mathf.Lerp(deadzoneWithStart, 0, returnTime / returnSpeed);
            transposer.m_DeadZoneWidth = 0f;

        }
    }
}

