using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    
    private float delay = .2f;
    public float[] delays;
    private bool Attacking=false;
    public class Weapon 
    {
        public GameObject obj;
        public Renderer rend;
        public PolygonCollider2D pc;
        public AudioSource sound;

        public Weapon(GameObject obj)
        {
            obj = obj;
            rend = obj.GetComponent<Renderer>();
            pc = obj.GetComponent<PolygonCollider2D>();
            sound = obj.GetComponent<AudioSource>();
        }
    }

    public GameObject[] weaponObjects;
    private Weapon[] weapons;

   
    

    private Weapon currWeapon;

    // Start is called before the first frame update
    void Start()
    {

        weapons = new Weapon[weaponObjects.Length];
        for(int i=0; i< weapons.Length; i++)
        {
            weapons[i] = new Weapon(weaponObjects[i]);
        }
        currWeapon = weapons[0];
        DisableWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !Attacking)
        { 
            StartCoroutine(Attack());
        }

       /* if (Input.GetKeyDown(KeyCode.Alpha1)) {
            swapWeapon(weapon1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            swapWeapon(weapon2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            swapWeapon(weapon3);
        }*/
    }

    void Kill(int id)
    {
        /*switch (id)
        {
            case 1:
                swapWeapon(weapon1);
                break;
            case 2:
                swapWeapon(weapon2);
                break;
            case 3:
                swapWeapon(weapon3);
                break;
            default:
                break;
        }*/

        swapWeapon(weapons[id]);
        delay = delays[id];
    }

    void swapWeapon(Weapon w) {
        DisableWeapon();
        currWeapon = w;
        //StartCoroutine(Attack());
    }
    IEnumerator Attack() 
    {
        Debug.Log("Attack!");
        EnabledWeapon();
        currWeapon.sound.Play();
        yield return new WaitForSeconds(delay);
        DisableWeapon();
    }
    void EnabledWeapon() 
    {
        Attacking = true;
        currWeapon.pc.enabled = true;
        currWeapon.rend.enabled = true;
    }

    void DisableWeapon() 
    {
        currWeapon.pc.enabled = false;
        currWeapon.rend.enabled = false;
        Attacking = false;
    }
}
