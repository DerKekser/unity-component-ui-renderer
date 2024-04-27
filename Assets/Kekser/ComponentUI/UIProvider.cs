﻿using System;

namespace Kekser.ComponentUI
{
    public class UIProvider: UIFragment
    {
        public override void OnRender(Context ctx, Action<Context> children)
        {
            children?.Invoke(ctx);
        }
    }
}