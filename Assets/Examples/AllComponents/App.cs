using System;
using Examples.AllComponents.Components;
using Examples.AllComponents.Pages;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentUI;
using Kekser.ComponentSystem.ComponentUI.Components;
using Kekser.ComponentSystem.ComponentUI.UIProps;
using UnityEngine;
using UnityEngine.UIElements;
using Box = Kekser.ComponentSystem.ComponentUI.Components.Box;
using Button = Kekser.ComponentSystem.ComponentUI.Components.Button;
using Label = Kekser.ComponentSystem.ComponentUI.Components.Label;
using ScrollView = Kekser.ComponentSystem.ComponentUI.Components.ScrollView;

namespace Examples.AllComponents
{
    public class AppProps : StyleProps
    {
        public OptionalValue<App.Pages> page { get; set; } = new();
    }
    
    public class App: UIComponent<AppProps>
    {
        public enum Pages
        {
            Button,
            DropdownField,
            Foldout,
            GroupBox,
            Label,
            MinMaxSlider,
            Portal,
            ProgressBar,
            RadioButton,
            RadioButtonGroup,
            ScrollView,
            Slider,
            SliderInt,
            TextField,
            Toggle,
        }
        
        public override AppProps DefaultProps { get; } = new AppProps()
        {
            style = new Style() { height = new StyleLength(Length.Percent(100)) },
            page = Pages.Button
        };
        
        public override void OnRender()
        {
            _<Layout, StyleProps>(
                props: new StyleProps() { style = new Style()
                {
                    height = new StyleLength(Length.Percent(100)),
                }},
                render: () =>
                {
                    _<ScrollView, StyleProps>(
                        props: new StyleProps() { style = new Style() { width = new StyleLength(200) } },
                        render: () =>
                        {
                            Each((Pages[])Enum.GetValues(typeof(Pages)), (page, index) =>
                            {
                                _<Button, ButtonProps>(
                                    key: index.ToString(),
                                    props: new ButtonProps()
                                    {
                                        onClick = new Action(() => Props.Set(new AppProps() { page = (Pages)page })),
                                        className = "mb-5 bg-white w-[100%] p-10 text-left hover:bg-[#f0f0f0]",
                                    },
                                    render: () =>
                                    {
                                        _<Label, LabelProps>(
                                            props: new LabelProps() { text = page.ToString() }
                                        );
                                    }
                                );
                            });
                        }
                    );
                    _<Box, StyleProps>(
                        props: new StyleProps() { style = new Style()
                        {
                            flexGrow = new StyleFloat(1),
                            marginLeft = new StyleLength(10),
                            minWidth = new StyleLength(600),
                        }},
                        render: () =>
                        {
                            _<Label, LabelProps>(
                                props: new LabelProps()
                                {
                                    text = Enum.GetName(typeof(Pages), (Pages)OwnProps.page),
                                    style = new Style()
                                    {
                                        fontSize = new StyleLength(20),
                                    }
                                }
                            );
                            switch ((Pages)OwnProps.page)
                            {
                                case Pages.Button:
                                    _<ButtonPage>();
                                    break;
                                case Pages.DropdownField:
                                    _<DropdownFieldPage>();
                                    break;
                                case Pages.Foldout:
                                    _<FoldoutPage>();
                                    break;
                                case Pages.GroupBox:
                                    _<GroupBoxPage>();
                                    break;
                                case Pages.Label:
                                    _<LabelPage>();
                                    break;
                                case Pages.MinMaxSlider:
                                    _<MinMaxSliderPage>();
                                    break;
                                case Pages.Portal:
                                    _<PortalPage>();
                                    break;
                                case Pages.ProgressBar:
                                    _<ProgressBarPage>();
                                    break;
                                case Pages.RadioButton:
                                    _<RadioButtonPage>();
                                    break;
                                case Pages.RadioButtonGroup:
                                    _<RadioButtonGroupPage>();
                                    break;
                                case Pages.ScrollView:
                                    _<ScrollViewPage>();
                                    break;
                                case Pages.Slider:
                                    _<SliderPage>();
                                    break;
                                case Pages.SliderInt:
                                    _<SliderIntPage>();
                                    break;
                                case Pages.TextField:
                                    _<TextFieldPage>();
                                    break;
                                case Pages.Toggle:
                                    _<TogglePage>();
                                    break;
                            }
                        }
                    );
                }
            );
        }
    }
}