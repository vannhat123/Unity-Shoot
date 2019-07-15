using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatoGun : MonoBehaviour
{
    public Vector3 target;
    // Use this for initialization
    void Start () {
	
    }
	
    // Update is called once per frame
    void Update () {

        LookAtCursor();
    }

    void LookAtCursor()
    {
        
        // từ camera tạo ra 1 tia xuất phát từ màn hình vuông góc với màn hình đi qua tọa
        // độ con chuột
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // từ tia đó lấy ra điểm chạm hit và lấy ra được taarget.
        if (Physics.Raycast(ray, out hit))
        {
            target = hit.point;
        }

        transform.LookAt(target);
    }
}
