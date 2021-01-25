using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EWeaponBehavior : MonoBehaviour
{
    private Renderer render;
    private PolygonCollider2D pc;
    public float EnemyAttackCooldown = .5f;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        pc = GetComponent<PolygonCollider2D>();
        render.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           // Debug.Log("ATTACK");
            render.enabled = true;
            pc.enabled = false;
            StartCoroutine(SheathSword());
            StartCoroutine(AttackCooldown());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

        }
    }

    IEnumerator SheathSword()
    {
        yield return new WaitForSeconds(0.2f);
       // Debug.Log("UNATTACK");
        render.enabled = false;
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(EnemyAttackCooldown);
        pc.enabled = true;
    }

}
