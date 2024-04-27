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
            /*GetSize(style.Width, out float width, out SizeType widthType);
            GetSize(style.Height, out float height, out SizeType heightType);
            GetSize(style.Top, out float top, out SizeType topType);
            GetSize(style.Left, out float left, out SizeType leftType);
            GetSize(style.Right, out float right, out SizeType rightType);
            GetSize(style.Bottom, out float bottom, out SizeType bottomType);*/
            
            // TODO: Apply styles to RectTransform
            
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.sizeDelta = new Vector2(0, 0);
            
            LayoutRebuilder.MarkLayoutForRebuild(_rectTransform.parent as RectTransform);
        }
        
    }
}