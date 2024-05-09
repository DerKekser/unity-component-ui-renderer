using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;

namespace Examples.AllComponents.Pages
{
    public class PortalPageProps: StyleProps
    {
        public OptionalValue<VisualElement> target { get; set; } = new();
    }
    
    public class PortalPage: UIComponent<PortalPageProps>
    {
        public override void OnRender()
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
                            target = OwnProps.target
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

            Props.Set(new PortalPageProps()
            {
                target = _<Group>().FragmentNode
            });
        }
    }
}