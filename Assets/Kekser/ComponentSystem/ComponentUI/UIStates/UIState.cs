using System;
using Kekser.ComponentSystem.ComponentBase.StateSystem;
using UnityEngine.UIElements;

namespace Kekser.ComponentSystem.ComponentUI.UIStates
{
    public class UIState<T>: BaseState<T>
    {
        public UIState(Action setDirty, T defaultValue = default) : base(setDirty, defaultValue)
        {
        }
    }
}