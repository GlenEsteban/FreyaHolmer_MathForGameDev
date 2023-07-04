/*
Exercise 6 Reflecting Off of Surface:
    In this exercise, there are two objects - the surface (terrain) and the ball (red ball). 
The objective is to raycast in the direction of the ball's forward direction and reflect the vector off 
of the surface of the terrain. Create visuals for the raycast of the ball's forward direction and the 
reflected vector.
*/
using UnityEngine;

public class ReflectingVector : MonoBehaviour {
    private void OnDrawGizmos() {
        Vector3 ballPos = transform.position;
        Vector3 forwardDir = transform.forward;

        // Raycast from the ball to the ball's forward direction
        bool isHit = Physics.Raycast(ballPos, forwardDir, out RaycastHit hit);

        if(isHit) {
            // Draw raycast from ball to the contact point on surface
            Gizmos.color = Color.white;
            Gizmos.DrawLine(ballPos, hit.point);

            // Calculate and draw reflected vector
            Vector3 reflectedVector = forwardDir - (2 * Vector3.Dot(forwardDir, hit.normal) * hit.normal);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(hit.point, hit.point + reflectedVector);
        }
        else {
            // Draw ball's forward direction if not hit
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, 100 * transform.forward);
        }
    }
}
