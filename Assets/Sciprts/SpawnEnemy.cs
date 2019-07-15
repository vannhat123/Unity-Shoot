using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // tạo 1 mảng zombie để xuất hiện ngẫu nhiên
    GameObject[] spawnPoint;
    // lấy đổi tượng zombie để tạo ra
    public GameObject zombie;
    public float minSpawnTime = 0.2f;
    public float maxSpawnTime = 1;
    private float lastSpawnTime = 0;
    private float spawnTime = 0;
    // Use this for initialization
    //update thời gian để lastSpawnTime quay lại ban đầu.
    void Start ()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("ReSpawn");
        UpdateSpawnTime();
    }

    void UpdateSpawnTime()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // chọn 1 chỗ bất kì để xuất hiện quái vật.
    // sau đó lệnh dưới để xuất hiện.
    void Spawn()
    {
        int point = Random.Range(0, spawnPoint.Length);
        Instantiate(zombie, spawnPoint[point].transform.position, Quaternion.identity);
        UpdateSpawnTime();
    }
	
    // Update is called once per frame
    // nếu thời gian hiện tại của game lớn hơn thời gian đẫ qua + thời gian xuất hiện zombie
    // thì tạo ra quái vật. Vì ví dụ hết game sẽ không ra quái vật nữa.
    void Update () {
        if (Time.time >= lastSpawnTime + spawnTime)
        {
            Spawn();
        }
    }
}
