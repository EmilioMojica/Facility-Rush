using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public Number numberToPut;
    public Goal goal;

    private void OnMouseDown()
    {
        //放上去選擇到的數字
        SpawnNumber(GetSquareClicked());
        if (GetSquareClicked().y == 0)
        {
            goal.SetVariable(numberToPut);
        }
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //得到的座標是用Pixel去算的
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos); //從攝影機的pixel座標轉換成世界座標

        Vector2 gridPos = SnapToGrid(worldPos); //為了要讓Cactus生成在格子上，必須把世界座標轉換成interger才準確
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 worldPos)
    {
        float newX = Mathf.RoundToInt(worldPos.x);
        float newY = Mathf.RoundToInt(worldPos.y);

        return new Vector2(newX, newY);
    }

    private void SpawnNumber(Vector2 coordinate)
    {
        Instantiate(numberToPut, coordinate, Quaternion.identity);
    }

    public void SetSelectedNumber(Number numberToSelect)
    {
        numberToPut = numberToSelect;
    }
}
