using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float maxSpeed = 3;
    [SerializeField] private Rigidbody rigidB;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform bottom;
    [SerializeField] private float bottomRotation;

    private void Start()
    {
        Cursor.visible = false;
    }


    private void Update()
    {
        /*
        Vector3 mouseInput = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * mouseSensitivity;
        Vector3 bodyEuler = this.transform.eulerAngles;
        this.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(bodyEuler.x, bodyEuler.y + mouseInput.y, bodyEuler.z));
        */

        Vector3 movDir2 = new Vector3(0, 0, 0);
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            movDir2 += this.transform.right;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movDir2 += -this.transform.right;
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            movDir2 += this.transform.forward;
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            movDir2 += -this.transform.forward;
        }
        movDir2.Normalize();

       

        if (movDir2 != new Vector3(0,0,0))
        {
            Vector3 normalOfMoveDir2 = new Vector3(movDir2.z, movDir2.y, -movDir2.x);
            // bottom.Rotate(normalOfMoveDir2 * Time.deltaTime);
            //normalOfMoveDir2 = Quaternion.AngleAxis(90, Vector3.up) * movDir2;
            bottom.RotateAround(normalOfMoveDir2, bottomRotation);
           // bottom.RotateAroundLocal(movDir2, 0.01f);

            rigidB.velocity = movDir2 * movementSpeed;
        }
        else
        {
            rigidB.velocity = new Vector3(0,0,0);
        }
        

       

    }


}
