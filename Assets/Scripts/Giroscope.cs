using UnityEngine;

public class Giroscope : MonoBehaviour
{
    public static float GravityMultiplier = 1;
    [SerializeField] private Vector2 gravityPower = new Vector2(2,2);
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
        Input.gyro.enabled = true;
    }

    void Update()
    {
        Vector2 g = Input.gyro.gravity;
        
        g.y = Mathf.Clamp(g.y, -1, 0);
        g.y = Utils.Remap(g.y, -1, 0, -1, 1);
        
        g.x = Mathf.Clamp(g.x, -0.5f, 0.5f);
        g.x = Utils.Remap(g.x, -0.5f, 0.5f, -1, 1);
        
        Physics2D.gravity = g * gravityPower * GravityMultiplier;
    }
}
