using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{

    [SerializeField]private int NoteSpeed = 5;
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
    }
}
