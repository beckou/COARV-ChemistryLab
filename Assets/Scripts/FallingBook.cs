using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBook : MonoBehaviour
{
    [SerializeField] private float _time = 70f;
    //public float fallSpeed = 8.0f;
    Component book;
    // Start is called before the first frame update
    void Start()
    {
        book = GetComponent<Component>();
        StartCoroutine(BookFall(_time));
    }

    public IEnumerator BookFall(float t)
    {
        yield return new WaitForSeconds(t);
        book.transform.position += new Vector3(0.23f, 0f, 0f); //add 0.2378 to x axis position of the book
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (CountDown.timeLeft < 90 && CountDown.timeLeft >= 89) { 

        // we need to take the input from the arrow keys pressed by the player. By default in unity, the left and right arrow keys are labeled as "Horizontal" and the up and down arrow keys as "Vertical"
        // we will define two variables where the value of each will be set "several times per frame" if the player presses the HorizontalMovement (i.e. the left or right key) and/or the VerticalMovement (i.e. the up or down key)
        float HorizontalMovement = Input.GetAxis("Horizontal");
        float VerticalMovement = Input.GetAxis("Vertical");

        // since we are dealing with three dimensional space, we will need to define a 3D vector based on which the ball should move. As such, we define a Vector3 called MoveBall that takes three parameters of X,Y,Z
        // movement on the X axis will be our HorizontalMovement (i.e. the left or right key) and on the Z axis the VerticalMovement (i.e. the up or down key). As we do not want to move the ball in the Y axis in the 3D space (i.e. up and down in the 3D space), the Y value will be set to 0
        Vector3 MoveBall = new Vector3(-0.233f, 0, -0.941f);

        //lastly, we will need to access the physics component of our ball game object (i.e. the Rigidbody) and add a force to it based on the values of the vector we just defined. We will multiple this force value with our Speed variable to be able to control the Speed of the force as we wish.
        gameObject.transform.GetComponent<Rigidbody>().AddForce(MoveBall * fallSpeed);
        

    }*/
        //Transform ThisTransform = GetComponent<Transform>(); //transform component on this object
        /*
        
        if (CountDown.timeLeft < 90 && CountDown.timeLeft >= 89)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
            audioBookFalling = GameObject.Find("Audio Book Falling").GetComponent<AudioSource>();
            audioBookFalling.Play();
            //ThisTransform.position += new Vector3(0.2378f, 0f, 0f); //add 0.2378 to x axis position of the book
            //ThisTransform.position = new Vector3(-0.233f, 0f, -0.941f);
        }
        
        */

    }
}
