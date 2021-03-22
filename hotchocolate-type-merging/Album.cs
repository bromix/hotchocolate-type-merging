using HotChocolate;
using HotChocolate.Types;

namespace hotchocolate_type_merging
{
    [ExtendObjectType("Album")]
    public class Album
    {
        [GraphQLDescription("My Custom Field")]
        public string MyCustomField([Parent] Album parent)
        {
            return "My Custom Field";
        }
    }
}