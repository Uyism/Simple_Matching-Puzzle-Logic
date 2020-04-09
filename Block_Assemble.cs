using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AssembleInfo
{
    public Utils.EShape shape;
    public List<int> index_list;
}

public class Block_Assemble
{
    Dictionary<int, Block>  mBlockMap;
    List<int> mSameColorIndexList;
    List<int> mDonList;
    List<AssembleInfo> mAssembleList;

    public bool mNoAssemble = true;

    public void DoAssembleBlock()
    {
        mNoAssemble = true;
        for (int i = 0; i < Utils.Col * Utils.Row; i++)
        {
            List<int> same_color_list = EachAssemble(i);
            Utils.EShape shape = Block_Check.GetShape(same_color_list);

            if (shape != Utils.EShape.ENothing)
            {
                AssembleInfo assemble_info;
                assemble_info.shape = shape;
                assemble_info.index_list = same_color_list;
                mAssembleList.Add(assemble_info);
                mNoAssemble = false;
            }
        }
    }

    public void SetBlockMap(Dictionary<int, Block> block_map)
    {
        mBlockMap = block_map;
        mDonList = new List<int>();
        mAssembleList = new List<AssembleInfo>();
    }

    public List<int> EachAssemble(int index)
    {
        
        mSameColorIndexList = new List<int>();
        CalcSameColor(index);

        return mSameColorIndexList;

    }

    public void CalcSameColor(int index)
    {
        if ((index < 0) || (index > Utils.Col * Utils.Row))
            return;

        if (!mBlockMap.ContainsKey(index))
            return;

        if (mDonList.Contains(index))
            return;

        mSameColorIndexList.Add(index);
        mDonList.Add(index);

        int left_index = Utils.GetDirIndex(index, Vector3.left);
        if (index % Utils.Col != 0)
        {
            if (mBlockMap.ContainsKey(left_index))
            {
                if (mBlockMap[index].nColor == mBlockMap[left_index].nColor)
                    CalcSameColor(left_index);
            }
        }

        int right_index = Utils.GetDirIndex(index, Vector3.right);
        if (index % Utils.Col != Utils.Col -1)
        {
            if (mBlockMap.ContainsKey(right_index))
            {
                if (mBlockMap[index].nColor == mBlockMap[right_index].nColor)
                    CalcSameColor(right_index);
            }
        }

        int down_index = Utils.GetDirIndex(index, Vector3.down);
        if (down_index < Utils.Col * Utils.Row)
        {
            if (mBlockMap.ContainsKey(down_index))
            {
                if (mBlockMap[index].nColor == mBlockMap[down_index].nColor)
                    CalcSameColor(down_index);
            }
        }

        int up_index = Utils.GetDirIndex(index, Vector3.up);
        if (up_index > 0)
        {
            if (mBlockMap.ContainsKey(up_index))
            {
                if (mBlockMap[index].nColor == mBlockMap[up_index].nColor)
                    CalcSameColor(up_index);
            }
        }
    }

    public List<AssembleInfo> GetAssembleList()
    {
        return mAssembleList;
    }
}
