using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    private float mH;
    private float mV;

    void Start()
    {
        //GetComponent<Transform>().position = new Vector3(3,2);
        rb = GetComponent<Rigidbody2D>();

        //GetComponent<Transform>().position = Mouse;           //Para arrastrar el objeto

        //GetComponent<Transform>().position = p;
        //GetComponent<Transform>().Translate(p * speed * Time.fixedDeltaTime);
        //rb.velocity = p * 1000 * speed * Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        mH = Input.GetAxis("Horizontal");
        mV = Input.GetAxis("Vertical");
        Movement(mH, mV);
    }

    private void Movement(float x, float y)
    {
        //GetComponent<Transform>().Translate(x * speed * Time.deltaTime, y *speed * Time.deltaTime, 0);
        Vector3 Pinput = new Vector3(x, y, 0);
        rb.velocity = Pinput * 1000 * speed * Time.fixedDeltaTime;
    }
}
