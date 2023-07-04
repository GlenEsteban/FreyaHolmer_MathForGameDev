/*
Exercise 4 World-to-Local Space:
    In this exercise, there are two objects, a parent object and its child. The parent object is used to
hold the child object and to define a point in world space. The objective of this exercise is to define 
a point in world space then calculate that point in local space. Using the child object, which follows 
the local space of the object, set the local space position of the child to the calculated world-to-local
point. After doing so, the child object would seem to be fixed at that point in world space despite moving
or rotating the parent object.

Note:
    There already exists a function that converts a point from world space to local space. This function 
takes in a vector (world space point) as the argument and returns a vector (local space conversion). In 
this exercise, you will do this manually via vector math and not use the function below.

    transform.InverseTransformPoint(pointWorldSpace);
*/
using UnityEngine;

public class WorldToLocalSpace : MonoBehaviour
{
    [Tooltip("Coordinates of point in world space. This will be the" + 
    "point that is converted to local space.")]
    [SerializeField] Vector2 pointWorldSpace;
    [SerializeField] GameObject objChild;
    private void OnDrawGizmos() {
        // Set base vectors and position as Vector2
        Vector2 objRight = transform.right;
        Vector2 objUp = transform.up;
        Vector2 objPosition = transform.position;

        // Draw visuals for world space and local space of object
        DrawBaseVectors(Vector2.zero, Vector2.up, Vector2.right);
        DrawBaseVectors(transform.position, transform.up, transform.right);

        // Find point-from-object offset via difference vector between object position and world space point
        Vector2 pointFromObjOffset = pointWorldSpace - objPosition;

        // Find dot product of obj base vectors and offset to get local space coordinates
        float x = Vector2.Dot(pointFromObjOffset, objRight);
        float y = Vector2.Dot(pointFromObjOffset, objUp);
        Vector2 pointLocalSpace = new Vector2 (x, y);

        // Set the objChild local space coordinates to the local space point
        objChild.transform.localPosition = pointLocalSpace;
    }

    // Draw base vectors for any transform
    void DrawBaseVectors(Vector2 position, Vector2 up, Vector2 right){
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, right);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, up);
    }
}
