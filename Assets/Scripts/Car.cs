using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float speed = 1;

    int extent;

    private void Update() {
        //Mengatur majunya mobil
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        //Mengatur kapan dihapusnya mobil, dengan kondisi terhapus kalau udah kena area abu
        if (this.transform.position.x < - (extent + 1) || this.transform.position.x > extent + 1){
            Destroy(this.gameObject);
        }
    }

    public void setup(int extent){
        this.extent = extent;
    }
}
