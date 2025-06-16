using UnityEngine;

public class AccelTest : MonoBehaviour
{
    void Update()
    {
        Vector3 accel = Input.acceleration;
        //Debug.Log($"Accel X:{accel.x} Y:{accel.y} Z:{accel.z}");
    }
}