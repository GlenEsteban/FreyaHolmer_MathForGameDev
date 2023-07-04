/*
Exercise 2 Look Trigger :
    In this exercise, there are two objects, the trigger and object. The objective of this exercise is to
have the trigger check if it is looking at the object by using the Dot product and a look threshold. 
The threshold would be range from 0 to 1, where 0 means that the object can be looked at if it is directly
to the right or left of the trigger while 1 means that the look range is very precise and requires the 
trigger to look directly at the object.
*/

using UnityEngine;
public class LookTrigger : MonoBehaviour {
    [SerializeField] GameObject obj;
    [SerializeField] [Range(0f, 1f)] float lookThreshold = .5f;
    void OnDrawGizmos() {
        // Calculate object's look direction and the direction towards enemy
        Vector3 lookDirection = transform.right;
        Vector3 directionToEnemy = (obj.transform.position - transform.position).normalized;
        
        // Create a bool that depends on the dot product of current direction and the direction to enemy
        bool isLookingAtObj = Vector3.Dot(lookDirection, directionToEnemy) > lookThreshold;

        // Based on the bool, draw visuals for current look direction and the direction to enemy
        Gizmos.color = isLookingAtObj ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, (transform.position + lookDirection));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (transform.position + directionToEnemy)); 
    }
}
