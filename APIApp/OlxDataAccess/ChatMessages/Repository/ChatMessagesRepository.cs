namespace OlxDataAccess.ChatMessages.Repository
{
    public class ChatMessagesRepository : BaseRepository<Chat_Message>, IChatMessagesRepository
    {
        public ChatMessagesRepository(OLXContext context) : base(context)
        {
        }
    }
}
