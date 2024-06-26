﻿using System;
using Examples.AllComponents.Components;
using Examples.AllComponents.Pages;
using Kekser.ComponentSystem.ComponentBase.PropSystem;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
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
    public class App: UIComponent
    {
        private enum Pages
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
        
        private State<Pages> _page;
        
        protected override void OnMount()
        {
            _page = UseState(Pages.Button);
        }

        public override StyleProps DefaultProps { get; } = new StyleProps()
        {
            style = new Style() { height = new StyleLength(Length.Percent(100)) },
        };

        protected override void OnRender()
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
                                        onClick = new Action(() => _page.Value = page),
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
                                    text = Enum.GetName(typeof(Pages), _page.Value),
                                    style = new Style()
                                    {
                                        fontSize = new StyleLength(20),
                                    }
                                }
                            );
                            switch (_page.Value)
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