using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoadGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] LetPrefab;
    [SerializeField] private Transform[] LetPositions;
    private List<GameObject> lets = new List<GameObject>();
    public float spawnLet;

    [SerializeField] private GameObject CoinPrefab;
    [SerializeField] private Transform[] CoinPosition;
    private List<GameObject> coins = new List<GameObject>();
    public float spawnCoin;

    public GameObject timerObj;
    public Text timerText;

    public GameObject ScorePoint;
    public int maxSpeedScorePoint = 3;
    private int speedScore = 0;

    public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    public float maxSpeed = 10;
    public static float speed = 0;
    public int maxRoadCount = 5;
    private int destroyPos = -20;

    public float genPosRoad = 10;

    public GameObject hero;

    public static bool isPlay = false;
    void Start()
    {
        ResetLevel();
       
    }

    void Update()
    {
        if (speed == 0) return;

        ScorePoint.transform.position += new Vector3(0, 0, speedScore * Time.deltaTime);

        foreach(GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
        if(roads[0].transform.position.z < destroyPos)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);

            CraeteNextRoad();
        }
        if (lets[0].transform.position.z < destroyPos)
        {
            Destroy(lets[0]);
            lets.RemoveAt(0);
        }

        
    }

    IEnumerator GenerateCoin()
    {
        GameObject coin = Instantiate(CoinPrefab, CoinPosition[Random.Range(0, 3)].position, Quaternion.identity);
        yield return new WaitForSeconds(spawnCoin);
        coins.Add(coin);
        CraeteNextCoin();
    }

    private void CraeteNextCoin()
    {
        if (speed != 0)
        {
            StartCoroutine(GenerateCoin());
        }
    }

    IEnumerator GenerateLet()
    {
        GameObject let = Instantiate(LetPrefab[Random.Range(0,4)], LetPositions[Random.Range(0, 3)].position, Quaternion.identity);
        yield return new WaitForSeconds(spawnLet);
        lets.Add(let);
        CraeteNextLet();
    }

    private void CraeteNextLet()
    {
        if (speed != 0)
        {
            StartCoroutine(GenerateLet());
        }
    }
    private void CraeteNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count>0) {
            pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, genPosRoad);
        }
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }
    public void StartLevel()
    {
        isPlay = true;
        hero.GetComponent<Pleyer>().StateChanger(charStates.run);
        Time.timeScale = 1f;
        LetSpawn.speed = maxSpeed;   
        Coin.speed = maxSpeed;
        speed = maxSpeed;
        speedScore = maxSpeedScorePoint;
        CraeteNextLet();
        CraeteNextCoin();

    }

    public void PauseLevel()
    {
        Time.timeScale = 0f;
        StopAllCoroutines();
    }

    public void ExPouse()
    {
        LetSpawn.speed =0;
        Coin.speed = 0;
        speed = 0;
        speedScore = 0;
        StartCoroutine(ExitPouse());
    }

    IEnumerator ExitPouse()
    {
        Time.timeScale = 1;
        timerObj.SetActive(true);
        timerText.text = 3.ToString();
        yield return new WaitForSeconds(1);
        timerText.text = 2.ToString();
        yield return new WaitForSeconds(1);
        timerText.text = 1.ToString();
        yield return new WaitForSeconds(1);
        timerObj.SetActive(false);
        LetSpawn.speed = maxSpeed;
        Coin.speed = maxSpeed;
        speed = maxSpeed;
        speedScore = maxSpeedScorePoint;
        CraeteNextLet();
        CraeteNextCoin();

    }

    public void ResetLevel()
    {
        hero.GetComponent<Pleyer>().StateChanger(charStates.idle);
        Time.timeScale = 1;
        isPlay = false;
        LetSpawn.speed = 0;
        Coin.speed = 0;
        speedScore = 0;
        speed = 0;
        ScorePoint.transform.position = new Vector3(0, 0, 0);
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for(int i = 0;i<maxRoadCount; i++)
        {
            CraeteNextRoad();
        }
        while (lets.Count != 0)
        {
            Destroy(lets[0]);
            lets.RemoveAt(0);
        }
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        StopAllCoroutines();
    }
}
