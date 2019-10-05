using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WASDMovement : MonoBehaviour
{
    public Rigidbody2D objRigidbody2D;
    public float speed = 8;
    public float impulse = 70;

    // Start is called before the first frame update
    void Start()
    {
        objRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (Mathf.Abs(x) > float.Epsilon)
        {
            objRigidbody2D.AddForce(new Vector2(x, 0) * impulse);
            objRigidbody2D.velocity = new Vector2(Mathf.Clamp(objRigidbody2D.velocity.x, -speed, speed), objRigidbody2D.velocity.y);
        }

        if (Mathf.Abs(y) > float.Epsilon)
        {
            objRigidbody2D.AddForce(new Vector2(0, y) * impulse);
            objRigidbody2D.velocity = new Vector2(objRigidbody2D.velocity.x, Mathf.Clamp(objRigidbody2D.velocity.y, -speed, speed));

        }
    }
}
