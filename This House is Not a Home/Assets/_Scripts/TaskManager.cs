using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject player;
    public int beerCount = 0;
    public int angerAdjuster;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void GetBeer()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(BeerRun());  
        } 
    }

    public void TakeOutTrash()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(TrashRun());
            //Debug.Log("Made it here");
            
        }
    }

    public void FixTV()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(TVRepair());
        }
    }

    public void QuietDog()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(ShutDatDogUp());
        }
    }

    public void GetKey()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(KeyGrab());
            
        }

    }

    public void UnlockDoor()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(DoorUnlock());
        }
    }

    public void MakeCall()
    {
        if (!GameManager.instance.isTravelling && !GameManager.instance.isBusy)
        {
            StartCoroutine(UsePhone());
        }
    }

    IEnumerator TrashRun() {

        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 2));
        //some animation

        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 1));

        //some animation
        //Task complete
        GameManager.instance.doesTrashSmell = false;
        GameManager.instance.buttons[1].SetActive(false);
        GameManager.instance.anger -= angerAdjuster;
        GameManager.instance.currentTasks--;

        GameManager.instance.trashCan.GetComponent<SpriteRenderer>().sprite = GameManager.instance.trashcanSprite[0];
        if (!GameManager.instance.isFirstTaskCompleted)
        {
            GameManager.instance.isFirstTaskCompleted = true;
            GameManager.instance.SpawnKey();
        }
    }

    IEnumerator BeerRun()
    {

        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 5));
        //some animation

        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 2));
        //some animation
        //Task complete
        GameManager.instance.doesNeedBeer = false;
        GameManager.instance.buttons[4].SetActive(false);
        GameManager.instance.anger -= angerAdjuster;
        GameManager.instance.currentTasks--;

        if (beerCount < 3)
        {
            beerCount++;
        }
        else
        {
            GameManager.instance.isDadAsleep = true;
            GameManager.instance.dad.GetComponent<AudioSource>().Play();
            beerCount = 0;
            if(GameManager.instance.objectivesCompleted > 2)
            {
                GameManager.instance.buttons[6].SetActive(true);
                GameManager.instance.buttons[6].GetComponent<AudioSource>().Play();
            }
            
        }

        if (!GameManager.instance.isFirstTaskCompleted)
        {
            GameManager.instance.isFirstTaskCompleted = true;
            GameManager.instance.SpawnKey();
        }
    }

    IEnumerator TVRepair()
    {
        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 4));
        //some animation
        //Task complete
        GameManager.instance.isTVBroke = false;
        GameManager.instance.buttons[3].SetActive(false);
        GameManager.instance.anger -= angerAdjuster;
        GameManager.instance.currentTasks--;
        GameManager.instance.tv.GetComponent<SpriteRenderer>().sprite = GameManager.instance.tvSprite[0];

        if (!GameManager.instance.isFirstTaskCompleted)
        {
            GameManager.instance.isFirstTaskCompleted = true;
            GameManager.instance.SpawnKey();
        }
    }

    IEnumerator ShutDatDogUp()
    {
        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 3));
        //some animation
        //Task complete
        GameManager.instance.isDogLoud = false;
        GameManager.instance.buttons[2].SetActive(false);
        GameManager.instance.anger -= angerAdjuster;
        GameManager.instance.currentTasks--;
     
        if (!GameManager.instance.isFirstTaskCompleted)
        {
            GameManager.instance.isFirstTaskCompleted = true;
            GameManager.instance.SpawnKey();
        }
    }

    IEnumerator KeyGrab()
    {
        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 0));
        yield return StartCoroutine(ObjectiveTimer());
      
        //objective complete
        GameManager.instance.objectivesCompleted = 1;
        
    }

    IEnumerator DoorUnlock()
    {
        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 7));
        yield return StartCoroutine(ObjectiveTimer());
        //some long animation
        //objective complete
        GameManager.instance.objectivesCompleted = 2;
    }

    IEnumerator UsePhone()
    {
        yield return StartCoroutine(player.GetComponent<Move>().findPath(GameManager.instance.currentPosition, 8));
        yield return StartCoroutine(ObjectiveTimer());
        //some long animation
        //objective complete
        GameManager.instance.objectivesCompleted = 3;
    }

    IEnumerator ObjectiveTimer()
    {
        GameManager.instance.isBusy = true;
        yield return new WaitForSeconds(3);
        GameManager.instance.isBusy = false;
    }
}
