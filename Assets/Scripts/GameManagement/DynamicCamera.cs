using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    public class DynamicCamera : MonoBehaviour
    {
        public List<Transform> Players;
        
        public Vector3 _rotationCam;
        public Vector3 _offset;

        public float _zoomSpeed = 5.0f;

        public float _maxSize = 60f;
        public float _minSize = 20f;

        private Camera _cam;
        
        void Start()
        {
            _cam = GetComponent<Camera>();
        }

        void Update()
        {
            Vector3 averagePosition = Vector3.zero;
            foreach (Transform obj in Players)
            {
                averagePosition += obj.position;
            }

            averagePosition /= Players.Count;

            float maxDistance = 0f;
            foreach (Transform obj in Players)
            {
                float distance = Vector3.Distance(obj.position, averagePosition);
                maxDistance = Mathf.Max(maxDistance, distance);
            }

            float targetSize = Mathf.Clamp(maxDistance * 1.5f, _minSize, _maxSize);
            _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize, targetSize, Time.deltaTime * _zoomSpeed);

            transform.position = averagePosition + _offset;

            transform.rotation = Quaternion.Euler(_rotationCam);
        
        }
    }
}