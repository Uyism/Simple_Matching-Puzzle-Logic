using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    public GameObject Block;
    // Start is called before the first frame update
    void Start()
    {
        List<Block> list_block = new List<Block>();
        for (int row = 0; row < Utils.Row; row++)
        {
           for (int col = 0; col < Utils.Col; col++)
           {
               int index = col + row * col;
               GameObject obj = CreateBlock();
               obj.transform.position = new Vector3(col + 0.5f , -row +  0.5f, 0);
               list_block.Add(obj.GetComponent<Block>());
           }
        }

        // 여분의 블록을 더 만들어둠
        // 새로 생성해야할 때 여기 만들어둔 블록을 사용
        for (int row = 0; row < Utils.Row; row++)
        {
            for (int col = 0; col < Utils.Col; col++)
            {
                int index = col + row * col;
                GameObject obj = CreateBlock();
                obj.SetActive(false);
                list_block.Add(obj.GetComponent<Block>());
            }
        }

        this.GetComponent<BlockManager>().SetBlockList(list_block);
    }

    // Update is called once per frame
    GameObject CreateBlock()
    {
        GameObject obj = Instantiate(Block);
        return obj;
    }
}
