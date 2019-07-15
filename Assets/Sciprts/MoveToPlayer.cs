using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    float moveSpeed;
    public float minMoveSpeed = 0.05f;
    public float maxMoveSpeed = 0.3f;
    GameObject player;
    GameObject lookAtTarget;

    public float attackRange = 1;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        lookAtTarget = GameObject.FindGameObjectWithTag("LookTarget");
        UpdateMoveSpeed();
    }

    void UpdateMoveSpeed()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }
	

    void Move()
    {
        // Neu player hoac lookAtTarget = null thi quay lai
        if (player == null || lookAtTarget == null)
            return;
        
        // neu quang duong hien tai toi player > 1 thi van di chuyen
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            // nhìn theo LookTarget vì LookTarget để tí nữa cho nó nhìn vào giữa.
            transform.LookAt(lookAtTarget.transform.position);
            // thay đổi từ điểm looktarget sang player để năm ở giữa
            transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        
        // nguoc lai neu nho hon 1 thi se setbool dung yen. va goi ham attack.
        // sau do cho movetoplayer dung lai. De cho no ko chay nua. Ton' thoi gian.
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isIdle", true);
            gameObject.GetComponent<ZombieController>().isAttack = true;
            gameObject.GetComponent<MoveToPlayer>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update () {
        Move();
    }
}
