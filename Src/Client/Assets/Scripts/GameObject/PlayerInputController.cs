using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

using Entities;
using SkillBridge.Message;

public class PlayerInputController : MonoBehaviour
{
    //public Rigidbody rb;
    //SkillBridge.Message.CharacterState state;

    public Character character;

    //public float rotateSpeed = 2.0f;

    //public float turnAngle = 10;

    //public int speed;

    public EntityController entityController;

    public bool onAir = false;


    // Start is called before the first frame update
    void Start()
    {
        //state
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(character == null)
        {
            return;
        }
        float v = Input.GetAxis("Vertical");
        if(v > 0)
        {

        }
    }
}
