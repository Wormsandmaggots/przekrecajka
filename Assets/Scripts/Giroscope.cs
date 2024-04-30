using UnityEngine;

public class Giroscope : MonoBehaviour
{
    public static float GravityMultiplier = 1;
    [SerializeField] private Vector2 gravityPower = new Vector2(2,2);
    [SerializeField] private Vector2 gravityOffset = new Vector2(0, 0);
    
    void Awake()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        // Quaternion q = GyroToUnity(Input.gyro.attitude);
        // Vector2 g = new Vector2(q.y + gravityOffset.x, -q.x + gravityOffset.y);

        Vector2 g = new Vector2(Input.gyro.gravity.x, Input.gyro.gravity.y);
        
        Physics2D.gravity = g * gravityPower * GravityMultiplier;
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
