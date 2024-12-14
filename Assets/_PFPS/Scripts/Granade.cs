using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public class Granade : MonoBehaviour
    {
        public LineRenderer trajectoryLine; // Attach the LineRenderer
        public Transform grenadeSpawnPoint; // Where the grenade will spawn
        public GrenadeBehaviour grenadePrefab; // Grenade prefab
        public float throwForce = 10f; // Initial throw force
        public int trajectoryResolution = 30; // Number of points in the line

        private bool _isAiming = false; // To check if the G key is held

        private void Update()
        {
            // Detect G key press and release
            if (Input.GetKeyDown(KeyCode.G))
            {
                _isAiming = true;
                trajectoryLine.enabled = true; // Show the trajectory line
            }

            if (Input.GetKey(KeyCode.G) && _isAiming)
            {
                DisplayTrajectory(); // Display the trajectory line while holding G
            }

            if (Input.GetKeyUp(KeyCode.G) && _isAiming)
            {
                ThrowGrenade(); // Throw the grenade on G key release
                _isAiming = false;
                trajectoryLine.enabled = false; // Hide the trajectory line
            }
        }

        private void DisplayTrajectory()
        {
            trajectoryLine.enabled = true;

            var trajectoryPoints = CalculateTrajectory(grenadeSpawnPoint.position, grenadeSpawnPoint.forward * throwForce);
            trajectoryLine.positionCount = trajectoryPoints.Length;
            trajectoryLine.SetPositions(trajectoryPoints);
        }

        private Vector3[] CalculateTrajectory(Vector3 startPosition, Vector3 initialVelocity)
        {
            var points = new Vector3[trajectoryResolution];
            var currentPosition = startPosition;
            var currentVelocity = initialVelocity;
            const float timeStep = 0.1f; // Adjust for smoother or coarser lines

            for (int i = 0; i < trajectoryResolution; i++)
            {
                points[i] = currentPosition;

                // Update position and velocity
                currentPosition += currentVelocity * timeStep;
                currentVelocity += Physics.gravity * timeStep; // Gravity effect
            }

            return points;
        }

        private void ThrowGrenade()
        {
            var grenade = Instantiate(grenadePrefab, grenadeSpawnPoint.position, Quaternion.identity);
            // var rb = grenade.GetComponent<Rigidbody>();
            // rb.velocity = grenadeSpawnPoint.forward * throwForce;
            grenade.Move(grenadeSpawnPoint.forward, throwForce);
        }
    }
}
