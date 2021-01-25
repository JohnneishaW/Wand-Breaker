using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private SpriteRenderer srend;
    public float speed = 5f;
    public float[] weaponSpeed;
    public GameObject weaponManager;
    public GameObject spawningManager;
    private Vector3 knockbackDir;
    private bool inWall;
    private int IFrameTimer=0;
    private int IFrameDuration = 40;
    private Color defaultCol;
    public int defaultDrag;
    public int slowDrag;
    public int defaultMass;
    public int bigMass;
    public int health;
    public int MaxHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        srend = gameObject.GetComponent<SpriteRenderer>();
        defaultCol = srend.color;
        rb.drag = defaultDrag;
        rb.angularDrag = 10000;
        rb.mass = defaultMass;
        health = MaxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementUpdate();
        RotationUpdate();
        IFrameUpdate();
    }

    void MovementUpdate() {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        /*
        float horizontalChange = horizontalInput *speed*Time.deltaTime;
        float verticalChange = verticalInput*speed*Time.deltaTime;
        
        transform.position += new Vector3(horizontalChange, verticalChange, 0);
        */
        Vector3 input = new Vector3(horizontalInput, verticalInput, 0);

        
        transform.position += input.normalized * speed * Time.deltaTime;

    }

    void RotationUpdate()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = worldPosition - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x );
        if(dir.x > 0)
        {
            angle += 180;
        }
        
        

        transform.eulerAngles = new Vector3(
    transform.eulerAngles.x,
    transform.eulerAngles.y,
    (angle+90)
    ); 

    }

    void IFrameUpdate()
    {
        if (IFrameTimer > 0) { 
            IFrameTimer--; 
            if((IFrameTimer/5) % 2 == ((IFrameDuration-1)/5) % 2)
            {
                srend.color = Color.red;
            }
            else
            {
                srend.color = defaultCol;
            }
            if (IFrameTimer == 0)
            {
                srend.color = defaultCol;
            }
        }
    }
    
    void Kill(int id)
    {
        if (id == 1)
        {
            rb.drag = slowDrag;
            rb.mass = bigMass;
        }
        else
        {
            rb.drag = defaultDrag;
            rb.mass = defaultMass;
        }
        
        speed = weaponSpeed[id - 1];
        //Debug.Log("speed is"+ speed);
        weaponManager.SendMessage("Kill", id); //PlayerWeaponManager kill()
        spawningManager.SendMessage("EnemyKilled");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EWeapon" || other.tag == "Enemy")
        {
            //Debug.Log("pATTACK");
            if (IFrameTimer <= 0)
            {


                Transform oTransform = other.transform;
                if(other.tag == "EWeapon")
                {
                    oTransform = oTransform.parent;
                }

                health -= oTransform.gameObject.GetComponent<Enemy>().damage;
                if(health < 0) 
                {
                    health = 0;
                }
                Debug.Log("phealth is " + health);

                Vector3 dir = oTransform.position - transform.position;
                dir = new Vector3(dir.x, dir.y, 0);
                dir = Vector3.Normalize(dir);
                knockbackDir = (dir * -6000f);
                rb.AddForce(knockbackDir);
                //Debug.Log(knockbackDir);
                IFrameTimer = IFrameDuration;
             
            }

        }
        if (other.tag == "Wall")
        {

            inWall = true;
           // Debug.Log("in wall");
        }

    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "EWeapon" || other.tag =="Enemy")
        {
           // Debug.Log("pUN-ATTACK");
            
        }
        if (other.tag == "Wall")
        {

            inWall = false;
            //Debug.Log("not in wall");
        }
    }

    /*void onCollisionEnter2D(Collision collision)
    {
        //if (other.tag == "wall")
        
            inWall = true;
            Debug.Log("in wall");
        
    }
    void onCollisionExit2D(Collision collision)
    {
        //if (other.tag == "wall")
        
            inWall = false;
            Debug.Log("not in wall");
        
    }*/
}
