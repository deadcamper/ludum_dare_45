using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpMovement : MonoBehaviour
{
    bool canJump = true;

    public Rigidbody2D objRigidbody2D;
    public float speed = 10;

    private float lastJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        objRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastJump + 1f < Time.time)
        {
            canJump = true;
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            objRigidbody2D.velocity += new Vector2(0, speed);
            canJump = false;

            lastJump = Time.time;
        }
    }

    void OnCollisionStay2D(Collision2D col2D)
    {
        if (!canJump)
        {
            ContactPoint2D contact = col2D.GetContact(0);
            if (Vector2.Dot(contact.normal, Vector3.up) > 0.5)
            {
                canJump = true;
            }
        }
    }
}
