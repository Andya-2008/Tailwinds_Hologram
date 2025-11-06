using UnityEngine;

public class MouseDragRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 4f;      // Sensitivity
    public float damping = 5f;            // How quickly rotation slows after release
    public float maxVelocity = 500f;      // Cap spin speed

    private Vector3 lastMousePosition;
    private float rotationVelocity;       // Only one float for Y-axis rotation
    private bool dragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
            rotationVelocity = 0f;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if (dragging)
        {
            // Horizontal mouse movement controls rotation around Y
            Vector3 delta = Input.mousePosition - lastMousePosition;
            rotationVelocity = -delta.x * rotationSpeed; // Flip sign for natural feel
            rotationVelocity = Mathf.Clamp(rotationVelocity, -maxVelocity, maxVelocity);

            lastMousePosition = Input.mousePosition;
        }
        else
        {
            // Smoothly slow down rotation when released
            rotationVelocity = Mathf.Lerp(rotationVelocity, 0f, damping * Time.deltaTime);
        }

        // Apply rotation around Y-axis (world)
        transform.Rotate(0f, rotationVelocity * Time.deltaTime, 0f, Space.World);
    }
}