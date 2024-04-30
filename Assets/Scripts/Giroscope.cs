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
        //initialGyro = Input.gyro.gravity;
        
        Debug.Log(initialGyro);
    }

    void Update()
    {
        // Quaternion q = GyroToUnity(Input.gyro.attitude);
        // Vector2 g = new Vector2(q.y + gravityOffset.x, -q.x + gravityOffset.y);
        
        Vector2 g = Input.gyro.gravity;
        
        g += gravityOffset;

        if (g.y >= -1)
        {
            g.y = Mathf.Abs(g.y);
        }
        else
        {
            g.y += 1;
        }

        //g += (Vector2)initialGyro;
        
        Physics2D.gravity = g * gravityPower * GravityMultiplier;
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
