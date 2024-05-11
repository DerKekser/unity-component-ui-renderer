using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public class Style
    {
        public OptionalValue<StyleEnum<Align>> alignContent { get; set; } = new();
        public OptionalValue<StyleEnum<Align>> alignItems { get; set; } = new();
        public OptionalValue<StyleEnum<Align>> alignSelf { get; set; } = new();
        public OptionalValue<StyleColor> backgroundColor { get; set; } = new();
        public OptionalValue<StyleBackground> backgroundImage { get; set; } = new();
        public OptionalValue<StyleColor> borderBottomColor { get; set; } = new();
        public OptionalValue<StyleLength> borderBottomLeftRadius { get; set; } = new();
        public OptionalValue<StyleLength> borderBottomRightRadius { get; set; } = new();
        public OptionalValue<StyleFloat> borderBottomWidth { get; set; } = new();
        public OptionalValue<StyleColor> borderLeftColor { get; set; } = new();
        public OptionalValue<StyleFloat> borderLeftWidth { get; set; } = new();
        public OptionalValue<StyleColor> borderRightColor { get; set; } = new();
        public OptionalValue<StyleFloat> borderRightWidth { get; set; } = new();
        public OptionalValue<StyleColor> borderTopColor { get; set; } = new();
        public OptionalValue<StyleLength> borderTopLeftRadius { get; set; } = new();
        public OptionalValue<StyleLength> borderTopRightRadius { get; set; } = new();
        public OptionalValue<StyleFloat> borderTopWidth { get; set; } = new();
        public OptionalValue<StyleLength> bottom { get; set; } = new();
        public OptionalValue<StyleColor> color { get; set; } = new();
        public OptionalValue<StyleCursor> cursor { get; set; } = new();
        public OptionalValue<StyleEnum<DisplayStyle>> display { get; set; } = new();
        public OptionalValue<StyleLength> flexBasis { get; set; } = new();
        public OptionalValue<StyleEnum<FlexDirection>> flexDirection { get; set; } = new();
        public OptionalValue<StyleFloat> flexGrow { get; set; } = new();
        public OptionalValue<StyleFloat> flexShrink { get; set; } = new();
        public OptionalValue<StyleEnum<Wrap>> flexWrap { get; set; } = new();
        public OptionalValue<StyleLength> fontSize { get; set; } = new();
        public OptionalValue<StyleLength> height { get; set; } = new();
        public OptionalValue<StyleEnum<Justify>> justifyContent { get; set; } = new();
        public OptionalValue<StyleLength> left { get; set; } = new();
        public OptionalValue<StyleLength> letterSpacing { get; set; } = new();
        public OptionalValue<StyleLength> marginBottom { get; set; } = new();
        public OptionalValue<StyleLength> marginLeft { get; set; } = new();
        public OptionalValue<StyleLength> marginRight { get; set; } = new();
        public OptionalValue<StyleLength> marginTop { get; set; } = new();
        public OptionalValue<StyleLength> maxHeight { get; set; } = new();
        public OptionalValue<StyleLength> maxWidth { get; set; } = new();
        public OptionalValue<StyleLength> minHeight { get; set; } = new();
        public OptionalValue<StyleLength> minWidth { get; set; } = new();
        public OptionalValue<StyleFloat> opacity { get; set; } = new();
        public OptionalValue<StyleEnum<Overflow>> overflow { get; set; } = new();
        public OptionalValue<StyleLength> paddingBottom { get; set; } = new();
        public OptionalValue<StyleLength> paddingLeft { get; set; } = new();
        public OptionalValue<StyleLength> paddingRight { get; set; } = new();
        public OptionalValue<StyleLength> paddingTop { get; set; } = new();
        public OptionalValue<StyleEnum<Position>> position { get; set; } = new();
        public OptionalValue<StyleRotate> rotate { get; set; } = new();
        public OptionalValue<StyleScale> scale { get; set; } = new();
        public OptionalValue<StyleEnum<TextOverflow>> textOverflow { get; set; } = new();
        public OptionalValue<StyleTextShadow> textShadow { get; set; } = new();
        public OptionalValue<StyleLength> top { get; set; } = new();
        public OptionalValue<StyleTransformOrigin> transformOrigin { get; set; } = new();
        public OptionalValue<StyleList<TimeValue>> transitionDelay { get; set; } = new();
        public OptionalValue<StyleList<TimeValue>> transitionDuration { get; set; } = new();
        public OptionalValue<StyleList<StylePropertyName>> transitionProperty { get; set; } = new();
        public OptionalValue<StyleList<EasingFunction>> transitionTimingFunction { get; set; } = new();
        public OptionalValue<StyleTranslate> translate { get; set; } = new();
        public OptionalValue<StyleColor> unityBackgroundImageTintColor { get; set; } = new();
        public OptionalValue<StyleEnum<ScaleMode>> unityBackgroundScaleMode { get; set; } = new();
        public OptionalValue<StyleFont> unityFont { get; set; } = new();
        public OptionalValue<StyleFontDefinition> unityFontDefinition { get; set; } = new();
        public OptionalValue<StyleEnum<FontStyle>> unityFontStyleAndWeight { get; set; } = new();
        public OptionalValue<StyleEnum<OverflowClipBox>> unityOverflowClipBox { get; set; } = new();
        public OptionalValue<StyleLength> unityParagraphSpacing { get; set; } = new();
        public OptionalValue<StyleInt> unitySliceBottom { get; set; } = new();
        public OptionalValue<StyleInt> unitySliceLeft { get; set; } = new();
        public OptionalValue<StyleInt> unitySliceRight { get; set; } = new();
        public OptionalValue<StyleInt> unitySliceTop { get; set; } = new();
        public OptionalValue<StyleEnum<TextAnchor>> unityTextAlign { get; set; } = new();
        public OptionalValue<StyleColor> unityTextOutlineColor { get; set; } = new();
        public OptionalValue<StyleFloat> unityTextOutlineWidth { get; set; } = new();
        public OptionalValue<StyleEnum<TextOverflowPosition>> unityTextOverflowPosition { get; set; } = new();
        public OptionalValue<StyleEnum<Visibility>> visibility { get; set; } = new();
        public OptionalValue<StyleEnum<WhiteSpace>> whiteSpace { get; set; } = new();
        public OptionalValue<StyleLength> width { get; set; } = new();
        public OptionalValue<StyleLength> wordSpacing { get; set; } = new();
        
        //Wrapper setter
        
        public StyleLength borderRadius
        {
            set
            {
                borderBottomLeftRadius = value;
                borderBottomRightRadius = value;
                borderTopLeftRadius = value;
                borderTopRightRadius = value;
            }
        }

        public StyleLength borderLeftRadius
        {
            set
            {
                borderBottomLeftRadius = value;
                borderTopLeftRadius = value;
            }
        }
        
        public StyleLength borderRightRadius
        {
            set
            {
                borderBottomRightRadius = value;
                borderTopRightRadius = value;
            }
        }
        
        public StyleLength borderTopRadius
        {
            set
            {
                borderTopLeftRadius = value;
                borderTopRightRadius = value;
            }
        }
        
        public StyleLength borderBottomRadius
        {
            set
            {
                borderBottomLeftRadius = value;
                borderBottomRightRadius = value;
            }
        }
        
        public StyleColor borderColor
        {
            set
            {
                borderBottomColor = value;
                borderLeftColor = value;
                borderRightColor = value;
                borderTopColor = value;
            }
        }
        
        public StyleFloat borderSpacing
        {
            set
            {
                borderLeftWidth = value;
                borderRightWidth = value;
                borderBottomWidth = value;
                borderTopWidth = value;
            }
        }
        
        public StyleLength margin
        {
            set
            {
                marginBottom = value;
                marginLeft = value;
                marginRight = value;
                marginTop = value;
            }
        }
        
        public StyleLength padding
        {
            set
            {
                paddingBottom = value;
                paddingLeft = value;
                paddingRight = value;
                paddingTop = value;
            }
        }
        
        public StyleFloat border
        {
            set
            {
                borderBottomWidth = value;
                borderLeftWidth = value;
                borderRightWidth = value;
                borderTopWidth = value;
            }
        }
        
        public StyleFloat flex
        {
            set
            {
                flexGrow = value;
                flexShrink = value;
            }
        }
    }
}