using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontroller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardspeed;
    private int desiredline = 1;
    public float lanedistance = 4;
    public float jumpForce;
    public float gravity = -20;
    public GameObject PressAny;
    public Animator animator;
    public float maxSpeed;
    public AudioSource source;


    void Start()
    {
       
        controller = GetComponent<CharacterController>();
         
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerManager.isGameStarted = true;
            Time.timeScale = 1f;
            Destroy(PressAny);
        }

        if (!PlayerManager.isGameStarted)
            return;
        if (forwardspeed < maxSpeed)
        {
            if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
            {
                forwardspeed += 0.5f * Time.deltaTime;
            }
            if (SceneManager.GetSceneByBuildIndex(3).isLoaded)
            {
                forwardspeed += 1f * Time.deltaTime;
            }
            else
            {
                forwardspeed += 0.1f * Time.deltaTime;
            }
            
        }
        animator.SetBool("run", true);
        direction.z = forwardspeed;
       
         
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredline++;
            if (desiredline==3)
            {
                desiredline = 2;

            }
        }

        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredline--;
            if (desiredline == -1)
            {
                desiredline = 0;
            }
        }

        Vector3 targetPos = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredline==0)
        {
            targetPos += Vector3.left * lanedistance;
        }
        else if (desiredline==2)
        {
            targetPos += Vector3.right * lanedistance;

        }
        //transform.position = targetPos;
        //controller.center = controller.center;
        if (transform.position == targetPos)
            return;
        Vector3 diff = targetPos - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);


    }
    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }



    private void Jump()
    {
        float jumpHeight = 2f; // yükseklik değeri
        float jumpDuration = 0.5f; // zıplama süresi
        
    Vector3 jumpVector = new Vector3(0, CalculateJumpVerticalSpeed(jumpHeight, jumpDuration), 0);
        direction.y = jumpForce;
       // animator.SetBool("jump", true);
    }

    private float CalculateJumpVerticalSpeed(float jumpHeight, float jumpDuration)
{
    float gravity = Physics.gravity.y;
    return Mathf.Sqrt(2 * jumpHeight* gravity / jumpDuration);
}

private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag=="obstacle")
        {
            source.Play();
            PlayerManager.gameover = true;
            
        }
        if (hit.transform.tag=="finish")
        {
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeSceneIndex+1);
            // SceneManager.LoadScene(1);

            if (activeSceneIndex==3)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
