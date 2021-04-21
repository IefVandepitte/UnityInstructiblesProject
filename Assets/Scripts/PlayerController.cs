using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private int _count;
    public Text countText;

    private Touch _theTouch;

    GameObject particle;


    // Start is called before the first frame update
    void Start()
    {
        _count = 0;
        CountText();
        Debug.Log("Started");
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

        // Touch support
        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                //Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(_theTouch.position);
                Debug.Log(ray);
                Debug.Log(Physics.Raycast(ray));
                if (Physics.Raycast(ray))
                {
                    // Create a particle if hit
                    Instantiate(particle, transform.position, transform.rotation);
                }
                Vector3 jump = new Vector3(0.0f, 30, 0.0f);
                GetComponent<Rigidbody>().AddForce(jump * speed * Time.deltaTime);
            }
        }

        

        // Accelerometer support

        Vector3 dir = Vector3.zero;
        // we assume that the device is held parrallel to the ground
        // and the home button is in the right hand

        //remap the device accelerometer axis to game coordinates:
        // 1) XY plane of the device is mapped onto XZ plane
        // 2) rotated 90 degrees around Y axis

        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        // clamp acceleration vector to the unit sphere
        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        // Move object
       //transform.Translate(dir * speed);      
        

        // force makes player move around, Time.deltaTime makes movement smoother, speed will be adapted in unity editor
        GetComponent<Rigidbody>().AddForce(dir * speed * Time.deltaTime);


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
