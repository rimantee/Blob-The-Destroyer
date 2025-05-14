using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlob : MonoBehaviour
{
    [Header("Movement and Growth")]
    public float moveSpeed = 10f;
    private Rigidbody rb;
    private Camera mainCam;
    private PlayerInputActions inputActions;
    private Vector2 screenPoint;
    private float groundY = 0f;
    private float blobSize = 1f;
    public float growthRate = 0.1f;

    [Header("Menu Idle Mode")]
    public bool isInMenu = false;
    public float idleBobSpeed = 2f;
    public float idleBobHeight = 0.1f;
    private Vector3 initialPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = Camera.main;

        groundY = GetComponent<Collider>().bounds.extents.y;
        initialPosition = transform.position;

        inputActions = new PlayerInputActions();
        inputActions.Player.Point.performed += ctx => screenPoint = ctx.ReadValue<Vector2>();
        inputActions.Player.Point.canceled += ctx => screenPoint = Vector2.zero;
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isInMenu) return; // No eating in menu

        if (other.CompareTag("Eatable"))
        {
            float objectSize = other.bounds.extents.magnitude;

            if (blobSize >= objectSize)
            {
                Destroy(other.gameObject);
                Grow();
            }
        }
    }

    void Grow()
    {
        blobSize += growthRate;
        transform.localScale = Vector3.one * blobSize;

        float halfHeight = GetComponent<Collider>().bounds.extents.y;
        transform.position = new Vector3(transform.position.x, halfHeight, transform.position.z);
    }

    void FixedUpdate()
    {
        if (isInMenu)
        {
            IdleWobble();
            return;
        }

        Ray ray = mainCam.ScreenPointToRay(screenPoint);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, groundY, 0));

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPoint = ray.GetPoint(enter);
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;

            Vector3 newPosition = transform.position + direction.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(new Vector3(newPosition.x, groundY, newPosition.z));
        }
    }

    void IdleWobble()
    {
        Vector3 pos = initialPosition;
        pos.y += Mathf.Sin(Time.time * idleBobSpeed) * idleBobHeight;
        transform.position = pos;
    }
}