using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject menu;

    public Text txtpoint;

    private int currentPoint = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPoint(int point)
    {
        currentPoint++;
        txtpoint.text = "Zombie killed: " + currentPoint.ToString();

    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
    
    public void EndGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0;
    }
}
