using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SlimeDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovementRB>())
        {


            //Debug.Log(gameObject.name + " hit Player");
        }
    }
}