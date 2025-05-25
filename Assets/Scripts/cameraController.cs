using NUnit.Framework;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public static cameraController Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private Vector3 defaultPos;
    [SerializeField] private Quaternion defaultRotation;
    [SerializeField] private Vector3 topPos;
    [SerializeField] private Quaternion topRotation;

    [SerializeField] private float moveSpeed, rotationSpeed;
    private bool isAtDefaultPos = true;
    void Update()
    {
        if (!audioController.Instance.isPause) moveCamDefault();
        else if (audioController.Instance.isPause) moveCamTop();
    }
    void moveCamTop()
    {
        transform.position = Vector3.MoveTowards(transform.position, topPos, moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, topRotation, rotationSpeed);
    }

    void moveCamDefault()
    {
        transform.position = Vector3.MoveTowards(transform.position, defaultPos, moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, rotationSpeed);
    }
}
