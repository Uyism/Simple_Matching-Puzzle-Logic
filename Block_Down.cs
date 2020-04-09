using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Down : MonoBehaviour
{
    Dictionary<int, Block> mBlockMap;
    Dictionary<int, bool> mExistMap;
    BlockManager mBlockManager;

    private void Start()
    {
        mBlockManager = GameObject.Find("SystemManager").GetComponent<BlockManager>();
    }

    public void SetBlockMap(Dictionary<int, Block> block_map)
    {
        mBlockMap = block_map;
        mExistMap = new Dictionary<int, bool>();
    }

    public void DoDownBlock()
    {
        // 1.움직일 맵을 미리 만듬 = move_map
        // 2.변동된 맵 정보 = mExistMap
        for (int index = Utils.Col * Utils.Row - 1; index >= 0; index--)
        {
            mExistMap[index] = (mBlockMap[index] != null);
        }

        // 1. 가장 끝 블록 부터 시작
        // 2. 블록이 없을 경우 위로 올라가면서 블록을 찾는다.
        // 3. 없다면 새로 만듬
        Dictionary<int, int> move_map = new Dictionary<int, int>();
        for (int index = Utils.Col * Utils.Row - 1; index >= 0; index--)
        {
            if (mExistMap[index] == false)
            {
                int upstair_index;
                upstair_index = FindUpStair(index);
                move_map[index] = upstair_index;
                mExistMap[index] = true;
                mExistMap[upstair_index] = false;
            }
        }


        Dictionary<int, int> accumulate_row_cnt = new Dictionary<int, int>();

        for (int index = Utils.Col * Utils.Row - 1; index >= 0; index--)
        {
            if (!move_map.ContainsKey(index))
                continue;

            // 인덱스가 음수인 경우 = 빈 자리 채울 블록이 없어서 새로 만들어야 하는 경우
            // 새로 만든 블록은 위에 쌓아서 내림 accumulate_row_cnt
            if (move_map[index] < 0)
            {
                // 새로 만들 블록 위치
                int row = index % Utils.Col;
                if (!accumulate_row_cnt.ContainsKey(row))
                    accumulate_row_cnt[row] = 0;

                int row_cnt = accumulate_row_cnt[row];

                // 블록 생성
                Block block = mBlockManager.MakeNewBlock(move_map[index] - row_cnt * Utils.Col);
                if (block == null) return;

                // 블록 이동
                accumulate_row_cnt[row] += 1;
                block.Move(index);
                mBlockMap[index] = block;
            }
            else
            {
                // 블록 스위칭
                int start_index = move_map[index];
                mBlockMap[start_index].Move(index);
                mBlockMap[index] = mBlockMap[start_index];
            }
        }

    }

    int FindUpStair(int index)
    {
        while (true)
        {
            index -= Utils.Col;
            if (index < 0)
                return index;

            if (mExistMap[index] == null)
                continue;

            if (mExistMap[index])
                return index;
        }
    
    }
}
