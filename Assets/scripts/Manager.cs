using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
    public static Manager _instance;

    public GameObject welcomePage;    //欢迎界面
    public GameObject numberPerfab;   //数字的perfab
    public Numbers[,] numbers = new Numbers[5, 5];
    public List<Numbers> isMovingNumbers = new List<Numbers>();     //正在移动中的number

    public bool hasMove = false;            //是否发生移动
    private bool playMusic;
    public UITexture music;
    public Texture musicOn;
    public Texture musicOff;

    void Awake()
    {
        _instance = this;
    }
	// Use this for initialization
	void Start ()
    {
        Instantiate(numberPerfab);
        Instantiate(numberPerfab);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isMovingNumbers.Count == 0)
        {
            int dirX = 0;
            int dirY = 0;
            if (Input.GetKeyDown("up"))
            {
                dirY = 1;
            }
            else if (Input.GetKeyDown("down"))
            {
                dirY = -1;
            }
            else if (Input.GetKeyDown("left"))
            {
                dirX = -1;
            }
            else if (Input.GetKeyDown("right"))
            {
                dirX = 1;
            }
            MoveNumber(dirX, dirY);
        }

        if (hasMove && isMovingNumbers.Count == 0)
        {
            Instantiate(numberPerfab);
            hasMove = false;
            for(int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 5; y++)
                {
                    if (numbers[x, y] != null)
                    {
                        numbers[x, y].hasMixed = false;
                    }
                }
            }
        }
	}
    public void CloseWelconPage()
    {
        welcomePage.SetActive(false);
    }
    public void MoveNumber(int directionX,int directionY)
    {
        if (directionX == 1)
        {
            //Debug.Log("right");
            for(int y = 0; y< 5; y++)
            {
                for (int x = 3; x >= 0; x--)
                {
                    if (numbers[x,y]!=null)
                    {
                        numbers[x, y].Move(directionX, directionY);
                    }
                }
            }
        }
        else if (directionX == -1)
        {
            //Debug.Log("left");
            for (int y = 0; y < 5; y++)
            {
                for (int x = 1; x <5; x++)
                {
                    if (numbers [x, y] != null)
                    {
                        numbers[x, y].Move(directionX, directionY);
                    }
                }
            }
        }
        else if (directionY == 1)
        {
            //Debug.Log("up");
            for (int x = 0; x < 5; x++)
            {
                for (int y = 3; y >= 0; y--)
                {
                    if (numbers [x, y] != null)
                    {
                        numbers[x, y].Move(directionX, directionY);
                    }
                }
            }
        }
        else if (directionY == -1)
        {
//            Debug.Log("down");
            for (int x = 0; x < 5; x++)
            {
                for (int y = 1; y < 5; y++)
                {
                    if (numbers[x, y] != null)
                    {
                        numbers[x, y].Move(directionX, directionY);
                    }
                }
            }

        }
    }

    public bool isEmpty(int x,int y)
    {
        if (x<0||x>4||y<0||y>4)
        {
            return false;
        }
        else if (numbers[x, y] != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Restart()
    {
        for(int x = 0; x < 5; x++)
        {
            for(int y = 0; y < 5; y++)
            {
                if (numbers[x, y] != null)
                {
                    Destroy(numbers[x, y].gameObject);
                }
            }
        }
        Instantiate(numberPerfab);
        Instantiate(numberPerfab);
    }
}
