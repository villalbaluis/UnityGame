using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonMovement : MonoBehaviour
{
    public float Speed; // Velocidad de Jhon
    public float JumpForce; // Fuerza aplicada al salto
    public GameObject BulletPrefab; // Llamado de la bala.

    private Rigidbody2D Rigidbody2D; // Llamado al componente Rigid
    private Animator Animator; // Llamado al componente Animator
    
    // Variables de uso solo en clase
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Obtención de datos de los componentes.
        Rigidbody2D = GetComponent<Rigidbody2D>(); 
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); // Obtención de movimiento horizontal del eje X.

        // Validación de movimiento en dirección, para cambio de forma de vista de Jhon.
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f); // Variable del animator en Unity para identificar el movimiento.

        // Debug.DrawRay(transform.position, Vector3.down, 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        // Validación para ejecución de función de salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Validación de pulsación para tecla de disparo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        // Al invocar la función, se agrega la "Fuerza" hacia arriba al componente.
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction  = Vector3.right;
        else direction = Vector3.left;

        // Al invocar la función, se modifica el prefab de Bullet para disparar.
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
