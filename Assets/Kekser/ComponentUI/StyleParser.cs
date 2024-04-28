using Kekser.ComponentUI.PropSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Kekser.ComponentUI
{
    public class StyleParser
    {
        private enum SizeType
        {
            Auto,
            Pixel,
            Percent
        }
        
        private RectTransform _rectTransform;
        
        public StyleParser(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;
        }
        
        private bool GetSize(string value, out float size, out SizeType type)
        {
            size = 0;
            type = SizeType.Auto;
            
            if (string.IsNullOrEmpty(value)) value = "auto";
            
            if (value == "auto")
            {
                type = SizeType.Auto;
                return true;
            }
            
            if (value.EndsWith("px"))
            {
                type = SizeType.Pixel;
                return float.TryParse(value.Substring(0, value.Length - 2), out size);
            }
            
            if (value.EndsWith("%"))
            {
                type = SizeType.Percent;
                return float.TryParse(value.Substring(0, value.Length - 1), out size);
            }
            
            if (float.TryParse(value, out size))
            {
                type = SizeType.Pixel;
                return true;
            }

            return false;
        }
        
        public void Parse(Props props)
        {
            GetSize(props.Get("width", "auto"), out float width, out SizeType widthType);
            GetSize(props.Get("height", "auto"), out float height, out SizeType heightType);
            GetSize(props.Get("top", "auto"), out float top, out SizeType topType);
            GetSize(props.Get("right", "auto"), out float right, out SizeType rightType);
            GetSize(props.Get("bottom", "auto"), out float bottom, out SizeType bottomType);
            GetSize(props.Get("left", "auto"), out float left, out SizeType leftType);
            
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.sizeDelta = new Vector2(0, 0);
            
            RectTransform parent = _rectTransform.parent as RectTransform;
            if (parent == null)
                return;
            
            if (widthType == SizeType.Auto && leftType == SizeType.Auto && rightType == SizeType.Auto)
                return;
            
            //convert percent to pixels
            if (widthType == SizeType.Percent)
                width = parent.rect.width * width / 100;
            if (leftType == SizeType.Percent)
                left = parent.rect.width * left / 100;
            if (rightType == SizeType.Percent)
                right = parent.rect.width * right / 100;

            //convert auto to pixels
            if (widthType == SizeType.Auto)
                width = parent.rect.width;
            if (leftType == SizeType.Auto)
                left = 0;
            if (rightType == SizeType.Auto)
                right = 0;

            //ignore width if left and right are set
            if (rightType != SizeType.Auto && leftType != SizeType.Auto)
                width = parent.rect.width - left - right;
            
            //anchor right if right is set else anchor left
            if (rightType != SizeType.Auto)
                _rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, right, width);
            else
                _rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left, width);
            
            
            //convert percent to pixels
            if (heightType == SizeType.Percent)
                height = parent.rect.height * height / 100;
            if (topType == SizeType.Percent)
                top = parent.rect.height * top / 100;
            if (bottomType == SizeType.Percent)
                bottom = parent.rect.height * bottom / 100;

            //convert auto to pixels
            if (heightType == SizeType.Auto)
                height = parent.rect.height;
            if (topType == SizeType.Auto)
                top = 0;
            if (bottomType == SizeType.Auto)
                bottom = 0;

            //ignore width if left and right are set
            if (bottomType != SizeType.Auto && topType != SizeType.Auto)
                height = parent.rect.height - top - bottom;
            
            //anchor right if right is set else anchor left
            if (bottomType != SizeType.Auto)
                _rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bottom, height);
            else
                _rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, top, height);
            
            
            LayoutRebuilder.MarkLayoutForRebuild(_rectTransform.parent as RectTransform);
        }
        
    }
}