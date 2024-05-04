using Kekser.ComponentSystem.ComponentBase.PropSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI
{
    public struct StyleProps
    {
        public OptionalValue<Style> style { get; set; }
    }
    
    public struct Style
    {
        public OptionalValue<StyleEnum<Align>> alignContent { get; set; }
        public OptionalValue<StyleEnum<Align>> alignItems { get; set; }
        public OptionalValue<StyleEnum<Align>> alignSelf { get; set; }
        public OptionalValue<StyleColor> backgroundColor { get; set; }
        public OptionalValue<StyleBackground> backgroundImage { get; set; }
        public OptionalValue<StyleColor> borderBottomColor { get; set; }
        public OptionalValue<StyleLength> borderBottomLeftRadius { get; set; }
        public OptionalValue<StyleLength> borderBottomRightRadius { get; set; }
        public OptionalValue<StyleFloat> borderBottomWidth { get; set; }
        public OptionalValue<StyleColor> borderLeftColor { get; set; }
        public OptionalValue<StyleFloat> borderLeftWidth { get; set; }
        public OptionalValue<StyleColor> borderRightColor { get; set; }
        public OptionalValue<StyleFloat> borderRightWidth { get; set; }
        public OptionalValue<StyleColor> borderTopColor { get; set; }
        public OptionalValue<StyleLength> borderTopLeftRadius { get; set; }
        public OptionalValue<StyleLength> borderTopRightRadius { get; set; }
        public OptionalValue<StyleFloat> borderTopWidth { get; set; }
        public OptionalValue<StyleLength> bottom { get; set; }
        public OptionalValue<StyleColor> color { get; set; }
        public OptionalValue<StyleCursor> cursor { get; set; }
        public OptionalValue<StyleEnum<DisplayStyle>> display { get; set; }
        public OptionalValue<StyleLength> flexBasis { get; set; }
        public OptionalValue<StyleEnum<FlexDirection>> flexDirection { get; set; }
        public OptionalValue<StyleFloat> flexGrow { get; set; }
        public OptionalValue<StyleFloat> flexShrink { get; set; }
        public OptionalValue<StyleEnum<Wrap>> flexWrap { get; set; }
        public OptionalValue<StyleLength> fontSize { get; set; }
        public OptionalValue<StyleLength> height { get; set; }
        public OptionalValue<StyleEnum<Justify>> justifyContent { get; set; }
        public OptionalValue<StyleLength> left { get; set; }
        public OptionalValue<StyleLength> letterSpacing { get; set; }
        public OptionalValue<StyleLength> marginBottom { get; set; }
        public OptionalValue<StyleLength> marginLeft { get; set; }
        public OptionalValue<StyleLength> marginRight { get; set; }
        public OptionalValue<StyleLength> marginTop { get; set; }
        public OptionalValue<StyleLength> maxHeight { get; set; }
        public OptionalValue<StyleLength> maxWidth { get; set; }
        public OptionalValue<StyleLength> minHeight { get; set; }
        public OptionalValue<StyleLength> minWidth { get; set; }
        public OptionalValue<StyleFloat> opacity { get; set; }
        public OptionalValue<StyleEnum<Overflow>> overflow { get; set; }
        public OptionalValue<StyleLength> paddingBottom { get; set; }
        public OptionalValue<StyleLength> paddingLeft { get; set; }
        public OptionalValue<StyleLength> paddingRight { get; set; }
        public OptionalValue<StyleLength> paddingTop { get; set; }
        public OptionalValue<StyleEnum<Position>> position { get; set; }
        public OptionalValue<StyleRotate> rotate { get; set; }
        public OptionalValue<StyleScale> scale { get; set; }
        public OptionalValue<StyleEnum<TextOverflow>> textOverflow { get; set; }
        public OptionalValue<StyleTextShadow> textShadow { get; set; }
        public OptionalValue<StyleLength> top { get; set; }
        public OptionalValue<StyleTransformOrigin> transformOrigin { get; set; }
        public OptionalValue<StyleList<TimeValue>> transitionDelay { get; set; }
        public OptionalValue<StyleList<TimeValue>> transitionDuration { get; set; }
        public OptionalValue<StyleList<StylePropertyName>> transitionProperty { get; set; }
        public OptionalValue<StyleList<EasingFunction>> transitionTimingFunction { get; set; }
        public OptionalValue<StyleTranslate> translate { get; set; }
        public OptionalValue<StyleColor> unityBackgroundImageTintColor { get; set; }
        public OptionalValue<StyleEnum<ScaleMode>> unityBackgroundScaleMode { get; set; }
        public OptionalValue<StyleFont> unityFont { get; set; }
        public OptionalValue<StyleFontDefinition> unityFontDefinition { get; set; }
        public OptionalValue<StyleEnum<FontStyle>> unityFontStyleAndWeight { get; set; }
        public OptionalValue<StyleEnum<OverflowClipBox>> unityOverflowClipBox { get; set; }
        public OptionalValue<StyleLength> unityParagraphSpacing { get; set; }
        public OptionalValue<StyleInt> unitySliceBottom { get; set; }
        public OptionalValue<StyleInt> unitySliceLeft { get; set; }
        public OptionalValue<StyleInt> unitySliceRight { get; set; }
        public OptionalValue<StyleInt> unitySliceTop { get; set; }
        public OptionalValue<StyleEnum<TextAnchor>> unityTextAlign { get; set; }
        public OptionalValue<StyleColor> unityTextOutlineColor { get; set; }
        public OptionalValue<StyleFloat> unityTextOutlineWidth { get; set; }
        public OptionalValue<StyleEnum<TextOverflowPosition>> unityTextOverflowPosition { get; set; }
        public OptionalValue<StyleEnum<Visibility>> visibility { get; set; }
        public OptionalValue<StyleEnum<WhiteSpace>> whiteSpace { get; set; }
        public OptionalValue<StyleLength> width { get; set; }
        public OptionalValue<StyleLength> wordSpacing { get; set; }
    }
}