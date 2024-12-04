using UnityEngine;

public class HammerController : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask raycastLayer;
    public Vector3 offset = new Vector3(0, 0.5f, 0); 

    [Header("Rotation Settings")]
    public float minRotationX = -135f;
    public float maxRotationX = -90f;
    public int hitFrames = 15;
    public int returnFrames = 10; 
    public float followSpeed = 10f; 

    private Vector3 currentVelocity;
    private bool isHitting = false;
    private float rotationProgress = 0f;
    private bool returningToDefault = false; 

    private float currentYRotation; 
    private float currentZRotation; 

    void Start()
    {
    

        currentYRotation = transform.localEulerAngles.y;
        currentZRotation = transform.localEulerAngles.z;
    }

    void Update()
    {
        SmoothFollowCursor();
        HandleInput();
        RotateHammer();
    }

    void SmoothFollowCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayer))
        {
            Vector3 targetPosition = hit.point + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 1f / followSpeed);
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !isHitting && !returningToDefault)
        {
            isHitting = true;
            rotationProgress = 0f;
        }
    }

    void RotateHammer()
    {
        if (isHitting)
        {
            rotationProgress += Time.deltaTime * (60f / hitFrames);
            float newXRotation = Mathf.Lerp(minRotationX, maxRotationX, rotationProgress);
            transform.localEulerAngles = new Vector3(newXRotation, currentYRotation, currentZRotation);

            if (rotationProgress >= 1f)
            {
                isHitting = false;
                returningToDefault = true;
                rotationProgress = 0f;
            }
        }
        else if (returningToDefault)
        {
            rotationProgress += Time.deltaTime * (60f / returnFrames);
            float newXRotation = Mathf.Lerp(maxRotationX, minRotationX, rotationProgress);
            transform.localEulerAngles = new Vector3(newXRotation, currentYRotation, currentZRotation);

            if (rotationProgress >= 1f)
            {
                returningToDefault = false;
                rotationProgress = 0f; 
            }
        }
    }
}
