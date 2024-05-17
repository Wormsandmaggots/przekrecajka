using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Floaters : MonoBehaviour
{
    [SerializeField] private Vector2 speedLimitX = new Vector2(5,5);
    [SerializeField] private Vector2 speedLimitY = new Vector2(5, 5);
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private SpriteMask spriteMask;
    private Vector2 direction;
    private Vector2 speed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMask = GetComponent<SpriteMask>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        direction = direction.normalized;
        speed = new Vector2(Random.Range(speedLimitX.x, speedLimitX.y), Random.Range(speedLimitY.x, speedLimitY.y));

        StartCoroutine(Delay(Random.Range(0f, 2f)));
    }

    private void Update()
    {
        spriteMask.sprite = spriteRenderer.sprite;

        Vector2 m = direction * speed * Time.deltaTime;
        
        transform.position += new Vector3(m.x, m.y, 0);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("bubbleHolder"))
        {
            Vector2 position = transform.position;
            Vector2 point = other.ClosestPoint(position);
            
            RaycastHit2D xHit = Physics2D.Raycast(point, new Vector2(direction.x > 0 ? -1 : 1, 0), 1f,
                Settings.bubbleHolder);
            RaycastHit2D yHit = Physics2D.Raycast(point, new Vector2(0, direction.y > 0 ? -1 : 1), 1f,
                Settings.bubbleHolder);
            
            if (xHit.collider != null)
            {
                direction.x = -direction.x;
            }
            
            if (yHit.collider != null)
            {
                direction.y = -direction.y;
            }
        }
    }

    private IEnumerator Delay(float time)
    {
        animator.speed = 0;

        yield return new WaitForSeconds(time);

        animator.speed = 1;
    }
}
