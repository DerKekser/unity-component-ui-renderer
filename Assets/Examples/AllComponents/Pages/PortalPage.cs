using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;

namespace Examples.AllComponents.Pages
{
    public class PortalPage: UIComponent
    {
        private State<VisualElement> _target;
        
        public PortalPage()
        {
            _target = CreateState<VisualElement>(null);
        }

        protected override void OnRender()
        {
            _<Group, StyleProps>(
                props: new StyleProps() {style = new Style()
                {
                    borderBottomColor = new StyleColor(new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1)),
                    borderBottomWidth = new StyleFloat(1f),
                    borderLeftColor = new StyleColor(new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1)),
                    borderLeftWidth = new StyleFloat(1f),
                    borderRightColor = new StyleColor(new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1)),
                    borderRightWidth = new StyleFloat(1f),
                    borderTopColor = new StyleColor(new UnityEngine.Color(0.5f, 0.5f, 0.5f, 1)),
                    borderTopWidth = new StyleFloat(1f),
                }},
                render: () =>
                {
                    _<Label, LabelProps>(
                        props: new LabelProps()
                        {
                            text = "It seems like it should be rendering here"
                        }
                    );
                    _<Portal, PortalProps>(
                        props: new PortalProps()
                        {
                            target = _target.Value
                        },
                        render: () =>
                        {
                            _<Label, LabelProps>(
                                props: new LabelProps()
                                {
                                    text = "But it's rendering here"
                                }
                            );
                        }
                    );
                }
            );

            _target.Value = _<Group>().Node;
        }
    }
}