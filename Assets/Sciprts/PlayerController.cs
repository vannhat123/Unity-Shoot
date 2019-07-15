using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // vì 1 lần nhấp chuột là rất nhiều click vào lên phải giảm tốc độ lại 
    // bằng cách thêm fireTime
    
    public int damge = 1;

    public float fireTime = 0.3f;
    public GameObject smoke;
    public GameObject gunHead;
    public float playerHealth = 10;
    public AudioClip playerDeadSound;
    public Slider healthBar;
    
    private float playerCurrentHealth = 10;
    
    private float lastFireTime = 0;
    private Animator anim;
    private  AudioSource audioSource;
    private GameObject gameController;

    // Use this for initialization
    // healthBar.value la mau hien tai.
    void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        UpdateFireTime();
        audioSource = gameObject.GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        healthBar.maxValue = playerHealth;
        healthBar.value = playerCurrentHealth;
        healthBar.minValue = 0;
    }

    // update thời gian bắn đạn = thời gian hiện tại.
    void UpdateFireTime()
    {
        lastFireTime = Time.time;
    }
    
    void SetFireAnim(bool isFire)
    {
        anim.SetBool("isShoot", isFire);
    }
	
    // Nếu thời gian hiện tại >= thời gian hiện tại lúc trước + freeTime.
    // khi đó sẽ xóa zombie bằng cách gọi hàm Gethit bên zombiecontroller.
//    void Fire()
//    {
//        if (Time.time >= lastFireTime + fireTime)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;
//
//            if (Physics.Raycast(ray, out hit))
//            {
//                if (hit.transform.tag.Equals("Zombie"))
//                {
//                    SetFireAnim(true);
//                    InsSmoke();
//                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(damge);
//                }
//            }
//
//            UpdateFireTime();
//        }
//        else
//        {
//            SetFireAnim(false);
//        }
//    }

// mỗi lần bị cào 1 cái là kêu, sau đó trừ 1 damge và lấy giá trị hiện tại.
    public void GetHit(float damge)
    {
        audioSource.Play();
        playerCurrentHealth -= damge;
        healthBar.value = playerCurrentHealth;

        if (playerCurrentHealth <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        audioSource.clip = playerDeadSound;
        audioSource.Play();
        gameController.GetComponent<GameController>().EndGame();
    }

    void Fire()
    {
        if (Time.time >= lastFireTime + fireTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#if UNITY_IOS || UNITY_ANDROID
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {            
                    SetFireAnim(true);
                    InsSmoke();
                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(damge);
                }
            }
#else           

            // bắn từ gunHEad về phía trước.
            // origin: toa do ban dau. direction phuong huong ban toi. forward phias truoc.
            RaycastHit hit;

            if (Physics.Raycast(gunHead.transform.position, gunHead.transform.forward, out hit))
            {
                if (hit.transform.tag.Equals("Zombie"))
                {
                    SetFireAnim(true);
                    InsSmoke();
                    hit.transform.gameObject.GetComponent<ZombieController>().GetHit(damge);
                }
            }
#endif
            UpdateFireTime();
        }
        else
        {
            SetFireAnim(false);
        }
    }

    // tao ra hieu ung khoi' khi ban' va pha huy sau 0.5 giay
    void InsSmoke()
    {
        GameObject sm = Instantiate(smoke , gunHead.transform.position, transform.rotation) as GameObject;
        Destroy(sm , 0.5f);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }
}
