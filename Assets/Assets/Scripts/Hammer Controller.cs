using UnityEngine;

public class HammerController : MonoBehaviour
{
    public Transform hammerPosition; // Parent object that follows the mouse
    public Transform hammer; // Hammer child object
    public Animator hammerAnimator; // Animator for the hammer
    public Camera mainCamera; // Main camera in the scene
    public float movementSpeed = 10f; // Speed of the Hammer Position following the mouse
    public float followDelay = 0.2f; // Delay for the hammer to follow the Hammer Position
    public LayerMask raycastLayer; // LayerMask for the raycast to hit only specific objects

    private Vector3 hammerVelocity; // Used for smooth dampening

    void Start()
    {
        if (hammerPosition == null || hammer == null || hammerAnimator == null || mainCamera == null)
        {
            Debug.LogError("Please assign all required references in the Inspector.");
        }
    }

    void Update()
    {
        MoveHammerPositionToMouse();
        SmoothFollowHammer();
        CheckForHit();
    }

    void MoveHammerPositionToMouse()
    {
        // Get mouse position using raycasting, restricted to a specific layer
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayer))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = hammerPosition.position.y; // Maintain constant Y height
            hammerPosition.position = Vector3.Lerp(hammerPosition.position, targetPosition, Time.deltaTime * movementSpeed);
        }
    }

    void SmoothFollowHammer()
    {
        if (hammer != null)
        {
            // Smoothly move the hammer towards the hammer position
            hammer.position = Vector3.SmoothDamp(hammer.position, hammerPosition.position, ref hammerVelocity, followDelay);
        }
    }

    void CheckForHit()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            hammerAnimator.SetTrigger("Hit"); // Trigger hammer hit animation
        }
    }
}
