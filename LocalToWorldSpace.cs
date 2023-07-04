/*
Exercise 3 Local-to-World Space:
    In this exercise, there is one object which is used to create a local space and to define a point in 
local space. The objective of this exercise is to define a point in local space and calculate that point
in world space. Since Gizmos only draws in world space, this local-to-world space calculation is 
necessary. After drawing this point, it should follow the object's local space.

Note:
    There already exists a function that converts a point from local space to world space. This function 
takes in a vector (local space point) as the argument and returns a vector (world space conversion). In 
this exercise, you will do this manually via vector math and not use the function below.

    transform.TransformPoint(pointLocalSpace);
*/
using UnityEngine;

public class LocalToWorldSpace : MonoBehaviour
{
    [Tooltip("Coordinates of point on object's local space." + 
    "This will be the point that is converted to world space.")]
    [SerializeField] Vector2 pointLocalSpace;
    private void OnDrawGizmos() {
        // Set base vectors and position as Vector2
        Vector2 objRight = transform.right;
        Vector2 objUp = transform.up;
        Vector2 objPosition = transform.position;

        // Draw world space and local space of object
        DrawBaseVectors(Vector2.zero, Vector2.up, Vector2.right);
        DrawBaseVectors(objPosition, objUp, objRight);

        // Multiply base vectors by the local space coordinates to get point-from-object offset
        Vector2 pointFromObjOffset = (objRight * pointLocalSpace.x) + (objUp * pointLocalSpace.y);

        // Add the object's position to the offset to get the world space coordinates
        Vector2 pointWorldSpace = pointFromObjOffset + objPosition;

        // Draw local space point
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointWorldSpace, .1f);
    }
    
    // Draws base vectors for any transform
    void DrawBaseVectors(Vector2 position, Vector2 up, Vector2 right){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, up);
    }
}
