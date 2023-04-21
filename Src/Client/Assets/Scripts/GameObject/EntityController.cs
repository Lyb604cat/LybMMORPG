using System.Collections;
using System.Collections.Generic;
using Entities;
using SkillBridge.Message;
using UnityEngine;

/// <summary>
/// 根据数据驱动，控制角色行为
/// </summary>
public class EntityController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;
    private AnimatorStateInfo currentBaseState;

    public Entity entity;

    public Vector3 position;//位置
    public Vector3 direction;//方向
    Quaternion lastRotation;

    public float speed;
    public float animSpeed = 1.5f;
    public float jumpPower = 3.0f;

    public bool isPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        if(entity != null)
        {
            this.UpdateTransform();
        }
        if(! this.isPlayer )
        {
            rb.useGravity = false;
        }
    }


    void UpdateTransform()
    {
        this.position = GameObjectTool.LogicToWorld(entity.position);
    }

    void OnDestroy()
    {
        if( entity != null )
        {

        }
    }

    void FixedUpdate()
    {
        if (this.entity == null)
            return;
    }

    public void OnEntityEvent(EntityEvent entityEvent)
    {
        switch (entityEvent)
        {
            case EntityEvent.Idle:
                anim.SetBool("Move", false);
                anim.SetTrigger("Idle");
                break;
            case EntityEvent.MoveFwd:
                anim.SetBool("Move", true);
                break;
            case EntityEvent.MoveBack:
                anim.SetBool("Move", true);
                break;
            case EntityEvent.Jump:
                anim.SetTrigger("Jump");
                break;
        }
    }
}
