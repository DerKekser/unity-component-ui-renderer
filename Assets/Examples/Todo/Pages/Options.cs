using System;
using Examples.Todo.Components;
using Kekser.ComponentSystem.ComponentBase;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.PropSystem.Rework;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using UnityEngine.UIElements;

namespace Examples.Todo.Pages
{
    public struct OptionsProps
    {
        public ObligatoryValue<Action> onBack { get; set; }
        public OptionalValue<Style> style { get; set; }
    }
    
    public class Options: UIComponent<OptionsProps>
    {
        private void HandleBack()
        {
            Action e = Props.Get<OptionsProps>().onBack;
            e?.Invoke();
        }
        
        public override void OnRender(BaseContext<VisualElement> ctx)
        {
            ctx._<ScrollArea>(
                render: ctx =>
                {
                    ctx.Each(new int[15], (x, i) => 
                    {
                        ctx._<StyledButton, StyledButtonProps>(
                            key: i.ToString(),
                            props: new StyledButtonProps()
                            {
                                style = new Style()
                                {
                                    marginBottom = new StyleLength(i == 14 ? 0 : 5),
                                    height = new StyleLength(50),
                                    flexShrink = new StyleFloat(0f)
                                },
                                text = $"Option {i}"
                            }
                        );
                    });
                }
            );
            ctx._<StyledButton, StyledButtonProps>(
                props: new StyledButtonProps()
                {
                    style = new Style()
                    {
                        marginTop = new StyleLength(5),
                        height = new StyleLength(50),
                        flexShrink = new StyleFloat(0f)
                    },
                    onClick = (Action)HandleBack,
                    text = "Back"
                }
            );
        }
    }
}