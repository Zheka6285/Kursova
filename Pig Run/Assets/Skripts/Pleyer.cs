using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum charStates
{
    idle, down, run
}

public class Pleyer : MonoBehaviour
{
    [SerializeField]private charStates curState;

    public GameObject spawnHero;
    public GameObject DeadPanel;
    public GameObject Pause;
    public GameObject Score;
    public GameObject SwipePanel;

    public Text getCoinText;

    public static int isSave = 0;

    private int countGetCoin = 0;

    public AudioClip coinSong;

    private Animator anim;

    public void StateChanger(charStates state)
    {
        curState = state;
    }
    public void Anim()
    {
        if(curState == charStates.idle)
            anim.SetInteger ("state", 0);
        if (curState == charStates.run)
            anim.SetInteger("state", 1);
        if (curState == charStates.down)
            anim.SetInteger("state", 2);
    }

    void Start()
    {
        curState = charStates.idle;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Anim();
    }
  
    private void OnCollisionEnter(Collision enemy)
    {
        if (enemy.collider.GetComponent<LetSpawn>())
        {
            Coin.speed = 0;
            LetSpawn.speed = 0;
            RoadGeneration.speed = 0;
            SwipePanel.SetActive(false);
            StartCoroutine(AniDead());
            isSave = 1;
            if (SaveProgress.vibration == 1) Handheld.Vibrate();
        }
    }

    IEnumerator AniDead()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.localScale -= new Vector3(0, 0, 1 * Time.deltaTime);
            transform.localScale += new Vector3(0, Time.deltaTime, 0);
            yield return new WaitForSeconds(0.015f);
        }
        yield return new WaitForSeconds(0.2f);
        DeadPanel.SetActive(true);
        Pause.SetActive(false);
        Score.SetActive(false);
    }

    public static bool IsDeadGoToSave()
    {
        if (isSave == 1)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider coin)
    {
        if (coin.GetComponent<Coin>())
        {
            AudioSource.PlayClipAtPoint(coinSong, transform.position);
            Destroy(coin.gameObject);
            countGetCoin += 1;
            getCoinText.text = countGetCoin.ToString("0");
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ground") SwipeManager.isGround = true;
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ground") SwipeManager.isGround = false;
    }
    public void ResetLevel()
    {
        StopAllCoroutines();
        countGetCoin = 0;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.3f, 1.5f);
        transform.position = new Vector3(spawnHero.transform.position.x, spawnHero.transform.position.y, spawnHero.transform.position.z);
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        getCoinText.text = 0.ToString();
    }
}
