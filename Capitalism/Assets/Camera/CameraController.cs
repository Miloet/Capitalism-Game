using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController self;
    Animator an;
    public State state = State.Default;
    static float timeSinceUpdate = 0;

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
    private void Update()
    {
        if(timeSinceUpdate > 0) timeSinceUpdate = Mathf.Max(timeSinceUpdate - Time.deltaTime, 0);
    }
    public static void UpdateCamera(State newState, bool ignoreTime = false)
    {
        if (ignoreTime) timeSinceUpdate = -1;
        if(newState != self.state && timeSinceUpdate <= 0)
        {
            timeSinceUpdate = 1;
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

