using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class player : MonoBehaviour
{
    public float deadLiney;
    public float myspeed;
    public float jumpForce;
    public int extraJump = 3;
    public GameObject attactCollider;

    Animator myAnim;
    Rigidbody2D myRigi;

    bool isJumpPressed, canJump, isattacked, ishurt;
    // Start is called before the first frame update

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();

        isJumpPressed = false;
        canJump = true;
        isattacked = false;
        ishurt = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0 && ishurt == false)
        {
            isJumpPressed = true;
            extraJump = extraJump - 1;
        }
        if (Input.GetKeyDown(KeyCode.Z) && ishurt == false)
        {
            myAnim.SetTrigger("attack");
            isattacked = true;
        }
    }

    private void FixedUpdate()
    {
        float a = Input.GetAxisRaw("Horizontal");
        if (isattacked == true)
        {
            a = 0;
        }
        if (a > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (a < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, -1f);
        }
        myAnim.SetFloat("run", Mathf.Abs(a));
        if (isJumpPressed)
        {
            myRigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;

        }
        if (!ishurt)
        {
            myRigi.velocity = new Vector2(a * myspeed, myRigi.velocity.y);
        }
        deadLiney = -20f;
        if (transform.position.y<deadLiney )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            ishurt = true;
            myAnim.SetBool("hurt", true);
            if (transform.localScale.x == 1.0f)
            {
                myRigi.velocity = new Vector2(-10.0f, 10.0f);
            }
            else
            {
                myRigi.velocity = new Vector2(10.0f, 10.0f);
            }
            StartCoroutine("SetisHurtFalse");

        }
    }
    IEnumerator SetisHurtFalse()
    {
        yield return new WaitForSeconds(0.8f);
        ishurt = false;
        myAnim.SetBool("hurt", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            extraJump = 3;
        }

    }
    public void Setisattackfalse()
    {
        isattacked = false;
        
    }
    public void SetAttackCollideron()
    {
        attactCollider.SetActive(true);
    }
    public void SetAttackColliderOff()
    {
        attactCollider.SetActive(false);
    }
  


}















