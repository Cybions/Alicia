using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontal, vertical, forward, left;
    public float mouseSensitivity = 3f, speedReduction;
    public Camera cam;
    public Transform arms;
    public bool isMoving = false;
    public bool isSprinting = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(GetSpeed());
    }
    void Update()
    {
        horizontal += Input.GetAxis("Mouse X") * mouseSensitivity;
        vertical -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        vertical = Mathf.Clamp(vertical, -80, 80);
        transform.localRotation = Quaternion.Euler(0, horizontal, 0);
        cam.transform.localRotation = Quaternion.Euler(vertical, 0, 0);
        arms.transform.localRotation = Quaternion.Euler(vertical, 0, 0);
        forward = Input.GetAxis("Horizontal");
        left = Input.GetAxis("Vertical");
        float currentSpeedReduction = speedReduction;
        if (Input.GetKey(KeyCode.LeftShift)) { currentSpeedReduction = currentSpeedReduction / 2; isSprinting = true; } else { isSprinting = false; }
        transform.Translate(new Vector3(forward / currentSpeedReduction, 0, left / currentSpeedReduction));
        if(forward == 0 && left == 0) { isMoving = false; } else { isMoving = true; }
    }

    IEnumerator GetSpeed()
    {
        while (true)
        {
            Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(1.0f);
            float distance = Vector3.Distance(origin, transform.position);
        }
    }

}