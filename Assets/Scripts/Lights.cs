using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [SerializeField] private float Speed = 3;
    [SerializeField] private int num = 0;
    private Renderer rend;
    private float alfa = 0;
    //Renderer => 画面での見え方のこと
    void Start()
    {
        rend = GetComponent<Renderer>();
        //対象を好きなカラーに変えるコード：GetComponent<Renderer>().material.color = Color.ColorName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(rend.material.color.a <= 0))
        {
            rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
        }

        if (num == 1)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                colorChange();
            }
            }
        if (num == 2)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                colorChange();
            }
        }
        if (num == 3)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                colorChange();
            }
        }
        if (num == 4)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                colorChange();
            }
        }
        alfa -= Speed * Time.deltaTime;
    }

    void colorChange()
    {
        alfa = 0.7f;
        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, alfa);
        //"rend"のカラーを右のカラーにしますよ～
        // rgba(red、green、blue、alpha)=>alphaは不透明度(0.0~1.0)
    }
}
