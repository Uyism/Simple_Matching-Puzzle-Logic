using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Check
{
    public static Utils.EShape GetShape(List<int> same_color_index_list)
    {
        if (same_color_index_list.Count < 3)
            return Utils.EShape.ENothing;

        if (same_color_index_list.Count >= 5)
            return Utils.EShape.EShapeMore5;

        return CheckShape(same_color_index_list);
    }

    public static Utils.EShape CheckShape(List<int> same_color_index_list)
    {
        int col = 0;
        int row = 0;
        int box = 0;
        int size = same_color_index_list.Count;
        
        // Stick Shape
        for (int i = 0; i < size - 1; i++)
        {
            if ((int)Mathf.Floor(same_color_index_list[i]) / Utils.Col == (int)Mathf.Floor(same_color_index_list[i + 1] / Utils.Col))
                row = row + 1;

            if (Mathf.Floor(same_color_index_list[i]) % Utils.Col == Mathf.Floor(same_color_index_list[i + 1] % Utils.Col))
                col = col + 1;
        }

        if (size == 3)
        {
            if ((row == 2) || (col == 2))
                return Utils.EShape.EShape3;
        }

        if (size == 4)
        {
            if ((row == 3) || (col == 3))
                return Utils.EShape.EShape4;

            /*
            if (IsBoxShape(same_color_index_list))
                return Utils.EShape.EShape4_Box;
            */
        }

        return Utils.EShape.ENothing;
    }

    static bool IsBoxShape(List<int> same_color_list)
    {
        // 차례대로 x간격, y간격에 부합할 경우 box로 간주
        // 1 2 3 
        // 5 6 7 
        // 9 10 11
        // x 간격 1, y 간격 2
        int size = same_color_list.Count;
        int row_dis = Utils.Col - (size / 2 - 1);

        same_color_list.Sort();
        for (int i = 0; i < size-1; i++)
        {
            int dis = same_color_list[i + 1] - same_color_list[i];
            if ((dis != 1) && (dis != row_dis))
                return false;
        }
        return true;
    }

}
