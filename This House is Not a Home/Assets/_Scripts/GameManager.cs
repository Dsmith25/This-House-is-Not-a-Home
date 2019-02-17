using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;

	public float anger = 0f;
	public int currentPosition = 2;
	public int objectivesCompleted = 0;
	public bool isFirstTaskCompleted = false;
	public int currentTasks = 0;
	public int taskWeight = 1;
	public float loseCondition = 1000f;
	public int upperBound = 20;
	public int lowerBound = 10;
	public bool isDadAsleep = false;
	public int sleepTime = 5;
    

    public GameObject player;
    public GameObject dad;
    public GameObject tv;
    public GameObject trashCan;
    public GameObject dog;
    public GameObject[] buttons;
    public GameObject key;
    public GameObject door;
    public GameObject pivot;

    public Color startColor;
    public Color endColor;
    public float changeRate = 1f;

    public float nextTimeToChange1 = 0f;
	public float nextTimeToChange2 = 0f;
	public float nextTimeToChange3 = 0f;
	public float nextTimeToChange4 = 0f;

    public bool doesNeedBeer = false;
    public bool isTVBroke = false;
    public bool isDogLoud = false;
    public bool doesTrashSmell = false;
    public bool isColorOne = true;
    public bool isTravelling = false;
    public bool isBusy = false;

    Animator animPlayer;
    Animator animDog;
    public Sprite[] AngryDad;
    public Sprite[] tvSprite;
    public Sprite[] trashcanSprite;
    //public Sprite[] dogBark;

    void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			
			//if not, set instance to this
			instance = this;
		
		//If instance already exists and it's not this:
		else if (instance != this)
			
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    
		
		//Sets this to not be destroyed when reloading scene
		//DontDestroyOnLoad(gameObject);

        foreach (GameObject g in buttons)
        {
            g.SetActive(false);
        }

        key.SetActive(false);
    }

	void Start()
	{
        animPlayer = player.GetComponent<Animator>();
        animDog = dog.GetComponent<Animator>();
        dad.GetComponent<SpriteRenderer>().sprite = AngryDad[0];
        

    }

	void Update()
	{
        animPlayer.SetBool("isTravling", isTravelling);
        animDog.SetBool("isActive", isDogLoud);

        if (anger < 0)
        {
            anger = 0;
        }

        if (!isDadAsleep) {
			anger += (currentTasks * taskWeight * Time.deltaTime);
		}

		//if (isFirstTaskCompleted) {
		//	SpawnKey();
		//}

		switch (objectivesCompleted) {
		case 1: KeyObtained();
			break;
		case 2: DoorUnlocked();
			break;
		case 3: Win();
			break;
		}

		if (anger > loseCondition)
        {
			GameOver();
		}        

		if (doesNeedBeer) {
			nextTimeToChange1++;
			if (nextTimeToChange1 > 60)
			{
				nextTimeToChange1 = 0;
				if (isColorOne)
				{
					buttons[4].GetComponent<Image>().color = endColor;
					isColorOne = false;
				} else
				{
					buttons[4].GetComponent<Image>().color = startColor;
					isColorOne = true;
				}
			}
		}

		//if (doesTrashSmell) {
		//	nextTimeToChange2++;
		//	if (nextTimeToChange2 > 60)
		//	{
		//		nextTimeToChange2 = 0;
		//		if (isColorOne)
		//		{
		//			buttons[1].GetComponent<Image>().color = endColor;
		//			isColorOne = false;
		//		} else
		//		{
		//			buttons[1].GetComponent<Image>().color = startColor;
		//			isColorOne = true;
		//		}
		//	}
		//}

		//if (isTVBroke) {
		//	nextTimeToChange3++;
		//	if (nextTimeToChange3 > 60)
		//	{
		//		nextTimeToChange3 = 0;
		//		if (isColorOne)
		//		{
		//			buttons[3].GetComponent<Image>().color = endColor;
		//			isColorOne = false;
		//		} else
		//		{
		//			buttons[3].GetComponent<Image>().color = startColor;
		//			isColorOne = true;
		//		}
		//	}
		//}

		//if (isDogLoud) {
		//	nextTimeToChange4++;
		//	if (nextTimeToChange4 > 60)
		//	{
		//		nextTimeToChange4 = 0;
		//		if (isColorOne)
		//		{
		//			buttons[2].GetComponent<Image>().color = endColor;
		//			isColorOne = false;
		//		} else
		//		{
		//			buttons[2].GetComponent<Image>().color = startColor;
		//			isColorOne = true;
		//		}
		//	}
		//}

        dadAnimations();
	}

	 public void CreateTask()
	{
		if (!isDadAsleep) {
			//Generate number between 1 and 4
			switch (Random.Range (1,5)) {
			case 1:
                if (!doesNeedBeer)
                {
                    currentTasks++;
                    NeedBeer();
                }
				break;
			case 2:
                if (!doesTrashSmell)
                {
                    currentTasks++;
                    TrashSmells();
                }
				break;
			case 3:
                if (!isTVBroke)
                {
                    currentTasks++;
                    TVBreaks();
                }
				break;
			case 4:
                if (!isDogLoud)
                {
                    currentTasks++;
                    DogBarks();
                }
				break;
			}

			// case statement that enables a task's button and changes the environment 


		}

		//call idle for a random number of seconds
		StartCoroutine(Idle(Random.Range(lowerBound,upperBound)));
	}

	IEnumerator Idle(float t){
		if (!isDadAsleep) {
			yield return new WaitForSeconds (t);
		} else {
			yield return new WaitForSeconds (sleepTime);
            isDadAsleep = false;
		}
		CreateTask ();
	}

	public void SpawnKey()
	{
        buttons[0].SetActive(true);
        key.SetActive(true);
    }

	void KeyObtained()
	{
        key.SetActive(false);
        buttons[0].SetActive(false);
        buttons[5].SetActive(true);
        buttons[5].GetComponent<AudioSource>().Play();
       
    }

	void DoorUnlocked()
	{
        door.transform.RotateAround(pivot.transform.position,Vector3.back,90);
        buttons[0].SetActive(false);
        buttons[5].SetActive(false);
        
        objectivesCompleted = 4;
    }

	void Win()
	{
        SceneManager.LoadScene("WinGame");
    }

	void GameOver()
	{
        SceneManager.LoadScene("LoseGame");
	}

	void NeedBeer()
	{

        buttons[4].SetActive(true);
        doesNeedBeer = true;
        buttons[4].GetComponent<AudioSource>().Play();

    }

	void TrashSmells()
	{
		buttons[1].SetActive(true);
		doesTrashSmell = true;
        buttons[1].GetComponent<AudioSource>().Play();
        trashCan.GetComponent<SpriteRenderer>().sprite = trashcanSprite[1];
    }

	void TVBreaks()
	{
		buttons[3].SetActive(true);
		isTVBroke = true;
        buttons[3].GetComponent<AudioSource>().Play();
        tv.GetComponent<SpriteRenderer>().sprite = tvSprite[1];
    }

	void DogBarks()
	{
		buttons[2].SetActive(true);
		isDogLoud = true;
        buttons[2].GetComponent<AudioSource>().Play();
    }

    void dadAnimations()
    {
        if(!isDadAsleep)
        {
            if( anger < 25)
            {
                dad.GetComponent<SpriteRenderer>().sprite = AngryDad[0];
            }
            else if(anger > 25 &&  anger < 75)
            {
                dad.GetComponent<SpriteRenderer>().sprite = AngryDad[1];
            }
            else if (anger > 75)
            {
                dad.GetComponent<SpriteRenderer>().sprite = AngryDad[2];
            }
        }
        else
        {
            dad.GetComponent<SpriteRenderer>().sprite = AngryDad[3];
            
        }
    }
}
