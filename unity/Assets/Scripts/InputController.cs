using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            BoxManager.I.Move(Direction.Left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            BoxManager.I.Move(Direction.Right);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            BoxManager.I.Move(Direction.Up);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            BoxManager.I.Move(Direction.Down);
        }
    }

    public void MoveLeft()
    {
        BoxManager.I.Move(Direction.Left);
    }

    public void MoveRight()
    {
        BoxManager.I.Move(Direction.Right);
    }

    public void MoveUp()
    {
        BoxManager.I.Move(Direction.Up);
    }

    public void MoveDown()
    {
        BoxManager.I.Move(Direction.Down);
    }
}
