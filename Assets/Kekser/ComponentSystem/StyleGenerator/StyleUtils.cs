namespace Kekser.ComponentSystem.StyleGenerator
{
    public static class StyleUtils
    {
        public static string Concat(params string[] values)
        {
            return string.Join(" ", values);
        }
    }
}