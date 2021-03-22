using HotChocolate.Types;

namespace hotchocolate_type_merging
{
    [ExtendObjectType(Name = "Query")]
    public class Query
    {
        public Book GetBook() =>
            new Book
            {
                Title = "C# in depth.",
                Author = new Author
                {
                    Name = "Jon Skeet"
                }
            };
    }
}