using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarBehavior : MonoBehaviour
{

    public GameObject Player;
    private SpriteRenderer sprRend;
    private Vector2 size;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = gameObject.GetComponent<SpriteRenderer>();
        size = sprRend.size;
    }

    // Update is called once per frame
    void Update()
    {
        int hp = Player.GetComponent<PlayerControl>().health;
        sprRend.size = new Vector2(size.x * hp / 100f, size.y);
    }
}
