using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YXwarriermove : MonoBehaviour
{
    public Animator animator;
    public Transform MainCharacter;
    public Transform Self;
    public Rigidbody2D Rigidbody;
    private float T;
    public Transform Dashwave;
    public string AnimName;
    public string LastAnimName;
    public int directionmultiplier;
    public Animator AirAttack;
    public Animator DashWave;
    public bool DamageBool;
    private int Dashmultiplier;
    public float angle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        T += Time.deltaTime;
        float MainX = MainCharacter.position.x;
        float MainY = MainCharacter.position.y;
        float SelfX = Self.position.x;
        float SelfY = Self.position.y;
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
        if (clips.Length > 0)
            AnimName = clips[0].clip.name;
        else
            AnimName = "None";
        if (MainX > SelfX)
        {
            if (AnimName != LastAnimName)
            {
                directionmultiplier = 1;
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else
        {
            if(AnimName != LastAnimName)
            {
                directionmultiplier = -1;
                transform.localScale = new Vector2(1, 1);
            }
        }
        if (AnimName == "Flash" && LastAnimName != AnimName)
        {
            int FlashDesission = animator.GetInteger("Flash Desission");
            if (FlashDesission == 0)
            {
                Self.position = new Vector2(MainX, 14);
                SelfX = MainX;
            }
            else
            {
                if (MainX < 0)
                {
                    int rand = UnityEngine.Random.Range(10, 17);
                    Self.position = new Vector2((36+MainX) / 2 + rand, 17);
                }
                else
                {
                    int rand = UnityEngine.Random.Range(10, 17);
                    Self.position = new Vector2((-36+MainX) / 2 - rand, 17);
                }
                SelfX = Self.position.x;
                SelfY = Self.position.y;
            }
        }
        else if (AnimName == "Flash 1")
        {
            Rigidbody.velocity = new Vector2(0,0) ;
        }
        else if(AnimName == "Flash")
        {
            Rigidbody.gravityScale = 0;
        }
        else if (AnimName == "PreDash")
        {
            Rigidbody.gravityScale = 0;
        }
        else if (AnimName == "Spear Spin")
        {
            Rigidbody.velocity = new Vector2(23*directionmultiplier, 0);
        }
        else if (AnimName == "Spear Spin Delay" && LastAnimName != AnimName)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x/5, 0);
        }
        else if (AnimName == "Dash" && LastAnimName != AnimName)
        {
            Dashwave.position = new Vector2(SelfX, SelfY);
            DashWave.SetTrigger("Dash Trigger");
            Rigidbody.velocity = new Vector2(0, 0);
            Rigidbody.gravityScale = 0;
            float dx = MainX - SelfX;
            float dy = MainY - SelfY;
            angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            if (MainX < SelfX)
            {
                angle += 180;
                Dashmultiplier = -1;
            }
            else
            {
                Dashmultiplier = 1;
            }
            Self.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Dashwave.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (AnimName == "Fly Back")
        {
            Rigidbody.velocity = new Vector2(-50*directionmultiplier, Rigidbody.velocity.y);
        }
        if (LastAnimName == "Dash" && LastAnimName != AnimName)
        {
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
        }
        if (LastAnimName == "Fly Back" && LastAnimName != AnimName)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x/5, Rigidbody.velocity.y);
        }
        if (AnimName == "Dash")
        {
            Vector2 localDir = transform.right;
            Rigidbody.AddForce(localDir * Dashmultiplier * 330, ForceMode2D.Force);

        }
        if (AnimName == "Dash02")
        {
            Vector2 localDir = transform.right;
            Rigidbody.velocity = new Vector2(directionmultiplier*90, 0);
        }
        if (LastAnimName == "Dash02" && LastAnimName != AnimName)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x / 8, Rigidbody.velocity.y);
        }
        if (AnimName == "Dash02" && LastAnimName != AnimName)
        {
            Rigidbody.velocity = new Vector2(0, 0);
        }
        if (AnimName != "Dash" && AnimName != "Flash" && AnimName != "PreDash"&& AnimName != "PreAirAttack")
        {
            Rigidbody.gravityScale = 3f;
            Self.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }
        if (AnimName == "AirAttack" && LastAnimName != AnimName)
        {
            AirAttack.SetTrigger("AirAttack");
        }
        if (LastAnimName != AnimName)
        {
            DamageBool = true;
        }
        LastAnimName = AnimName;
    }
}
