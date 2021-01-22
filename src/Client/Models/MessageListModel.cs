using System.Collections.Generic;

namespace Client.Models
{
    public class MessageListModel
    {
        public List<string> Messages { get; set; }

        public MessageListModel()
        {
            Messages = new System.Collections.Generic.List<string>();
        }
    }
}
