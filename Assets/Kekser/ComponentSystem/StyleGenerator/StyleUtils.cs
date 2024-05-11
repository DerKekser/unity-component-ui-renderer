namespace Kekser.ComponentSystem.StyleGenerator
{
    public class StyleUtils
    {
        public string Concat(params string[] values)
        {
            return string.Join(" ", values);
        }
    }
}