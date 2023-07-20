namespace APIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        #region Fields
        protected readonly IChatMessagesRepository _chatMessagesRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ChatMessagesController(IChatMessagesRepository chatMessagesRepository, IMapper mapper)
        {
            _chatMessagesRepository = chatMessagesRepository;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        #region Get

        #region Get All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat_Message>>> GetAllChats(int? page)
        {
            int? pageSize = 10;
            if (page < 1 || pageSize < 1)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            int chat_MessagesCount = _chatMessagesRepository.GetAll().Result.Count();
            if (chat_MessagesCount == 0)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IEnumerable<Chat_Message> chat_Messages = await _chatMessagesRepository.GetAllWithPagination(page: page ?? 1, pageSize: pageSize ?? chat_MessagesCount);

            int totalPages = (int)Math.Ceiling((double)chat_MessagesCount / pageSize ?? chat_MessagesCount);
            if (totalPages < page)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, page ?? 1, totalPages, chat_MessagesCount, chat_Messages));
        }
        #endregion

        #region Get By ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat_Message>> GetChatById(int id)
        {
            if (await _chatMessagesRepository.GetAll() == null)
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            Chat_Message? chat_Message = await _chatMessagesRepository.GetById(id);

            if (chat_Message == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, chat_Message));
        }
        #endregion

        #region Get Chat By Sender or Resever
        [HttpGet("{sender}/{receiver}")]
        public async Task<ActionResult<Chat_Message>> GetSenderAndReceiverById(int sender, int receiver)
        {
            if (await _chatMessagesRepository.GetAll() == null || IsIDsValid(sender, receiver))
                return Ok(AppConstants.Response<string>(AppConstants.noContentCode, AppConstants.notContentMessage));

            IQueryable<Chat_Message>? chat_Message = _chatMessagesRepository.GetSenderAndReceiverById(sender, receiver);
            if (chat_Message == null)
                return NotFound(AppConstants.Response<string>(AppConstants.notFoundCode, AppConstants.notFoundMessage));

            return Ok(AppConstants.Response<object>(AppConstants.successCode, AppConstants.getSuccessMessage, 1, 1, 1, chat_Message));
        }

        #endregion

        #endregion

        #region Send Message

        [HttpPost]
        public async Task<ActionResult> SendMessage(ChatMessageDTO chatDto)
        {
            if (chatDto == null)
                return BadRequest(AppConstants.Response<string>(AppConstants.badRequestCode, AppConstants.invalidMessage));

            #region autoMapper
            Chat_Message? chat = _mapper.Map<Chat_Message>(chatDto);
            #endregion
            await _chatMessagesRepository.Add(chat);

            return Created("", AppConstants.Response<object>(AppConstants.successCode, AppConstants.addSuccessMessage, 1, 1, 1, chat));
        }
        #endregion

        #region Is IDs Vaild
        private static bool IsIDsValid(int senderId, int receiverId)
        {
            return senderId == receiverId || senderId == 0 || receiverId == 0;
        }
        #endregion

        #endregion

    }
}
