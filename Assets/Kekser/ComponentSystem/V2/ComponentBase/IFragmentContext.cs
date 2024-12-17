namespace Kekser.ComponentSystem.V2.ComponentBase
{
    public interface IFragmentContext
    {
        int? Key { get; }
        void SetKey(int? key = null);
        void SetProps(object props = null);
        void SetChildren(params IFragmentContext[] children);
        void AddChildren(params IFragmentContext[] children);
        IFragmentContext _(params IFragmentContext[] children);
    }
}