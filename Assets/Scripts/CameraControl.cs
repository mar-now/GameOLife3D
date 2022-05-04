using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float _defaultMoveSpeed = 0.1f;
    [SerializeField] private float _boostMoveSpeed = 0.5f;
    [SerializeField] private float _defaultZoomSpeed = 0.3f;
    [SerializeField] private float _boostZoomSpeed = 1.2f;
    [SerializeField] private float _mouseWheelMultiplier = 80;
    private Vector3 newPosition;
    [SerializeField] private float _movementTime = 5;

    private void Start()
    {
        if (camera == null)
            camera = gameObject.GetComponent<Camera>();

        newPosition = transform.position;
    }

    void Update()
    {
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        // Camera zooming
        // There's also a hardcoded multiplier so the values look more reasonable compared
        // to the movement speed in the editor.
        // Keyboard zoom-in
        if (Input.GetKey(KeyCode.Space) == true && camera.orthographicSize <= 400)
            camera.orthographicSize += (Input.GetKey(KeyCode.RightShift) ? _boostZoomSpeed : _defaultZoomSpeed) 
                * Time.deltaTime * 10;
        // Mousewheel zoom-in
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f && camera.orthographicSize <= 400)
            camera.orthographicSize += (Input.GetKey(KeyCode.RightShift) ? _boostZoomSpeed : _defaultZoomSpeed) 
                * Time.deltaTime * _mouseWheelMultiplier * 10;
        // Keyboard zoom-out
        else if (Input.GetKey(KeyCode.LeftShift) == true  && camera.orthographicSize >= 10)
            camera.orthographicSize -= (Input.GetKey(KeyCode.RightShift) ? _boostZoomSpeed : _defaultZoomSpeed) * Time.deltaTime * 10;
        // Mousewheel zoom-out
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && camera.orthographicSize >= 10)
            camera.orthographicSize -= (Input.GetKey(KeyCode.RightShift) ? _boostZoomSpeed : _defaultZoomSpeed)
                * Time.deltaTime * _mouseWheelMultiplier * 10;

        // Camera movement
        newPosition += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * 
            (Input.GetKey(KeyCode.RightShift) ? _boostMoveSpeed : _defaultMoveSpeed) * camera.orthographicSize / 50;

        transform.position = Vector3.Lerp(transform.position, newPosition, _movementTime * Time.deltaTime);
    }
}
