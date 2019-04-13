using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformPlayer : MonoBehaviour
{
    public float speed = 200f;
    public float jumpForce = 13.0f;
    private Rigidbody2D _body;
    private Animator _ani;
    private BoxCollider2D _box;
    // Use this for initialization
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _ani = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = 0;
        deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(max.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;

        if (hit != null)
        {
            grounded = true;
        }

         
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _body.gravityScale = 1.4f;
              
            if (grounded)
            {
                _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            }
        }


        MovingPlatformController platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatformController>();
        }


        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }
       

        _ani.SetFloat("speed", Mathf.Abs(deltaX));

        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if (deltaX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }

      

    }
}
