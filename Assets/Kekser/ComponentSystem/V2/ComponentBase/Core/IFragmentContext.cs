namespace Kekser.ComponentSystem.V2.ComponentBase.Core
{
    public interface IFragmentContext
    {
        IFragment Fragment { get; }
        int? Key { get; }
        void SetKey(int? key = null);
        void SetProps(object props = null);
        IFragmentContext[] Children { get; }
        IFragmentContext InternalChildren { get; }
        void SetChildren(params IFragmentContext[] children);
        void AddChildren(params IFragmentContext[] children);
        IFragmentContext _(params IFragmentContext[] children);
    }
}