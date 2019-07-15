using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public int zombieHealth = 3;
    private Animator anim;
    private bool isShooten;
    public float shootTime = 0.5f;
    public bool isAttack = false;
    public float attackTime = 1;
    private float lastAttackTime = 0;
    private bool isDead;
    private GameObject player;
    public float damge = 1;
    private GameObject gameController;
    
    private AudioSource audioSource;
    public AudioClip zombieDeadSound;
    
    // Nếu IsShooten = true thì isShooten= true và ShootenAnim(true) nghĩa là khi bắn thì sẽ set anim cho zombie luôn..
    public bool IsShooten
    {
        get { return isShooten; }
        set
        {
            isShooten = value;
            ShootenAnim(isShooten);
            UpdateShootenTime();
        }
    }
    private float lastShootenTime = 0;
    
    // đặt mặc định IsShooten va isDead mặc định là false
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
        IsShooten = false; 
        anim.SetBool("isDead", false);
        audioSource = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void UpdateShootenTime()
    {
        lastShootenTime = Time.time;
    }

    void UpdateAttackTime()
    {
        lastAttackTime = Time.time;
    }
    // Set gia tri cho isShooten khi truyefn isShooten vao.
    void ShootenAnim(bool isShooten)
    {
        anim.SetBool("isShooten", isShooten);
    }

    void AttackAnim(bool isAttack)
    {
        anim.SetBool("isAttack", isAttack);
    }
	
    // truyền dame vào mỗi lần click chuột fire thì gọi 1 lần. 3 lần thì destroy.
    // duoc su dung ben PlayerController.
    // them if(isDead) để kiểm tra nếu zombie chết return về ko bắn được nữa.
    public void GetHit(int damge)
    {
        if(isDead)
            return;
        audioSource.Play();
        IsShooten = true;
        zombieHealth -= damge;       

        if (zombieHealth <= 0)
        {
            Dead();
        }
    }

    // phải đổi isDead = true thì GetHit sẽ ko thực hiện nữa.
    void Dead()
    {
        isDead = true;
        audioSource.clip = zombieDeadSound;
        audioSource.Play();
        anim.SetBool("isDead", true);
        gameController.GetComponent<GameController>().GetPoint(1);
        Destroy(gameObject, 1f);
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackTime)
        {
            player.GetComponent<PlayerController>().GetHit(damge);
            AttackAnim(true);
            UpdateAttackTime();
        }
        else
        {
            {
                AttackAnim(false);
            }
        }
        
    }
    
    // If là để sau khi bắn xong với thời gian bắn là shootTime thì zombie mất anim luôn.
    // Nếu True và thời gian hiện tại >= thời gian đã qua+ thời gian bắn thì false.
    // isAttack = true khi o ben MoveToPlayer set True
    // Update is called once per frame
    void Update () {
        if (IsShooten && Time.time >= lastShootenTime + shootTime)
        {
            IsShooten = false;
        }

        if (isAttack)
        {
            Attack();
        }
    }
}
