namespace OlxDataAccess.ChatMessages.Repository
{
    public interface IChatMessagesRepository : IBaseRepository<Chat_Message>
    {
        public IQueryable<Chat_Message> GetChatBySenderAndRecevierIds(int senderId, int receiverIdId);
    }
}
