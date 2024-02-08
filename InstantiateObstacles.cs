using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObstacles : MonoBehaviour
{

    private int currentLevel;

    public GameObject EasyObstacles;
    public GameObject MediumObstacles;
    public GameObject HardObstacles;
    public GameObject UltimateObstacles;

    public GameObject InstaiatePosition;
    public GameObject ObstaclesContainer;

    private float delay;


    private float minY = -200f;
    private float maxY = 120f;
    private float xPosition;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        xPosition = InstaiatePosition.transform.position.x;

        if(currentLevel > 4 || currentLevel < 1){
            currentLevel = 2;
        }

        if(currentLevel == 1){
            delay = 2.5f;
            InstantiateEasyObstacles();
        }

        if(currentLevel == 2){
            delay = 2.0f;
            InstantiateMediumObstacles();
        }

        if(currentLevel == 3){
            delay = 1.5f;
            InstantiateHardObstacles();
        }

        if(currentLevel == 4){
            delay = 1.0f;
            InstantiateUltimateObstacles();
        }
        
    }

    void InstantiateEasyObstacles(){
        GameObject obstacle = Instantiate(EasyObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        Invoke("InstantiateEasyObstacles", delay);
    }

    void InstantiateMediumObstacles(){
        GameObject obstacle = Instantiate(MediumObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        Invoke("InstantiateMediumObstacles", delay);
    }
    void InstantiateHardObstacles(){
        GameObject obstacle = Instantiate(HardObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        Invoke("InstantiateHardObstacles", delay);
    }
    void InstantiateUltimateObstacles(){
        GameObject obstacle = Instantiate(UltimateObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        Invoke("InstantiateUltimateObstacles", delay);
    }
}
