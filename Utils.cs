using System.Collections;
using System.Collections.Generic;
using System.Data;

using UnityEngine;

public class Utils
{
    public static int Col = 6;
    public static int Row = 6;
    public static int Speed = 40; // Block Speed
    public static int Color_Cnt = 6; // red, blue, white, green, yellow
    public static float Block_Size = 1f;

    public enum EShape { ENothing, EShape3, EShape4, EShape4_Box, EShapeMore5}
    public enum EFlowState { EDefault, EMoving, EChecking, ERemoving, EDown, EMakeItem }

    public static int PosToIndex(Vector3 pos)
    {
        int x = (int)Mathf.Floor(pos.x);
        int y = -(int)Mathf.Floor(pos.y);

        int index = x + Col * y;

        return index;
    }

    public static Vector3 IndexToPos(int index)
    {
        int x;
        int y;
        if (index < 0)
        {
            /*
             * -3 -2 -1
             *  0  1  2
             *  3  4  5
             */
            y = index / Col - 1;
            x = (index + Col * Mathf.Abs(y)) % Col;
        }
        else
        {
            x = index % Col;
            y = (index) / Col;
        }
        return new Vector3(x + Block_Size/2, -y + Block_Size/2, 0);
    }

    public static Vector3 PosToCenterPos(Vector3 pos)
    {
        Vector3 center_pos = new Vector3(0, 0, 0);

        pos.x = pos.x - Block_Size/2;
        pos.y = pos.y - Block_Size/2;

        center_pos.x = (int)Mathf.Floor(pos.x);
        center_pos.y = (int)Mathf.Floor(pos.y);

        center_pos.x = center_pos.x + Block_Size/2;
        center_pos.y = center_pos.y + Block_Size/2;

        return center_pos;
    }

    public static Vector3 GetDir(Vector3 origin, Vector3 target)
    {
        Vector3 result = Vector3.zero;
        Vector3 dir = target - origin;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {

            if (dir.x > 0)
                result = Vector3.right;

            else
                result = Vector3.left;
        }
        else
        {
            if (dir.y > 0)
                result = Vector3.up;

            else
                result = Vector3.down;
        }

        return result;
    }

    public static int GetDirIndex(int index, Vector3 dir)
    {
        if (dir == Vector3.up)
            index -= Col;
        else if (dir == Vector3.down)
            index += Col;
        else if (dir == Vector3.right)
            index += 1;
        else if (dir == Vector3.left)
            index -= 1;

        return index;
    }

    public static bool CanSwitch(int index_a, int index_b)
    {
        if (!IsInBlockRange(index_a))
            return false;

        if (!IsInBlockRange(index_b))
            return false;

        // Only Same Row, Same Col
        if ((index_a / Col == index_b / Col) || (index_a % Row == index_b % Row))
            return true;

        return false;
    }

    public static bool IsInBlockRange(int index)
    {
        if (index < 0) return false;

        if (index >= Col * Row) return false;

        return true;
    }
}
