using System;
using UnityEngine;

namespace UserControlSystem
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] 
        private float _panSpeed = 20f;
        [SerializeField]
        private float _panBorderThickness = 10f;
        [SerializeField] 
        private Vector2 _panLimit;
        
        [SerializeField]
        private float _scrollSpeed = 20f;
        [SerializeField]
        private float _minY = 20f;
        [SerializeField]
        private float _maxY = 120f;

        private void Update()
        {
            var position = transform.position;

            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - _panBorderThickness)
            {
                position.z += _panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= _panBorderThickness)
            {
                position.z -= _panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - _panBorderThickness)
            {
                position.x += _panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= _panBorderThickness)
            {
                position.x -= _panSpeed * Time.deltaTime;
            }

            var scrollAxis = Input.GetAxis("Mouse ScrollWheel");
            position.y -= scrollAxis * _scrollSpeed * 100f * Time.deltaTime;

            position.x = Mathf.Clamp(position.x, -_panLimit.x, _panLimit.x);
            position.y = Mathf.Clamp(position.y, _minY, _maxY);
            position.z = Mathf.Clamp(position.z, -_panLimit.y, _panLimit.y);

            transform.position = position;
        }
    }
}