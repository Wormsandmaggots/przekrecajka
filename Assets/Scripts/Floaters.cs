using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floaters : MonoBehaviour
{
    [SerializeField] private Vector2 speedLimit = new Vector2(5,5);
    private SpriteRenderer spriteRenderer;
    private SpriteMask spriteMask;
    private Vector2 direction;
    private Vector2 speed;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMask = GetComponent<SpriteMask>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction = direction.normalized;
        speed = new Vector2(Random.Range(1, speedLimit.x), Random.Range(1, speedLimit.y));
    }

    private void Update()
    {
        spriteMask.sprite = spriteRenderer.sprite;

        Vector2 m = direction * speed * Time.deltaTime;
        
        transform.position += new Vector3(m.x + Mathf.Sin(Time.deltaTime), m.y + Mathf.Sin(Time.deltaTime), 0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("bubbleHolder"))
        {
            Vector2 point = other.ClosestPoint(transform.position);
            
            RaycastHit2D xHit = Physics2D.Raycast(point, new Vector2(-direction.x, 0), 1f,
                Settings.platforms);
            RaycastHit2D yHit = Physics2D.Raycast(point, new Vector2(0, -direction.y), 1f,
                Settings.platforms);

            if (xHit)
            {
                direction.x = -direction.x;
            }

            if (yHit)
            {
                direction.y = -direction.y;
            }
        }
    }
}
