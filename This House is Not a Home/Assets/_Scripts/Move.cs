using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	public float transitionTime;
	public Transform thingToMove;
	public Transform[] pointsList;
	public GameManager gm;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//finds path from current position to target position my recursively calling TransLerp for single increment movements
	public IEnumerator findPath(int currentPosition, int targetPosition) {
        GameManager.instance.isTravelling = true;

		if (currentPosition < targetPosition) {
            yield return StartCoroutine(TransLerp (currentPosition, currentPosition + 1));     //will probably run asynchronously and cause all kinds of problems
            yield return StartCoroutine((findPath(currentPosition + 1, targetPosition)));
		} else if (currentPosition > targetPosition) {
            yield return StartCoroutine(TransLerp (currentPosition, currentPosition - 1)); //here aswell
            yield return StartCoroutine((findPath(currentPosition - 1, targetPosition)));
        }

        GameManager.instance.isTravelling = false;
	}

	//Moves transform of thingToMove directly from current position to target position over a period of transitionTime
	IEnumerator TransLerp(int currentPosition, int targetPosition)
	{
		float t = 0f;
		
		while(t < transitionTime)
		{
			t += Time.deltaTime;
			thingToMove.position = Vector2.Lerp(pointsList[currentPosition].position, pointsList[targetPosition].position, t);
			//TODO: INSERT WALKING ANIMATION HERE
			yield return null;
			
		}

		gm.currentPosition = targetPosition;
		//selectionComplete = true;	//From code I copied but I don't think it applies here
	}
}
