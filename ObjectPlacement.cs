/*
Exercise 5 Placing Objects:
    In this exercise, there are two objects - the surface (sphere) and the player (capsule). 
The objective is to raycast in the direction of the player's forward direction and create a bounding box for an
object to be placed onto the surface where the raycaast hits. Take into consideration the normal of the surface
when placing the object. The forward direction of the object should be placed in the general direction of the 
player's forward direction.
*/

using UnityEngine;
using UnityEditor; // Accesses Handles for drawing visuals

public class ObjectPlacement : MonoBehaviour {
    [SerializeField] GameObject obj;
    Vector3 headPos;
    Vector3 lookDir;
    Vector3 playerUp;
    Vector3 hitPos;
    Vector3 objUp;
    Vector3 objRight;
    Vector3 objForward;
    Quaternion objRotation;
    Matrix4x4 objToWorld;
    Vector3 worldPt;
    Vector3[] boundingBoxPoints = new Vector3[] {
        new Vector3 (1, 0, 1), // bottom 4 vertices
        new Vector3 (-1, 0, 1),
        new Vector3 (-1, 0, -1),
        new Vector3 (1, 0, -1),
        new Vector3 (1, 2, 1), // top 4 vertices
        new Vector3 (-1, 2, 1),
        new Vector3 (-1, 2, -1),
        new Vector3 (1, 2, -1),
    };
    
    private void OnDrawGizmos() {
        // Declare head position and look direction
        Vector3 headPos = transform.position;
        Vector3 lookDir = transform.forward;

        // Raycast from player look direction (forward direction)
        bool isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

        // Method for drawing visuals for base vectors
        void DrawRay(Vector3 p, Vector3 dir) => Handles.DrawAAPolyLine(p, p + dir);

        // Draw visuals based on if player is looking onto a surface or not
        if (isHit) {
            hitPos = hit.point;
            objUp = hit.normal; 
            // Draw line between player and pointToPlace
            Handles.color = Color.white;
            Handles.DrawAAPolyLine(headPos, hitPos);

            // Draw the normal of surface (object's up direction)
            Handles.color = Color.green;
            DrawRay(hitPos, objUp);

            // Calculate and draw the right base vector (object's right direction)
            if (Vector3.Dot(lookDir, objUp) != -1) { // guards against when the player looks straight onto a surface
                objRight = (Vector3.Cross(objUp , lookDir)).normalized;
                Handles.color = Color.red;
                DrawRay(hitPos, objRight);
            }
            else {
                playerUp = transform.up;
                objRight = (Vector3.Cross(objUp , playerUp)).normalized;
                Handles.color = Color.red;
                DrawRay(hitPos, objRight);
            }

            // Calculate and draw the forward base vector (object's forward direction)
            objForward = Vector3.Cross(objRight, objUp);
            Handles.color = Color.cyan;
            DrawRay(hitPos, objForward);

            // Create a matrix based on the object position and rotation (scale is identity scale)
            objRotation = Quaternion.LookRotation(objForward, objUp);
            objToWorld = Matrix4x4.TRS(hitPos, objRotation, Vector3.one); // Vector3.one is the identity scale
            
            // Convert the bounding box points to world space based on the matrix created
            Gizmos.color = Color.red;
            for(int i = 0; i < boundingBoxPoints.Length; i ++) {
                worldPt = objToWorld.MultiplyPoint3x4(boundingBoxPoints[i]);
                Gizmos.DrawSphere(worldPt, .1f);
            }

            /*  NOTE: You can make Gizmos use the matrix as the default space instead of multiplying the matrix per point. 
                    
                    Gizmos.matrix = objToWorld;
                    for(int i = 0; i < boundingBoxPoints.Length; i ++) {
                        Gizmos.DrawSphere(pts[i], .1f);
                    } 

                NOTE: To reset the Gizmos.matrix, use the code:

                    Gizmos.matrix = Matrix4x4.identity;
                      
            */
        }
        else {
            // Draw the look direction of player if raycast fails to hit
            Handles.color = Color.red;
            DrawRay(headPos, 100 * lookDir); // Ray length scaled by 100 for better visual
        }
    }
}
