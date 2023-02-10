using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyKnockback : MonoBehaviour
{
    [SerializeField]
    private float _knockbackForce = 15f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        { 
            Vector3 knockbackDirection = collision.transform.position - transform.position;
            if (knockbackDirection.x >= 0f)
            {
                knockbackDirection = new Vector3(2f, 1f, 0f);
            }
            else
            {
                knockbackDirection = new Vector3(-2f, 1f, 0f);
            }
            knockbackDirection.Normalize();
            knockbackDirection *= _knockbackForce;

            Rigidbody2D rb = collision.transform.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddForce(knockbackDirection, ForceMode2D.Impulse);
        }
    }
}