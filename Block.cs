using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    
    Vector3 mNextPos;
    Vector3 mDir;

    float mSpeed = Utils.Speed;
    public int nColor;
    bool mIsMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        // 
        int random = Random.Range(1, Utils.Color_Cnt);
        nColor = random;

        Color color = Color.red;
        switch (random)
        {
            case 1:
                color = Color.red;
                break;
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.blue;
                break;
            case 4:
                color = Color.green;
                break;
            case 5:
                color = Color.white;
                break;
        }
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void Update()
    {
        if (mIsMoving)
        {
            transform.position += mDir;

            if (mNextPos == transform.position)
            {
                mIsMoving = false;
            }
        }
    }

    public void Move(int to_index)
    {
        mNextPos = Utils.IndexToPos(to_index);
        mDir = (mNextPos - gameObject.transform.position) / mSpeed;
        mIsMoving = true;
    }

    public void Remove()
    {
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        gameObject.SetActive(true);
        Start();
    }
}
