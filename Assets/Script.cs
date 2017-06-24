﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Script : MonoBehaviour {
    Rigidbody2D rb2D;
    public Text hpValue;
    float health = 100f;


    public float DamageMultiplier = 4f;
    public float DamageTolerance = 4f;

    private Vector2 lookPos;
    private float lookTo = -1;

    string debuglog;

	// Use this for initialization
	void Start () {
        rb2D = this.GetComponent<Rigidbody2D>();
        hpValue.text = health.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2D.AddForce(new Vector2(0, CrossPlatformInputManager.GetAxis("Vertical") * 1));
        rb2D.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * 1 , 0));
        //Rotation Scripts:
        if(CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0) {
            lookPos = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            lookPos = lookPos.normalized;
            Debug.Log(Mathf.Atan(lookPos.y / lookPos.x) * Mathf.Rad2Deg);
            hpValue.text = ("X: " + lookPos.x + " " + "Y: " + lookPos.y + " " + "Angle: " + Mathf.Atan(lookPos.y / lookPos.x) * Mathf.Rad2Deg).ToString() + "  " + debuglog;
            if (lookPos.y == 1 && lookPos.x == 0) {
                Debug.Log("3");
                if (transform.localScale.x == 1) {
                    rb2D.MoveRotation(-90);
                }
                else {
                    rb2D.MoveRotation(90);
                }
            }
            else if(lookPos.y == -1 && lookPos.x == 0) {
                Debug.Log("4");
                if (transform.localScale.x == 1) {
                    rb2D.MoveRotation(90);
                }
                else {
                    rb2D.MoveRotation(-90);
                }
            }
            else if(lookPos.y == 0) {
                transform.localScale = new Vector3(lookPos.x == 1 ? -1 : 1, 1, 1);
                rb2D.MoveRotation(0);
            }
            else if(lookPos.x > 0) {
                Debug.Log("1");
                debuglog = "1";
                transform.localScale = new Vector3(-1, 1, 1);
                rb2D.MoveRotation(Mathf.Atan(lookPos.y / lookPos.x) * Mathf.Rad2Deg);
            }
            else if(lookPos.x < 0) {
                Debug.Log("2");
                debuglog = "2";
                transform.localScale = new Vector3(1, 1, 1);
                rb2D.MoveRotation(Mathf.Atan(lookPos.y / lookPos.x) * Mathf.Rad2Deg);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(rb2D.velocity.magnitude * rb2D.mass);
        if(rb2D.velocity.magnitude * rb2D.mass > DamageTolerance) {
            health -= Mathf.Ceil(rb2D.velocity.magnitude * rb2D.mass * DamageMultiplier);
            //hpValue.text = health.ToString();
        }
    }
    void Die() {
        Debug.Log("I'm Dead!");
    }
}