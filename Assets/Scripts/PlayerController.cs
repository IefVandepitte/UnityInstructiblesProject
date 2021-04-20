using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private int _count;
    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        _count = 0;
        CountText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handles physics-related protocols
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // zero in up/down direction, this game player can move in 2 dimensions
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // force makes player move around, Time.deltaTime makes movement smoother, speed will be adapted in unity editor
        GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
    }

    //handles collisions with objects
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "item")
        {
            other.gameObject.SetActive(false);
            _count = _count+1;
            CountText();
        }
        if (other.gameObject.tag == "hazard")
        {
            other.gameObject.SetActive(false);
            Vector3 jump = new Vector3(0.0f, 30, 0.0f);
            GetComponent<Rigidbody>().AddForce(jump * speed * Time.deltaTime);
        }
    }

    void CountText()
    {
        countText.text = $"Count: {_count.ToString()}";
    }
}
