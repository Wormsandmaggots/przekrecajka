using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacgroudnImage : MonoBehaviour
{
    [SerializeField] private Vector2 direction = new Vector2(1, 1);
    [SerializeField] private Vector2 speed = new Vector2(1, 1);

    private void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.position += (Vector3)movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("platforms"))
        {
            Vector2 point = other.ClosestPoint(transform.position);
            Vector2 dir = point.normalized;

            RaycastHit2D xHit = Physics2D.Raycast(point, new Vector2(dir.x, 0), 0.1f,
                Settings.platforms);
            RaycastHit2D yHit = Physics2D.Raycast(point, new Vector2(0, dir.y), 0.1f,
                Settings.platforms);

            if (xHit.transform && !yHit.transform)
            {
                direction.x = -direction.x;
            }

            if (yHit.transform && !xHit.transform)
            {
                direction.y = -direction.y;
            }
        }
    }
}
