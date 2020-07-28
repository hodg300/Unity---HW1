using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMotion : MonoBehaviour
{
    private float _speed;
    private float _angularSpeed = 0.05f;
    private float _rotationAngle;
    private CharacterController _characterController;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _speed = 0f;
        _rotationAngle = 0f;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        if (Input.GetKey(KeyCode.W))
        {
            _speed += 0.01f;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            _speed -= 0.01f;
        }
        
        _rotationAngle += mouse_x * _angularSpeed;
        transform.Rotate(0,_rotationAngle,0);
        Vector3 direcation = transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        _characterController.Move(direcation);
    }
}
