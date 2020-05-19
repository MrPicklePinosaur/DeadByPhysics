using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup {

    public enum FitMode {
        Uniform,
        Width,
        Height
    }

    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public FitMode fitMode;

    public override void CalculateLayoutInputHorizontal() {
        base.CalculateLayoutInputHorizontal();

        float sqrt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrt);
        columns = Mathf.CeilToInt(sqrt);

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)columns - 2*spacing.x/(float)columns - (padding.left + padding.right)/(float)columns;
        float cellHeight = parentHeight / (float)rows - 2*spacing.y/(float)rows - (padding.top + padding.bottom)/(float)rows;

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int c = 0;
        int r = 0;

        for (int i = 0; i < rectChildren.Count; i++) {
            r = i / columns;
            c = i % columns;

            var child = rectChildren[i];
            var x = cellSize.x * c + spacing.x * c + padding.left;
            var y = cellSize.y * r + spacing.y * r + padding.top;
            SetChildAlongAxis(child, 0, x, cellSize.x);
            SetChildAlongAxis(child, 1, y, cellSize.y);

        }

    }

    public override void CalculateLayoutInputVertical() {
        
    }

    public override void SetLayoutHorizontal() {
        
    }

    public override void SetLayoutVertical() {
        
    }
    
}
