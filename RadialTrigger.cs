/*
Exercise 1 Radial Trigger :
    In this exercise, there are two objects, the trigger and object. The objective of this exercise is to
create a radial trigger for the trigger object. The radial trigger checks if the object is inside the 
radial trigger by using only vector math.
*/

using UnityEngine;
#if UNITY_EDITOR // Compiles out Unity Editor since it is not compatible when building
using UnityEditor; // Used to create Handles
#endif

public class RadialTrigger : MonoBehaviour
{
    [SerializeField] float triggerRadius = 5f;
    [SerializeField] Transform obj;

    #if UNITY_EDITOR
    private void OnDrawGizmos() {
        // Calcuate distance between trigger and object
        Vector2 dispVec = obj.transform.position - transform.position;
        float distance = Mathf.Sqrt(Mathf.Pow(dispVec.x, 2) + Mathf.Pow(dispVec.y, 2)); 

        // Update visuals based on whether the obj is inside or outside the trigger
        bool isInTrigger = distance < triggerRadius;
        Handles.color = isInTrigger ? Color.green : Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, triggerRadius);

        /* 
        NOTE: 
        Mathf.Sqrt() and Mathf.Pow() can be a heavy and expensive operations. 
        Instead of using Mathf.Sqrt(), leave out the square root operation and compare 
        the squared distance with the square of the trigger's radius. Instead of using 
        Mathf.Pow(), simply multiply the variable or value by itself.

        OPTIMIZED CODE:
        Vector2 dispVec = obj.transform.position - transform.position;
        float squaredDistance = (dispVec.x * dispVec.x) + (dispVec.y * dispVec.y); 
        bool isInTrigger = squaredDistance < (triggerRadius * triggerRadius);
        Handles.color = isInTrigger ? Color.green : Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, triggerRadius);
        */
    }
    #endif
}
