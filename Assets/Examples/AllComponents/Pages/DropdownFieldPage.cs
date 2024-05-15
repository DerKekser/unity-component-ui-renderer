using System;
using System.Collections.Generic;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;

namespace Examples.AllComponents.Pages
{ 
    public class DropdownFieldPage: UIComponent
    {
        private State<string> _selected;
        
        public DropdownFieldPage()
        {
            _selected = CreateState("Option 1");
        }
        
        private void HandleChange(string selected)
        {
            _selected.Value = selected;
        }

        protected override void OnRender()
        {
            _<DropdownField, DropdownFieldProps>(
                props: new DropdownFieldProps()
                {
                    value = _selected.Value,
                    onChange = new Action<string>(HandleChange),
                    options = new List<string>()
                    {
                        "Option 1",
                        "Option 2",
                        "Option 3",
                        "Option 4",
                        "Option 5",
                    }
                }
            );
            _<Label, LabelProps>(
                props: new LabelProps() { text = $"Selected value: {_selected.Value}" }
            );
        }
    }
}