using System.Collections;
using UnityEngine;

public class FeatherFalling : MonoBehaviour, ICollectable
{
    [SerializeField] private float gravityMultiplierSubtract = 0.2f;
    [SerializeField] private float timer = 2f;
    
    public void Collect(Player player)
    {
        StartCoroutine(GravityTimer());
        
        Destroy(gameObject);
    }

    private IEnumerator GravityTimer()
    {
        Debug.Log("Feather falling start");
        if(Giroscope.GravityMultiplier - gravityMultiplierSubtract <= 0.1f)
            yield break;

        Giroscope.GravityMultiplier -= gravityMultiplierSubtract;
        yield return new WaitForSeconds(timer);
        Giroscope.GravityMultiplier += gravityMultiplierSubtract;
        Debug.Log("Feather falling stop");
    }
}
