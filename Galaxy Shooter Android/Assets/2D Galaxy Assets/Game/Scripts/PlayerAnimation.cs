using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

	void Start ()
    {
        //get our component
        _anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        else if (horizontalInput > 0)
        {
            _anim.SetBool("Turn_Right", true);
            _anim.SetBool("Turn_Left", false);
        }
        else if (horizontalInput == 0)
        {
            _anim.SetBool("Turn_Right", false);
            _anim.SetBool("Turn_Left", false);
        }
    }
}
