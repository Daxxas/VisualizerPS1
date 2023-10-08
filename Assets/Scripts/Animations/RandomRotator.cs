using System;
using UnityEngine;

namespace Animations
{
    public class RandomRotator : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 90f;
        
        private void Update()
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}