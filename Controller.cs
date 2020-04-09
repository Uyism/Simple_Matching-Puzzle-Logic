using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Controller : MonoBehaviour
{
    public Camera _Camera;

    Vector3 mBeginPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            mBeginPos = _Camera.ScreenToWorldPoint(pos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Input.mousePosition;
            Vector3 end_pos = _Camera.ScreenToWorldPoint(pos);
            Vector3 dir = Utils.GetDir(mBeginPos, end_pos);

            int begin_index = Utils.PosToIndex(mBeginPos);
            int end_index = Utils.GetDirIndex(begin_index, dir);

            // 위치 스위치
            if (Utils.CanSwitch(begin_index, end_index))
                this.GetComponent<BlockManager>().Move(begin_index, end_index);
        }
    }
}
