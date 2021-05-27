using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Timers;
using System.Collections;

public class SwipeManager : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public GameObject Hero;
    SpriteRenderer spriteHero;
    Rigidbody rigidHero;
    public static bool isDown;

    public float JumpForce = 4;
    public float gravityMod;

    public static bool isGround = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        float maxRight = 0.8f;
        float maxLeft = -0.8f;

        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (eventData.delta.x > 0)
            {
                if (Hero.transform.position.x < maxRight)
                {
                    Hero.transform.position += new Vector3(0.9f, 0, 0);
                }
                else
                {
                    Hero.transform.Rotate(0, 180, 0);
                }
            }
            else if (eventData.delta.x < 0)
            {
                if (Hero.transform.position.x > maxLeft)
                {
                    Hero.transform.position += new Vector3(-0.9f, 0, 0);
                }
                else
                {
                    Hero.transform.Rotate(0, 180, 0);
                }
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                if (isGround)
                    Jump();
            }
            else
            {
                //isDown = true;
                StartCoroutine(SitDown());
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
    
    public void Jump()
    {
        rigidHero.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Hero.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    IEnumerator SitDown()
    {
        Hero.GetComponent<BoxCollider>().size = new Vector3(0.9f, 0.65f ,1.5f);
        Hero.GetComponent<Pleyer>().StateChanger(charStates.down);
        yield return new WaitForSeconds(0.6f);
        Hero.GetComponent<Pleyer>().StateChanger(charStates.run);
        Hero.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.3f, 1.5f);
    }

   

    void Start()
    {
       
        spriteHero = Hero.GetComponent<SpriteRenderer>();
        rigidHero = Hero.GetComponent<Rigidbody>();
        Physics.gravity *= gravityMod;
    }

}
