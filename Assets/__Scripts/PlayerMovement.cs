using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState{//states for player movement, enum type because it needs many states
    walk,
    attack,
    interact,
    stagger,
    idle,
    ability,
    shield,
    jump
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;//refrence the player's current state
    public float speed;//player moving speed
    private Rigidbody2D myRigidbody;//player rigibody 
    private Vector3 change;//position change vectpr
    private Animator animator;//for player animation
    public FloatValue currentHealth;//health value of player
    public Signal playerHealthSignal;//signal for player health
    public GameObject projectile;//arrow
    public GameObject mudBall;//mudball the player throws
    public VectorValue startingPosition;
    public GameObject knight;//knight picture displayed on top right
    public GameObject wizard;//wizard picture displayed on top right
    public Inv playerInventory; //inventory of player
    public GameObject collision; //collision that prevents player from walking on water

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;//setting current state to walk when starting game
        animator = GetComponent<Animator>();//getting player animation
        myRigidbody = GetComponent<Rigidbody2D>();//reference to player rigidbody
        animator.SetFloat("moveX", 0);//setting correct starting animation for x component
        animator.SetFloat("moveY", -1);//setting correct starting animation for y component
        transform.position=startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        change=Vector3.zero;//reset player change to zero every frame
        change.x=Input.GetAxisRaw("Horizontal");//setting x value of movement change
        change.y=Input.GetAxisRaw("Vertical");//setting y valye of movement change
        if (!IsRestricted(currentState))
        {
            if (Input.GetButtonDown("attack") && (knight.activeSelf) && currentState != PlayerState.attack)//if the player has knight selected and attack button is pressed and player is not already attacking and the current state is not stagger
            {
                StartCoroutine(AttackCo());//if true, then start attack coroutine
            }
            else if (Input.GetButtonDown("Second Weapon") && (wizard.activeSelf) && currentState != PlayerState.attack && currentState != PlayerState.stagger)//if the player has wizard selected andsecond weapon button is pressed
            {
                {
                    StartCoroutine(SecondAttackCo());
                }
            }
            else if (Input.GetButtonDown("shield") && (knight.activeSelf) && currentState != PlayerState.attack )//if the player has knight selected and second weapon button is pressed
            {
                {
                    StartCoroutine(ShieldCo());
                }
            }
            else if (Input.GetButtonDown("jump") && (wizard.activeSelf) && currentState != PlayerState.attack && currentState != PlayerState.stagger)//if the player has wizard selected and jump button is pressed
            {
                {
                    StartCoroutine(JumpCo());
                }
            }
            if (Input.GetButtonDown("mudball") && currentState != PlayerState.attack && currentState != PlayerState.stagger)//if mudball button is pressed and player is not already attacking and the current state is not stagger
            {
                StartCoroutine(MudballCo());//if true, then start attack coroutine
            }
            else if (currentState == PlayerState.walk || currentState == PlayerState.idle)//if player is in walk state or idle state
            {
                UpdateAnimationAndMove();//move  animations according to player
            }
        }

        if (playerInventory.coins>=20)
        {
            collision.SetActive(false);
        }
    }
    bool IsRestricted(PlayerState current)
    {
        if(current == PlayerState.attack || currentState == PlayerState.ability)
        {
            return true;
        }
        return false;
    }
    private IEnumerator AttackCo()//for sword attack routine
    {
        animator.SetBool("attacking", true); //set attacking boolean variable to true
        currentState = PlayerState.attack;//making current state attack
        yield return null;//wait a frame before next action
        animator.SetBool("attacking", false);//stop attacking by setting attacking boolean to false
        yield return new WaitForSeconds(.3f);//waiting 3f seconds before walking again
        currentState = PlayerState.walk;//set state back to walk
    }

    private IEnumerator SecondAttackCo() //for second attack/bow and arrow attack
    {
        animator.SetBool("attacking0", true);
        currentState = PlayerState.attack;
        yield return null;
        MakeArrow();//call instintiating method
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
        animator.SetBool("attacking0", false);//stop attacking by setting attacking boolean to false
        yield return new WaitForSeconds(.3f);//waiting 3f seconds before walking again
        currentState = PlayerState.walk;//set state back to walk
    }

    private IEnumerator MudballCo() //for second attack/bow and arrow attack
    {
        currentState = PlayerState.attack;
        yield return null;
        MakeMudball();//call instintiating method
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator ShieldCo() //for shield
    {
        animator.SetBool("shield", true); //set shield king boolean variable to true
        currentState = PlayerState.shield;//making current state shield
        yield return null;//wait a frame before next action
        animator.SetBool("shield", false);//stop shield by setting shield boolean to false
        yield return new WaitForSeconds(.3f);//waiting 3f seconds before walking again
        currentState = PlayerState.walk;//set state back to walk
    }

    private IEnumerator JumpCo() //for shield
    {
        animator.SetBool("jump", true); //set jump boolean variable to true
        currentState = PlayerState.jump;//making current state jump
        yield return null;//wait a frame before next action
        animator.SetBool("jump", false);//stop jump by setting jump boolean to false
        yield return new WaitForSeconds(.3f);//waiting 3f seconds before walking again
        currentState = PlayerState.walk;//set state back to walk
    }

    private void MakeArrow()//instintiate arrow
    {
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));//to get right direction in a temporary
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();//making arrow according to player position
            arrow.Setup(temp, ChooseArrowDirection());
        }
    }

    private void MakeMudball()//instintiate mudball
    {
        {
            Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));//to get right direction in a temporary
            Arrow mudball = Instantiate(mudBall, transform.position, Quaternion.identity).GetComponent<Arrow>();//making arrow according to player position
            mudball.Setup(temp, ChooseArrowDirection());
        }
    }

    Vector3 ChooseArrowDirection()//to send arrow in right direction
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX"))* Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    void UpdateAnimationAndMove()//updating the animation
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter() //moving character to places without buttons
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime //moving a small amount each frame
        );
    }

    public void Knock(float knockTime, float damage)//calling knockback routine function
    {
        currentHealth.RuntimeValue -=damage;//decrement health by damage done
        playerHealthSignal.Raise(); //raise the signal when health of the player decrements
        if (currentHealth.RuntimeValue>0)//if there is health left for the player
        {
            StartCoroutine(KnockCo(knockTime));//knockback routine
        }else {
            this.gameObject.SetActive(false);//else if health is done, player dissapears
        }
    }

    private IEnumerator KnockCo(float knockTime)//knockback player when attack 
    {
        if (myRigidbody != null)//if there is a rigidbody
        {
            yield return new WaitForSeconds(knockTime);//wait for knocktime to be over
            myRigidbody.velocity = Vector2.zero;//set velocity to zero
            currentState = PlayerState.idle;//change player state to idle
            myRigidbody.velocity = Vector2.zero;//set velocity to zero
        }
    }
}
