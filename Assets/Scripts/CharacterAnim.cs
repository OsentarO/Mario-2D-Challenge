using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour {

    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        if ((Input.GetKey(KeyCode.LeftArrow)) || Input.GetKey(KeyCode.RightArrow)){
            anim.SetBool("isrunning", true);
        } else {
            anim.SetBool("isrunning", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("isjump");
        }
    }
}