namespace OlxDataAccess.ChatMessages.Repository
{
    public class ChatMessagesRepository : BaseRepository<Chat_Message>, IChatMessagesRepository
    {
        public ChatMessagesRepository(OLXContext context) : base(context)
        {
        }

        public IQueryable<Chat_Message> GetSenderAndReceiverById(int senderID, int receiverID)
        {
            IQueryable<Chat_Message>? result = from cm in _context.Chat_Messages
                                               join sender in _context.Users on cm.Sender_ID equals sender.Id
                                               join receiver in _context.Users on cm.Receiver_ID equals receiver.Id
                                               where (cm.Sender_ID == senderID && cm.Receiver_ID == receiverID) || (cm.Sender_ID == receiverID && cm.Receiver_ID == senderID)
                                               select cm;
            return result;
        }
        //public IQueryable<Chat_Message> GetChatBySenderAndRecevierIds(int senderId, int receiverIdId)
        //{
        //    #region With Names
        //    //IQueryable<Chat_Message>? chat = from cm in _context.Chat_Messages
        //    //                                 join sender in _context.Users on cm.Sender_ID equals sender.Id
        //    //                                 join receiver in _context.Users on cm.Receiver_ID equals receiver.Id
        //    //                                 where (cm.Sender_ID == senderId && cm.Receiver_ID == receiverIdId) || (cm.Sender_ID == receiverIdId && cm.Receiver_ID == senderId)
        //    //                                 select cm;
        //    #endregion

        //    #region Only chat
        //    //IQueryable<Chat_Message>? chat = from cm in _context.Chat_Messages
        //    //                                 where (cm.Sender_ID == 1 && cm.Receiver_ID == 2) || (cm.Sender_ID == 2 && cm.Receiver_ID == 1)
        //    //                                 select cm;
        //    #endregion

        //    return from cm in _context.Chat_Messages
        //           where (cm.Sender_ID == senderId && cm.Receiver_ID == receiverIdId) || (cm.Sender_ID == senderId && cm.Receiver_ID == receiverIdId)
        //           select cm;
        //}

    }
}
