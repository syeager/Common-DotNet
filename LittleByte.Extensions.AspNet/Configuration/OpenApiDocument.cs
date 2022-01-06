namespace LittleByte.Extensions.AspNet
{
    public readonly struct OpenApiDocument
    {
        public string Name { get; }
        public uint Version { get; }
        public string Title { get; }
        public string Description { get; }

        public OpenApiDocument(string name, uint version, string title, string description)
        {
            Name = name;
            Version = version;
            Title = title;
            Description = description;
        }
    }
}
