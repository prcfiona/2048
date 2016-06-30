using UnityEngine;
using System.Collections;

public class Numbers : MonoBehaviour {
    public int value;     //数字的值
    public int positionX;   //数字在方格中的横坐标       
    public int positionY;   //数字在方格中的纵坐标 
    public UISprite mySprite;  //数字sprite
    public TweenPosition tp;    //控制数字的移动            

    public bool hasMixed=false;              //数字是否完成过一次合成
    private bool isMoving;             //数字是否在移动
    private bool toDestory=false;      //判断数字是否在移动完成之后需要被删除
    // Use this for initialization
    void Start ()
    {
        value = Random.value > 0.2f ? 2 : 4;
        mySprite.spriteName = value.ToString();
        do
        {
            positionX = Random.Range(0, 5);
            positionY = Random.Range(0, 5);
        }
        while ( Manager._instance.numbers[positionX, positionY] != null);

        transform.localPosition = new Vector2(-199 + positionX * 100, -187 + positionY * 100);
        Manager._instance.numbers[positionX, positionY] = this;   //保存位置

        tp.from = new Vector2(-199 + positionX * 100, -187 + positionY * 100);
        tp.to= new Vector2(-199 + positionX * 100, -187 + positionY * 100);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isMoving)
        {
            if (transform.localPosition != new Vector3(-199 + positionX * 100, -187 + positionY * 100))
            {
                isMoving = true;
                tp.from = transform.localPosition;
                tp.to = new Vector3(-199 + positionX * 100, -187 + positionY * 100);
                tp.ResetToBeginning();
                tp.PlayForward();
            }
        }
	}

    public void Move(int directionX,int directionY)
    {
        if (directionX == 1)
        {
            //Debug.Log("right");
            int index = 1;
            while (Manager._instance.isEmpty(positionX+index, positionY))
            {
                index++;
            }
            if (index > 1)
            {
                if(!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                Manager._instance.hasMove = true;
                Manager._instance.numbers[positionX, positionY] = null;
                positionX = positionX + index - 1;
                Manager._instance.numbers[positionX, positionY]=this;
            }

            if(positionX<4 && value == Manager._instance.numbers[positionX + 1, positionY].value && Manager._instance.numbers[positionX + 1, positionY].hasMixed==false)
            {
                Manager._instance.numbers[positionX + 1, positionY].hasMixed = true;
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                toDestory = true;
                Manager._instance.numbers[positionX + 1, positionY].value *= 2;
                Manager._instance.numbers[positionX, positionY] = null;
                positionX += 1;
            }
        }
        else if (directionX == -1)
        {
            //Debug.Log("left");
            int index = 1;
            while (Manager._instance.isEmpty(positionX-index, positionY))
            {
                index++;
            }
            if (index > 1)
            {
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                Manager._instance.numbers[positionX, positionY] = null;
                positionX = positionX - index + 1;
                Manager._instance.numbers[positionX, positionY] = this;
            }

            if (positionX >0 && value == Manager._instance.numbers[positionX - 1, positionY].value && Manager._instance.numbers[positionX - 1, positionY].hasMixed==false)
            {
                Manager._instance.numbers[positionX - 1, positionY].hasMixed = true;
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                toDestory = true;
                Manager._instance.numbers[positionX - 1, positionY].value *= 2;
                Manager._instance.numbers[positionX, positionY] = null;
                positionX -= 1;
            }
        }
        else if (directionY == 1)
        {
            //Debug.Log("up");
            int index = 1;
            while (Manager._instance.isEmpty(positionX, positionY+index))
            {
                index++;
            }
            if (index > 1)
            {
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                Manager._instance.numbers[positionX, positionY] = null;
                positionY = positionY + index - 1;
                Manager._instance.numbers[positionX, positionY] = this;
            }

            if (positionY < 4 && value == Manager._instance.numbers[positionX , positionY + 1].value && Manager._instance.numbers[positionX, positionY + 1].hasMixed==false)
            {
                Manager._instance.numbers[positionX, positionY + 1].hasMixed = true;
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                toDestory = true;
                Manager._instance.numbers[positionX, positionY + 1].value *= 2;
                Manager._instance.numbers[positionX, positionY] = null;
                positionY += 1;
            }
        }
        else if (directionY == -1)
        {
            //Debug.Log("down");
            int index = 1;
            while (Manager._instance.isEmpty(positionX, positionY - index))
            {
                index++;
            }
            if (index > 1)
            {
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                Manager._instance.numbers[positionX, positionY] = null;
                positionY = positionY - index + 1;
                Manager._instance.numbers[positionX, positionY] = this;
            }

            if (positionY >0 && value == Manager._instance.numbers[positionX, positionY - 1].value && Manager._instance.numbers[positionX, positionY - 1].hasMixed==false)
            {
                Manager._instance.numbers[positionX, positionY - 1].hasMixed = true;
                Manager._instance.hasMove = true;
                if (!Manager._instance.isMovingNumbers.Contains(this))
                {
                    Manager._instance.isMovingNumbers.Add(this);
                }
                toDestory = true;
                Manager._instance.numbers[positionX, positionY - 1].value *= 2;
                Manager._instance.numbers[positionX, positionY] = null;
                positionY -= 1;
            }
        }
    }
        
    public void MoveOver()
    {
        isMoving = false;
        if (toDestory)
        {
            Destroy(this.gameObject);
            Manager._instance.numbers[positionX, positionY].mySprite.spriteName = Manager._instance.numbers[positionX, positionY].value.ToString();
        }
        Manager._instance.isMovingNumbers.Remove(this);
    }
}
