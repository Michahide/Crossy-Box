using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    //Range nya jangan diset 0
    [SerializeField, Range(0.01f, 1f)] float moveDuration =0.2f;

    [SerializeField, Range(0.01f, 1f)] float jumpHeight =0.5f;
    // Update is called once per frame
    private void Update()
    {
        //GetKey = Kalau dipencet terus, bakal kedeteksi terus
        //GetKeyDown = Kalau dipencet terus, cuma kedeteksi sekali
        // if (Input.GetKey(KeyCode.UpArrow))
        // {
        //     Debug.Log("Forward");
        // }

        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     Debug.Log("Back");
        // }

        //buat kontrol pencet panah atas, dll
        var moveDir = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += new Vector2(0,1);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += new Vector2(0,-1);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += new Vector2(1,0);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += new Vector2(-1,0);
        }

        /*dua if di bawah berguna agar karakter tidak dapat 
        move sebelum sampai di tempat*/
        if (moveDir == Vector2.zero)
            return;

        if(isJumping() == false)
            Jump(moveDir);

    }

    private void Jump(Vector2 dir){
        var targetDirection = new Vector3( x: dir.x, y: 0, z: dir.y );
        var targetPosition = transform.position + targetDirection; 
        
        //Bikin sequence untuk animasi
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight,moveDuration/2));
        moveSeq.Append(transform.DOMoveY(0,moveDuration/2));

        // transform.DOMoveY(2f,0.2f)
        //     .OnComplete(()=>transform.DOMoveY(0,0.2f));
        

        transform.DOMoveX(targetPosition.x,moveDuration/2);
        transform.DOMoveZ(targetPosition.z,moveDuration/2);
    }

    private bool isJumping()
    {
        return DOTween.IsTweening(transform);
    }
}
