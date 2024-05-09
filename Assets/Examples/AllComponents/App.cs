﻿using System;
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
using ListView = Kekser.ComponentSystem.ComponentUI.Components.ListView;
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
            ListView,
            MinMaxSlider,
            Portal,
            ProgressBar,
            RadioButton,
            RadioButtonGroup,
            Scroller,
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
                                        style = new Style()
                                        {
                                            paddingBottom = new StyleLength(5),
                                            paddingLeft = new StyleLength(5),
                                            paddingRight = new StyleLength(5),
                                            paddingTop = new StyleLength(5),
                                            marginBottom = new StyleLength(5),
                                            backgroundColor = new StyleColor(new Color(1f, 1f, 1f, 1)),
                                            borderTopRightRadius = new StyleLength(5),
                                            borderBottomRightRadius = new StyleLength(5),
                                            borderBottomLeftRadius = new StyleLength(5),
                                            borderTopLeftRadius = new StyleLength(5),
                                        }
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
                                case Pages.ListView:
                                    _<ListViewPage>();
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
                                case Pages.Scroller:
                                    _<ScrollerPage>();
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