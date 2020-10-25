using System;

namespace StateDemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample state management");

            Console.WriteLine("--->Article 1");
            var article1 = new Article("Art 1", "Sample article");
            Console.WriteLine("State should be New: {0}", article1.State == State.New);

            article1.Perform(Action.Edit);
            Console.WriteLine("State should be New: {0}", article1.State == State.New);

            article1.Perform(Action.Validate);
            Console.WriteLine("State should be Validated: {0}", article1.State == State.Validated);

            article1.Perform(Action.Edit);
            Console.WriteLine("State should be Edited: {0}", article1.State == State.Edited);

            article1.Perform(Action.Edit);
            Console.WriteLine("State should be Edited: {0}", article1.State == State.Edited);

            article1.Perform(Action.Validate);
            article1.Perform(Action.Delete);
            Console.WriteLine("State should be Deleted: {0}", article1.State == State.Deleted);

            Console.WriteLine("--->Article 2");
            var article2 = new Article("Art 2", "Sample article")
            {
                State = State.Validated
            };
            Console.WriteLine("State should be Validated: {0}", article2.State == State.Validated);

            try
            {
                article2.Perform(Action.Restore);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("--->Article 3");
            var article3 = new NewArticle("Art 3", "Sample article");
            var sm3 = new NewArticleStateManager(article3);
            Console.WriteLine("State should be New: {0}", article3.State == State.New);

            sm3.Perform(Action.Validate);
            Console.WriteLine("State should be Validated: {0}", article3.State == State.Validated);

            Console.WriteLine("--->Article 4");
            var article4 = new NewArticle("Art 4", "Sample article")
            {
                State = State.Edited
            };
            var sm4 = new NewArticleStateManager(article4);
            Console.WriteLine("State should be Edited: {0}", article4.State == State.Edited);

            sm4.Perform(Action.Validate);
            Console.WriteLine("State should be Validated: {0}", article4.State == State.Validated);

            Console.ReadLine();
        }
    }
}
