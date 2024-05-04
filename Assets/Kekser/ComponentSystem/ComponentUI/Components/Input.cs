﻿using System;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.Components
{
    public struct InputProps
    {
        public OptionalValue<string> value { get; set; }
        public OptionalValue<Action<string>> onChange { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public sealed class Input: UIComponent<TextField, InputProps>
    {
        private void Change(ChangeEvent<string> eChangeEvent)
        {
            Action<string> e = Props.Get<InputProps>().onChange;
            e?.Invoke(eChangeEvent.newValue);
        }
        
        public override void OnMount()
        {
            FragmentNode.RegisterValueChangedCallback(Change);
        }
        
        public override void OnUnmount()
        {
            FragmentNode.UnregisterValueChangedCallback(Change);
        }

        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            InputProps props = Props.Get<InputProps>();
            if (props.value.IsSet)
                FragmentNode.value = props.value;
        }
    }
}