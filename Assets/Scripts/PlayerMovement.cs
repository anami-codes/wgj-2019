using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool canMove;
    public static List<string> inventory;
    public Animator anim;
    public float speed = 1;
    private Vector3 target;
    private SpriteRenderer sprite;
    private ContactFilter2D filter2D;

    public LayerMask layerInter;
    public LayerMask layerGround;

    void Start()
    {
        target = transform.position;
        inventory = new List<string>();
        sprite = anim.GetComponent<SpriteRenderer>();


        filter2D.SetLayerMask(layerInter);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && canMove) {
            RaycastHit2D hitInteractibles = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100f, layerInter.value);
            
            if (hitInteractibles.collider != null) {
                if (hitInteractibles.transform.CompareTag("Item")) {
                    if (!hitInteractibles.transform.GetComponent<ItemManager>().UseItem(transform.position)) {
                        target = hitInteractibles.transform.position;
                        anim.SetBool("isWalking", true);
                        if (target.x < transform.position.x) {
                            sprite.flipX = true;
                        } else {
                            sprite.flipX = false;
                        }
                    }
                } else if (hitInteractibles.transform.CompareTag("Interactable")) {
                    hitInteractibles.transform.GetComponent<DialogManager>().StartDialog();
                }
            }

            RaycastHit2D hitGround = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100f, layerGround.value);
            if (hitGround.collider != null) {
                if (hitGround.transform.CompareTag("Ground")) {
                    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = transform.position.z;
                    anim.SetBool("isWalking", true);
                    if (target.x < transform.position.x) {
                        sprite.flipX = true;
                    } else {
                        sprite.flipX = false;
                    }
                } 
            }
        }

        if (transform.position == target) {
            anim.SetBool("isWalking", false);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.name == "STOP") {
            Debug.Log("Colliding");
            target = transform.position;
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        
    }
}
