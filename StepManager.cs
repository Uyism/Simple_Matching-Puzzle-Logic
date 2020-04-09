using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    BlockManager mBlockManager;
    Block_Assemble mBlockAssemble;
    Block_Down mBlockDown;

    Utils.EFlowState mFlowState = Utils.EFlowState.EDefault;

    void Start()
    {
        mBlockAssemble = new Block_Assemble();
        mBlockDown = this.gameObject.GetComponent<Block_Down>();
        mBlockManager = this.gameObject.GetComponent<BlockManager>();
    }

    void Update()
    {
        switch (mFlowState)
        {
            case Utils.EFlowState.EDefault:
                break;

            case Utils.EFlowState.EChecking:
                Assemble();
                break;

            case Utils.EFlowState.ERemoving:
                RemoveBlock();
                break;

            case Utils.EFlowState.EMakeItem:
                // @TODO
                // MakeItem();
                break;

            case Utils.EFlowState.EDown:
                Down();
                break;
        }
    }

    public void ChangeState(Utils.EFlowState state)
    {
        mFlowState = state;
    }

    void Assemble()
    {
        Dictionary<int, Block> block_map = mBlockManager.GetBlockMap();

        // 현재 블록 상태 입력
        mBlockAssemble.SetBlockMap(block_map);
        mBlockAssemble.DoAssembleBlock();

        if (mBlockAssemble.mNoAssemble)
            ChangeState(Utils.EFlowState.EDefault);
        else
            ChangeState(Utils.EFlowState.ERemoving);
    }

    void RemoveBlock()
    {
        List<AssembleInfo> assemble_list = mBlockAssemble.GetAssembleList();
        mBlockManager.RemoveBlock(assemble_list);

        StartCoroutine("DelayTime", Utils.EFlowState.EDown);
        ChangeState(Utils.EFlowState.EDefault);
    }

    void MakeItem()
    {
        List<AssembleInfo> assemble_list = mBlockAssemble.GetAssembleList();
        foreach (AssembleInfo info in assemble_list)
        {
            if (info.shape != Utils.EShape.EShape3)
                mBlockManager.MakeItem(info);
        }
        ChangeState(Utils.EFlowState.EDown);
    }

    void Down()
    {
        Dictionary<int, Block> block_map = mBlockManager.GetBlockMap();
        mBlockDown.SetBlockMap(block_map);
        mBlockDown.DoDownBlock();
        StartCoroutine("DelayTime", Utils.EFlowState.EChecking);
        ChangeState(Utils.EFlowState.EDefault);
    }

    IEnumerator DelayTime(Utils.EFlowState state)
    {
        yield return new WaitForSeconds(0.5f);
        ChangeState(state);
    }
}
