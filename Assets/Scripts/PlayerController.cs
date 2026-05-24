using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public GameObject laserPrefab;
    public Transform firePoint;

    private Camera cam;
    private SpriteRenderer sr;



    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 move = Vector2.zero;

        // A
        if (Keyboard.current.aKey.isPressed)
            move.x = -1;

        // D
        if (Keyboard.current.dKey.isPressed)
            move.x = 1;

        // W
        if (Keyboard.current.wKey.isPressed)
            move.y = 1;

        // S
        if (Keyboard.current.sKey.isPressed)
            move.y = -1;

        // Di chuyển
        transform.position +=
            (Vector3)(move.normalized * speed * Time.deltaTime);

        LimitScreen();

        if (Keyboard.current.spaceKey.wasPressedThisFrame || 
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(
                laserPrefab,
                firePoint.position,
                Quaternion.identity
            );
        }
    }

    void LimitScreen()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // nửa kích thước player
        float playerHalfWidth =
            sr.bounds.extents.x;

        float playerHalfHeight =
            sr.bounds.extents.y;

        float x = Mathf.Clamp(
            transform.position.x,
            -halfWidth + playerHalfWidth,
            halfWidth - playerHalfWidth
        );

        float y = Mathf.Clamp(
            transform.position.y,
            -halfHeight + playerHalfHeight,
            halfHeight - playerHalfHeight
        );

        transform.position =
            new Vector3(x, y, 0);
    }
}