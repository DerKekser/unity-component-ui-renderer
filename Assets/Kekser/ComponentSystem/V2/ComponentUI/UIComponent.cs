using Kekser.ComponentSystem.V2.ComponentBase;
using Kekser.ComponentSystem.V2.ComponentUI.Components;
using UnityEngine.UIElements;
using Label = Kekser.ComponentSystem.V2.ComponentUI.Components.Label;

namespace Kekser.ComponentSystem.V2.ComponentUI
{
    public abstract class UIComponent : BaseComponent<VisualElement>
    {
        protected static IFragmentContext _(
            string text,
            int? key = null
        )
        {
            return _<Label, LabelProps>(
                key: key,
                props: new LabelProps()
                {
                    text = text
                }
            );
        }
    }
    
    public abstract class UIComponent<TProps> : BaseComponent<VisualElement, TProps> where TProps : class, new()
    {
        protected static IFragmentContext _(
            string text,
            int? key = null
        )
        {
            return _<Label, LabelProps>(
                key: key,
                props: new LabelProps()
                {
                    text = text
                }
            );
        }
    }
    
    public abstract class UIComponent<TObject, TProps> : BaseComponent<VisualElement, TObject, TProps> where TObject : VisualElement, new() where TProps : class, new()
    {
        protected static IFragmentContext _(
            string text,
            int? key = null
        )
        {
            return _<Label, LabelProps>(
                key: key,
                props: new LabelProps()
                {
                    text = text
                }
            );
        }
    }
}