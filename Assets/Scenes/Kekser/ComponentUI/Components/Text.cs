﻿using System;
using TMPro;

namespace Scenes.Kekser.ComponentUI.Components
{
    public class Text: Component
    {
        private TMP_Text _textField;

        public override void OnMount()
        {
            _textField = Node.gameObject.AddComponent<TextMeshProUGUI>();
            _textField.text = Props.Get<TextProp>("text").Value;
            _textField.color = UnityEngine.Color.black;
        }
        
        public override void OnUnmount()
        {
            
        }

        public override void OnRender(Context ctx, Action<Context> children)
        {
            _textField.text = Props.Get<TextProp>("text").Value;
        }
    }
}