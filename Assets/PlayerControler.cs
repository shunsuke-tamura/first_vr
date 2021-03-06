using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;
    private CharacterController controller;
    private Vector3 startPos;

    public GameObject cameraC;
    private Vector3 moveDir = Vector3.zero;
    private float gravity = 9.8f;
    private float moveH;
    private float moveV;
    private float rotateY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPos = transform.position;
    }

    void Update()
    {
        rotateY = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick).x;
        transform.rotation = transform.rotation * Quaternion.Euler(0, rotateY, 0);

        moveH = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x;
        moveV = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y;

        Vector3 desiredMove = cameraC.transform.forward * moveV + cameraC.transform.right * moveH;
        moveDir.x = desiredMove.x * 3f;
        moveDir.z = desiredMove.z * 3f;
        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime * speed);
    }

    void OnTriggerStay(Collider other)
    {
        controller.enabled = false;
        transform.position = startPos;
        transform.rotation = Quaternion.identity;
        controller.enabled = true;
    }
}
