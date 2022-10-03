namespace LittleByte.Common.Json
{
    public static class Json
    {
        public static IJsonService Service { get; set; } = new JsonNetService();

        /// <summary>
        ///     Shorthand for <see cref="Service" />
        /// </summary>
        public static IJsonService S => Service;
    }
}
