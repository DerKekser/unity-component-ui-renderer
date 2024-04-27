using System;
using TMPro;

namespace Scenes.Kekser.ComponentUI.Components
{
    public sealed class Text: UIComponent
    {
        private TMP_Text _textField;

        public override void OnMount()
        {
            _textField = Node.gameObject.AddComponent<TextMeshProUGUI>();
        }
        
        public override void OnRender(Context ctx, Action<Context> children)
        {
            _textField.text = Props.Get("text", "");
            _textField.fontSize = Props.Get("fontSize", 14);
            _textField.color = Props.Get("color", UnityEngine.Color.black);
            _textField.alignment = Props.Get("alignment", TMPro.TextAlignmentOptions.Center);
        }
    }
}