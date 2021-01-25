using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour
{
    private PolygonCollider2D pc;
    private Renderer renderer;
    private SpriteRenderer srend;
    private float delay = .2f;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        pc = GetComponent<PolygonCollider2D>();
        renderer = gameObject.GetComponent<Renderer>();
        srend = gameObject.GetComponent<SpriteRenderer>();

        pc.enabled = false;
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() 
    {
        Debug.Log("Attack!");
        StartCoroutine(EnabledSword());
        yield return new WaitForSeconds(delay);
        DisableSword();
    }

    /*
    void Kill()
    {
        delay = Random.Range(1, 8)* .1f;
               
        srend.color= new Color(
      Random.Range(0f, 1f),
      Random.Range(0f, 1f),
      Random.Range(0f, 1f)
  );
        
    }
    */

    IEnumerator EnabledSword() 
    {
        pc.enabled = true;
        renderer.enabled = true;
        yield return new WaitForSeconds(.05f);
        pc.enabled = false;

    }

    void DisableSword() 
    {
        pc.enabled = false;
        renderer.enabled = false;
    }
}
