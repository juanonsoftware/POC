namespace StateDemoConsole
{
    public class NewArticle
    {
        public State State { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public NewArticle(string title, string description)
        {
            Title = title;
            Description = description;
            State = State.New;
        }
    }
}