using System;
using Examples.Todo.Components;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine.UIElements;
using ScrollView = Kekser.ComponentSystem.ComponentUI.Components.ScrollView;

namespace Examples.Todo.Pages
{
    public class OptionsProps: StyleProps
    {
        public ObligatoryValue<Action> onBack { get; set; } = new();
    }
    
    public class Options: UIComponent<OptionsProps>
    {
        private void HandleBack()
        {
            Action e = OwnProps.onBack;
            e?.Invoke();
        }
        
        public override void OnRender()
        {
            _<ScrollView>(
                render: () =>
                {
                    Each(new int[15], (x, i) => 
                    {
                        _<StyledButton, StyledButtonProps>(
                            key: i.ToString(),
                            props: new StyledButtonProps()
                            {
                                className = "mb-5 h-50 flex-shrink-0",
                                text = $"Option {i}"
                            }
                        );
                    });
                }
            );
            _<StyledButton, StyledButtonProps>(
                props: new StyledButtonProps()
                {
                    className = "mb-5 h-50 flex-shrink-0",
                    onClick = (Action)HandleBack,
                    text = "Back"
                }
            );
        }
    }
}