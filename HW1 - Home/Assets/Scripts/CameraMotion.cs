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
    private float _minX, _minZ, _maxX, _maxZ;
    
    // Start is called before the first frame update
    void Start()
    {
        _speed = 0f;
        _rotationAngle = 0f;
        _characterController = GetComponent<CharacterController>();
        _minX = Terrain.activeTerrain.terrainData.bounds.min.x;
        _minZ = Terrain.activeTerrain.terrainData.bounds.min.z;
        _maxX = Terrain.activeTerrain.terrainData.bounds.min.x + Terrain.activeTerrain.terrainData.size.x;
        _maxZ = Terrain.activeTerrain.terrainData.bounds.min.x + Terrain.activeTerrain.terrainData.size.z;
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
        Vector3 point = transform.forward * Time.deltaTime * _speed;
        if (transform.position.x <= _minX || transform.position.x >= _maxX || transform.position.z <= _minZ || transform.position.z >= _maxZ)
            point.y = 0;
        else
        {//update height to terrain height in point (position.x,position.z)
            Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
           point.y = 1 + Terrain.activeTerrain.SampleHeight(pos) - transform.position.y;///delta in y direction
        }
            transform.Translate(point);
        Vector3 direcation = transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        //_characterController.Move(direcation);
    }
}
