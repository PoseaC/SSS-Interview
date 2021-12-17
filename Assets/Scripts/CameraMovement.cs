using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Object Selection")]
    public Transform pivotPoint;
    public Camera mainCamera;
    public LayerMask whatIsInteractable;
    public float interactionRange = 10f;
    GameObject interactedObject;
    [HideInInspector] public float distanceFromCamera;

    [Header("Camera Movement")]
    public Vector2 verticalMovementLimit;
    public float movementSpeed;

    Vector3 movementDirection;
    float horizontalInput;
    float verticalInput;
    private void Start()
    {
        distanceFromCamera = mainCamera.nearClipPlane + 10;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        horizontalInput += -Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        verticalInput += Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        verticalInput = Mathf.Clamp(verticalInput, verticalMovementLimit.x, verticalMovementLimit.y);

        movementDirection = Vector3.up * horizontalInput + Vector3.right * verticalInput;

        transform.rotation = Quaternion.Euler(movementDirection);

        distanceFromCamera += Input.mouseScrollDelta.y;
        distanceFromCamera = Mathf.Clamp(distanceFromCamera, mainCamera.nearClipPlane + 1, mainCamera.farClipPlane);
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, interactionRange, whatIsInteractable))
            {
                distanceFromCamera = hit.distance;
                interactedObject = hit.collider.gameObject;
                interactedObject.transform.SetParent(pivotPoint);
                interactedObject.transform.localPosition = Vector3.zero;
                interactedObject.GetComponent<BuildingBlock>().PickUpBlock();
            }
        }
        if (Input.GetMouseButtonUp(0) && interactedObject != null)
        {
            interactedObject.GetComponent<BuildingBlock>().DropBlock();
            interactedObject = null;
        }
        if (Input.GetMouseButton(0))
        {
            pivotPoint.position = mouseWorldPosition;
        }
    }
}
