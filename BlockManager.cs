using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    StepManager mStepManager;

    List<Block> mListBlock; // 블록 저장소

    Dictionary<int, Block> mBlockMap = new Dictionary<int, Block>(); // 블록 위치 저장소
    public ref Dictionary<int, Block>  GetBlockMap()
    {
        return ref mBlockMap;
    }

    void Start()
    {
        mStepManager = this.GetComponent<StepManager>();    
    }

    public void SetBlockList(List<Block> list_block)
    {
        mListBlock = list_block;
        for (int i = 0; i < Utils.Col * Utils.Row; i++)
            mBlockMap[i] = mListBlock[i];
    }

    public void Move(int begin_index, int end_index)
    {
        Block begin_block = mBlockMap[begin_index];
        Block end_block = mBlockMap[end_index];

        // 블록 이동
        begin_block.Move(end_index);
        end_block.Move(begin_index);

        // 블록 위치 저장소 정보 갱신
        mBlockMap[begin_index] = end_block;
        mBlockMap[end_index] = begin_block;

        mStepManager.ChangeState(Utils.EFlowState.EChecking);  
    }

    public void RemoveBlock(List<AssembleInfo> same_color_list)
    {
        foreach (AssembleInfo info in same_color_list)
        {
            List<int> index_list = info.index_list;
            foreach (int index in index_list)
            {
                mBlockMap[index].Remove();
                mBlockMap[index] = null;
            }
        }
    }

    // @For Bomb Item
    public void MakeItem(AssembleInfo info)
    {
        foreach (int index in info.index_list)
        {
            Block block = FindSpareBlock();
            if (block == null) return;

            mBlockMap[index] = block;
            block.transform.position = Utils.IndexToPos(index);
            block.Revive();
            return;
        }
    }

    public Block MakeNewBlock(int index)
    {      
        Block block = FindSpareBlock();
        if (block == null) return null;

        block.transform.position = Utils.IndexToPos(index);
        mBlockMap[index] = block;
        block.Revive();
        return block;
    }

    Block FindSpareBlock()
    {
        foreach (Block block in mListBlock)
        {
            if (!block.gameObject.activeSelf)
                return block;
        }

        return null;
    }

}
