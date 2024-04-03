using UnityEngine;

public class Giroscope : MonoBehaviour
{
    public static float GravityMultiplier = 1;
    [SerializeField] private float gravityPower = 2;
    
    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        Physics2D.gravity = Input.gyro.gravity * gravityPower * GravityMultiplier;
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
