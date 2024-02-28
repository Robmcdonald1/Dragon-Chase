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

    public GameObject InvinciblePowerUp;
    public GameObject TimerPowerUp;

    public GameObject[] Dragons;

    private float delay;

    private float powerupdelay;

    public bool haveToGivePowerUp;


    private float minY = -200f;
    private float maxY = 120f;
    private float xPosition;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        xPosition = InstaiatePosition.transform.position.x;

        for(int i = 0; i < Dragons.Length; i++)
        {
            if(i == PlayerPrefs.GetInt("CurrentDragon"))
            {
                Dragons[i].SetActive(true);
            }
            else
            {
                Dragons[i].SetActive(false);
            }
        }

        if(currentLevel > 4 || currentLevel < 1){
            currentLevel = 2;
        }

        if(currentLevel == 1){
            delay = 2.5f;
            powerupdelay = 15f;
            InstantiateEasyObstacles();
        }

        if(currentLevel == 2){
            delay = 2.0f;
            powerupdelay = 20f;
            InstantiateMediumObstacles();
        }

        if(currentLevel == 3){
            delay = 1.5f;
            powerupdelay = 20f;
            InstantiateHardObstacles();
        }

        if(currentLevel == 4){
            delay = 1.0f;
            powerupdelay = 25f;
            InstantiateUltimateObstacles();
        }

        Invoke("GivePowerUps", Random.Range(12f, 18f));
        
    }

    void GivePowerUps()
    {
        haveToGivePowerUp = true;
    }

    void BringPowerUp(GameObject obstacle)
    {
            haveToGivePowerUp = false;
            int rand = Random.Range(1, 101);

            GameObject PowerUp;

            if (rand % 2 == 0)
            {
                PowerUp = Instantiate(InvinciblePowerUp, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
            }

            else
            {
                PowerUp = Instantiate(TimerPowerUp, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
            }


            PowerUp.transform.SetParent(obstacle.transform, false);
            PowerUp.transform.localPosition = new Vector3(0f, 0f, 0f);
            Invoke("GivePowerUps", Random.Range(powerupdelay - 3, powerupdelay + 3));
    }



    void InstantiateEasyObstacles(){
        GameObject obstacle = Instantiate(EasyObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        if (haveToGivePowerUp)
        {
            BringPowerUp(obstacle);
        }
        Invoke("InstantiateEasyObstacles", delay);
    }

    void InstantiateMediumObstacles(){
        GameObject obstacle = Instantiate(MediumObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        if (haveToGivePowerUp)
        {
            BringPowerUp(obstacle);
        }
        Invoke("InstantiateMediumObstacles", delay);
    }
    void InstantiateHardObstacles(){
        GameObject obstacle = Instantiate(HardObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        if (haveToGivePowerUp)
        {
            BringPowerUp(obstacle);
        }
        Invoke("InstantiateHardObstacles", delay);
    }
    void InstantiateUltimateObstacles(){
        GameObject obstacle = Instantiate(UltimateObstacles, new Vector3(xPosition, Random.Range(minY, maxY), 0f), Quaternion.identity);
        obstacle.transform.SetParent(ObstaclesContainer.transform, false);
        obstacle.transform.position = new Vector2(xPosition,  obstacle.transform.position.y);
        if (haveToGivePowerUp)
        {
            BringPowerUp(obstacle);
        }
        Invoke("InstantiateUltimateObstacles", delay);
    }
}
