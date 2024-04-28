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

            return false;
        }
        
        public void Parse(Props props)
        {
            GetSize(props.Get("width", "auto"), out float width, out SizeType widthType);
            GetSize(props.Get("height", "auto"), out float height, out SizeType heightType);
            GetSize(props.Get("Top", "auto"), out float top, out SizeType topType);
            GetSize(props.Get("Right", "auto"), out float right, out SizeType rightType);
            GetSize(props.Get("Bottom", "auto"), out float bottom, out SizeType bottomType);
            GetSize(props.Get("Left", "auto"), out float left, out SizeType leftType);
            
            // TODO: Apply styles to RectTransform
            
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.sizeDelta = new Vector2(0, 0);
            
            LayoutRebuilder.MarkLayoutForRebuild(_rectTransform.parent as RectTransform);
        }
        
    }
}