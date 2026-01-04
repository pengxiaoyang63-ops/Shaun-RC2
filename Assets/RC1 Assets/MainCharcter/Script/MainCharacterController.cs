using System.Collections;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    public int Xforce;
    public int Yforce;
    public bool Isgrounded;
    public float CurrentHeight;
    public float MaxHeight;
    public float TimesinceLastAttack;
    public float AttackInterval;
    public string AnimName;
    public string LastAnimName;
    public bool Controlled;
    public bool Jump;
    public bool PreJumped;
    public Vector2 Hitposition;
    public Animator animator;
    public Animator HittedEffect;
    public GameObject GameObject;
    public Rigidbody2D Rigidbody;
    public Transform Transform;
    public ParticleSystem targetPs;
    public ParticleSystem targetPs2;
    public PreGroundChecker PreGroundChecker;
    public HitBack HitBack;
    public HitBack HitBack2;
    public HitBack HitBack3;
    private ParticleSystem.EmissionModule emission;
    private ParticleSystem.EmissionModule emission2;
    private ParticleSystem.MainModule main1;
    private ParticleSystem.MainModule main2;
    public keyBindConfig keyBindConfig;

    // Start is called before the first frame update
    void Start()
    {
        MaxHeight = 0.4f;
        Xforce = 15;
        Yforce = 50;
        Isgrounded = false;
        CurrentHeight = 0;
        TimesinceLastAttack = 0f;
        AttackInterval = 0.6f;
        emission = targetPs.emission;
        emission2 = targetPs2.emission;
        main1 = targetPs.main;
        main2 = targetPs2.main;
        emission.enabled = false;
        emission2.enabled = false;
    }
    void Awake()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("RunSpeed", 1f);
        main1.simulationSpeed = Time.timeScale;
        main2.simulationSpeed = Time.timeScale;
        AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
        if (clips.Length > 0)
            AnimName = clips[0].clip.name;
        else
            AnimName = "None";
        TimesinceLastAttack += Time.deltaTime;
        if (AnimName.Contains("ATK") && LastAnimName != AnimName)
        {
            TimesinceLastAttack = 0;
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A))
        {
            if (!AnimName.Contains("ATK"))
            {
                if (Isgrounded == false)
                {
                    animator.SetBool("Idle", true);
                }
                else
                {
                    animator.SetBool("Idle", false);
                }
            }
            else
            {
                animator.SetBool("Idle", false);
            }
        }
        else
        {
            animator.SetBool("Idle", false);
        }
        //测试
        if (AnimName == "Hitted" && LastAnimName != AnimName)
        {
            HittedEffect.SetTrigger("Hitted");
            emission.enabled = true;
            emission2.enabled = true;
            ImpactFreeze();
            Controlled = true;
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x / 8, Rigidbody.velocity.y / 8);
            Rigidbody.velocity = new Vector2(-40 * Transform.localScale.x, 40);

        }
        if (LastAnimName == "Hitted" && LastAnimName != AnimName)
        {
            Controlled = false;
            Rigidbody.velocity = new Vector2(0, 0);
            animator.SetFloat("RunSpeed", 1f);
        }
        //�ⲿ�̼�
        if (Controlled == false)
        {
            //��������
            if (Input.GetKey(KeyCode.A))
            {
                Rigidbody.velocity = new Vector2(-Xforce, Rigidbody.velocity.y);
                if (!AnimName.Contains("ATK"))
                {
                    Transform.localScale = new Vector2(-1, 1);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Rigidbody.velocity = new Vector2(Xforce, Rigidbody.velocity.y);
                if (!AnimName.Contains("ATK"))
                {
                    Transform.localScale = new Vector2(1, 1);
                }
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            }
            if (Input.GetKeyDown(KeyCode.Space) && Isgrounded == true)
            {
                CurrentHeight += Time.deltaTime;
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Yforce);
                Jump = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && Isgrounded != true && PreGroundChecker.JumpAble == true)
            {
                PreJumped = true;
            }
            if (PreJumped == true && Isgrounded == true)
            {
                CurrentHeight += Time.deltaTime;
                Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Yforce);
                Jump = true;
                PreJumped = false;
                PreGroundChecker.JumpAble = false;
            }
            if (CurrentHeight == 0)
            {
                animator.SetInteger("Jump", 0);
            }
            if (CurrentHeight != 0 && AnimName == "Walk")
            {
                animator.SetInteger("Jump", 1);
            }
            if (Jump)
            {
                if (Input.GetKeyDown(KeyCode.Space) && Isgrounded == true)
                {
                    animator.SetBool("Idle", false);
                    animator.SetFloat("RunSpeed", 100);
                    CurrentHeight += Time.deltaTime;
                    Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Yforce);
                    animator.SetInteger("Jump", 1);
                }
                else if (Input.GetKey(KeyCode.Space) && Isgrounded == false && CurrentHeight <= MaxHeight)
                {
                    animator.SetFloat("RunSpeed", 1);
                    CurrentHeight += Time.deltaTime;
                    Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Yforce);
                    animator.SetInteger("Jump", 1);
                }
                if (Input.GetKeyUp(KeyCode.Space) && Isgrounded == false && CurrentHeight <= MaxHeight)
                {
                    CurrentHeight = MaxHeight + 1;
                    Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y / 1.5f);
                    animator.SetInteger("Jump", 2);
                    Invoke("Jump03", 0.3f);
                }
                if (CurrentHeight > MaxHeight && AnimName == "Jump 01")
                {
                    animator.SetInteger("Jump", 2);
                    Invoke("Jump03", 0.3f);
                }
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.J) && animator.GetInteger("Jump")==0 && !AnimName.Contains("ATK"))
            {
                animator.SetBool("Idle", true);
                Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            }
                if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.S))
                {
                    if (Isgrounded == false)
                    {
                        if (TimesinceLastAttack >= AttackInterval)
                        {
                        animator.SetInteger("ATK", 2);
                        }
                    }
                    else
                    {
                        animator.SetInteger("ATK", 1);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.J) && Input.GetKey(KeyCode.W))
                {
                    if (TimesinceLastAttack >= AttackInterval)
                    {
                    animator.SetInteger("ATK", 3);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.J) && !Input.GetKey(KeyCode.W))
                {
                    animator.SetInteger("ATK", 1);
                }
            if (AnimName == "ATK 0" || AnimName == "ATK 1" && LastAnimName != AnimName)
            {
                if (Input.GetKey(KeyCode.D))
                    Transform.localScale = new Vector2(1, 1);
                if (Input.GetKey(KeyCode.A))
                    Transform.localScale = new Vector2(-1, 1);
            }
        }
        //����
        if (HitBack.HitBool == true || HitBack2.HitBool == true)
        {
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
            HitBackFunction();
            Invoke("HitBoolfalse", 0.1f);
        }
        if (HitBack3.HitBool == true)
        {
            HitUpFunction();
            Invoke("HitBoolfalse", 0.1f);
        }
        LastAnimName = AnimName;
    }
    public void GroundFix()
    {
        Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 1);
    }
    public void Jump03()
    {
        animator.SetInteger("Jump", 3);
    }
    public void ImpactFreeze(float freezeTime = 0.3f, float slowTime = 0.08f, float slowScale = 0.2f)
    {
        StopAllCoroutines();          // ��ֹ�����ܻ�����
        StartCoroutine(ImpactFreezeCR(freezeTime, slowTime, slowScale));
    }

    private IEnumerator ImpactFreezeCR(float freezeTime, float slowTime, float slowScale)
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(freezeTime);
        Time.timeScale = slowScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        yield return new WaitForSecondsRealtime(slowTime);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        StopEmmission();
    }
    public void StopEmmission()
    {
        emission.enabled = false;
        emission2.enabled = false;
    }
    public void continue_()
    {
        Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
    }
    public void HitBackFunction()
    {
        if (Hitposition.x < transform.position.x)
        {
            Rigidbody.velocity = new Vector2(-transform.localScale.x * 20, Rigidbody.velocity.y);
        }
        else
        {
            Rigidbody.velocity = new Vector2(-transform.localScale.x * -20, Rigidbody.velocity.y);
        }
    }
    public void HitUpFunction()
    {
        if (Rigidbody.velocity.y < 0f)
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);
        }
        else
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Yforce-10);
        }
    }
    public void HitBoolfalse()
    {
        HitBack.HitBool = false;
        HitBack2.HitBool = false;
        HitBack3.HitBool = false;
    }
}
