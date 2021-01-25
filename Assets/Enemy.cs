using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject player;
   // public Enemy enemy;
    public GameObject weapon;
    public float speed = 5.0f;
    public int health = 20;
    public int id;
    private Rigidbody2D rb;
    private int IFrameTimer = 0;
    private int IFrameDuration = 10;
    private int StunTimer = 0;
    private int StunDuration = 80;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        //weapon moves with Enemy
        weapon.transform.SetParent(gameObject.transform);
        //enemy is alive
        gameObject.SetActive(true);
        //full health
        this.health = 20;
        Debug.Log("Enemy health: " + health);
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 80;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        RotationUpdate();
        MovementUpdate();
        if (IFrameTimer > 0)
        {
            IFrameTimer--;

        }
        
    }

    void SetTarget(Transform ntarget)
    {
        target = ntarget;
    }

    void SetPlayer(GameObject nplayer)
    {
        player = nplayer;
       // Debug.Log("setPlayer");
    }

    void MovementUpdate()
    {
        //Debug.Log(StunTimer);
        if (StunTimer > 0)
        {
            StunTimer--;

        }
        else
        {

            //Enemy towards player
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }

    }

    void RotationUpdate()
    {

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan(dir.y / dir.x);
        if (dir.x > 0)
        {
            angle += 180;
        }



        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            (angle + 90)
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("PWeapon")  )
        {
            if (IFrameTimer <= 0)
            {
                //Debug.Log("Enemy attacked by player's weapon");
                //If the enemy's health is above 0, the enemy loses health when it hits the player's weapon
                if (health > 0)
                {
                    float knockbackMult = 1f;
                    int damage = 5;
                    if (other.gameObject.tag.Contains("Green"))
                    {
                        knockbackMult = 2f;
                        damage = 6;
                    }
                    if (other.gameObject.tag.Contains("Blue"))
                    {
                        StunTimer = StunDuration;
                        //Debug.Log("E Stunned");
                        knockbackMult = 0f;
                    }
                    if (other.gameObject.tag.Contains("Red"))
                    {
                        knockbackMult = .2f;
                        damage = 4;
                    }

                    health -= damage;
                    //Debug.Log("Enemy hit. Enemy health: " + health);
                    Vector3 dir = other.transform.parent.position - transform.position;
                    dir = new Vector3(dir.x, dir.y, 0);
                    dir = Vector3.Normalize(dir);
                    Vector3 knockbackDir = (dir * -6000f * knockbackMult);
                    rb.AddForce(knockbackDir);
                    IFrameTimer = IFrameDuration;
                    

                }
                //If the enemy's health is 0 or below, the enemy dies (deactivates)
                else
                {
                    gameObject.SetActive(false);
                    player.SendMessage("Kill", id);  //PlayerControl kill()
                }
            }
        }
               
    }
    
}
