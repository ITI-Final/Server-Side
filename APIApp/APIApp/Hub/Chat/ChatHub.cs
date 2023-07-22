namespace OlxDataAccess
{
    internal class ChatHub : Hub
    {
        #region Fileds
        private readonly OLXContext _oLXContext;
        #endregion

        #region Constructors
        public ChatHub(OLXContext oLXContext)
        {
            _oLXContext = oLXContext;
        }
        #endregion

        #region Method

        #region Send Message
        public async void SendMessage(Chat_Message chat)
        {
            #region Get Users
            User? sender = await _oLXContext.Users.Where(u => u.Id == chat.Sender_ID).FirstOrDefaultAsync();
            User? receiver = await _oLXContext.Users.Where(u => u.Id == chat.Receiver_ID).FirstOrDefaultAsync();

            if (sender == null || receiver == null) return;
            #endregion

            #region Get Recever Connections ID
            IReadOnlyList<string> listOfConnectionsID = _oLXContext.User_Connections.Where(uc => uc.User_ID == receiver.Id).Select(uc => uc.Connection_ID).ToImmutableList();
            #endregion

            #region Save Into Database
            try
            {
                await _oLXContext.Chat_Messages.AddAsync(chat);
                await _oLXContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

            #region Call Back Function
            await Clients.Clients(listOfConnectionsID).SendAsync("retrieveMessage", chat);
            #endregion
        }
        #endregion

        #region On Connected
        public override Task OnConnectedAsync()
        {
            if (Context.GetHttpContext().Request.Query.TryGetValue("userId", out var userId))
            {
                int userid = int.Parse(userId);

                _oLXContext.User_Connections.Add(new User_Connection { User_ID = userid, Connection_ID = Context.ConnectionId });
                _oLXContext.SaveChanges();
            }

            return base.OnConnectedAsync();
        }
        #endregion

        #region On Disconnected
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.GetHttpContext().Request.Query.TryGetValue("userId", out var userId))
            {
                int userid = int.Parse(userId);

                _oLXContext.User_Connections.Remove(new User_Connection { User_ID = userid, Connection_ID = Context.ConnectionId });
                _oLXContext.SaveChanges();
            }

            return base.OnDisconnectedAsync(exception);
        }
        #endregion

        #endregion
    }
}
