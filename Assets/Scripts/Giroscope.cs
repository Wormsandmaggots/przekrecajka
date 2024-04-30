using UnityEngine;

public class Giroscope : MonoBehaviour
{
    public static float GravityMultiplier = 1;
    private static Giroscope gyro = null;
    [SerializeField] private Vector2 gravityPower = new Vector2(2,2);
    [SerializeField] private Vector2 gravityOffset = new Vector2(0, 0);
    private Vector3 initialGyro;
    
    void Awake()
    {
        if (gyro == null)
        {
            gyro = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
        Input.gyro.enabled = true;
        initialGyro = Input.gyro.gravity;
    }

    void Update()
    {
        // Quaternion q = GyroToUnity(Input.gyro.attitude);
        // Vector2 g = new Vector2(q.y + gravityOffset.x, -q.x + gravityOffset.y);
        
        Debug.Log(Input.gyro.gravity);

        Vector2 g = new Vector2(Input.gyro.gravity.x + Mathf.Abs(initialGyro.x) + gravityOffset.x, Input.gyro.gravity.y + Mathf.Abs(initialGyro.y) + gravityOffset.y);
        
        Physics2D.gravity = g * gravityPower * GravityMultiplier;
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
