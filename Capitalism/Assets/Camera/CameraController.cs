using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController self;
    Animator an;
    public State state = State.Default;

    GameObject wall;
    public enum State 
    {
        Default,
        Selected,
        Inspect
    }
    // Start is called before the first frame update
    void Start()
    {
        self = this;
        an = GetComponent<Animator>();
        wall = GameObject.Find("WallPlane");
    }

    public static void UpdateCamera(State newState)
    {
        if(newState != self.state)
        {
            self.state = newState;

            self.wall.SetActive(true);

            switch(self.state)
            {
                case State.Default:
                    self.an.SetTrigger("Return");

                    break;

                case State.Inspect:
                    self.an.SetTrigger("Inspect");

                    break;

                case State.Selected:
                    self.an.SetTrigger("Select");
                    self.wall.SetActive(false);
                    break;
            }
        }
    }
}

