using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed; // Velocidad de la bala

    private Rigidbody2D Rigidbody2D; // Llamado al componente Rigid
    private Vector2 Direction; // Dirección de la bala.

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JhonMovement jhon = collision.GetComponent<JhonMovement>();
        GruntScript grunt = collision.GetComponent<GruntScript>();
        if(jhon != null)
        {
            jhon.Hit();
        }
        if (grunt != null)
        {
            grunt.Hit();
        }
        DestroyBullet();
    }
}
