using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimNameChanger : MonoBehaviour {

	public string[] ActionTokens = {
	
		"attack",
		"hurt",
		"dash",
	};
		
	public enum Direction{
		up,
		down,
		left,
		right
	};

	public DirectionToken[] DirectionTokens = {
		new DirectionToken("left",Direction.left),
		new DirectionToken("right",Direction.right),
		new DirectionToken("up",Direction.up),
		new DirectionToken("down",Direction.down),
	};

	public AnimationClip[] clips;

	public string FixNames (string clipName)
	{

		clipName = clipName.ToLower ();

		string actionName = null;
		string directionName = null;

		// find action name
		foreach (var action in ActionTokens) {
			if (clipName.Contains (action)) {
				actionName = action;
				break;
			}
		}

		// find direction name
		foreach (var item in DirectionTokens) {

			if (clipName.Contains (item.token)) {
				directionName = item.direction.ToString ();
				break;
			}
		}

		if (actionName == null || directionName == null)
			return clipName;
		//clip.name = actionName + "_" + directionName;

		return actionName + "_" + directionName;
	}

}

[System.Serializable]
public class DirectionToken{
	public string token;
	public AnimNameChanger.Direction direction;

	public DirectionToken (string token, AnimNameChanger.Direction direction)
	{
		this.token = token;
		this.direction = direction;
	}
}