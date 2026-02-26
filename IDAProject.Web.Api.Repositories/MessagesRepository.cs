using Microsoft.EntityFrameworkCore;
using DeclarationFactory.Web.Api.Models.Interfaces.Repositories;
using DeclarationFactory.Web.Db.MainDatabase;
using DeclarationFactory.Web.Helpers;
using DeclarationFactory.Web.Models.Dto.Documents;
using DeclarationFactory.Web.Models.Dto.Messages;
using DeclarationFactory.Web.Models.RequestModels.Messages;

namespace DeclarationFactory.Web.Api.Repositories
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DeclarationFactoryContext _dbContext;

        public MessagesRepository(DeclarationFactoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateFcmTokenAsync(int idUser, string token)
        {
            var dbRecord = await _dbContext.AspNetUsers.FirstAsync(x => x.Id == idUser);
            dbRecord.FcmToken = token;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetFcmTokenAsync(int idUser)
        {
            var dbRecord = await _dbContext.AspNetUsers.FirstAsync(x => x.Id == idUser);
            return dbRecord.FcmToken!;
        }

        public async Task<int> InsertMessageAsync(SaveUserMessageRequestModel newMessage)
        {
            var dbRecord = DataHelpers.CloneObjectWithIL<SaveUserMessageRequestModel, UserMessage>(newMessage);
            _dbContext.UserMessages.Add(dbRecord!);
            await _dbContext.SaveChangesAsync();

            return dbRecord!.Id;
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(int idUser)
        {
            var result = new List<Conversation>();
            // get the unique list of user messages where the specified user is either sender or receiver
            var conversations = await (from um in _dbContext.UserMessages
                                       where um.UserTo == idUser || um.UserFrom == idUser
                                       select new ConversationBaseModel
                                       {
                                           IdSender = um.UserFrom,
                                           IdReceipt = um.UserTo
                                       })
                                 .Distinct()
                                 .ToListAsync();

            var otherUsersList = new List<int>();

            foreach (var baseConversation in conversations)
            {
                var otherUser = baseConversation.IdReceipt == idUser ? baseConversation.IdSender : baseConversation.IdReceipt;

                if (!otherUsersList.Contains(otherUser))
                {
                    otherUsersList.Add(otherUser);
                }
            }

            foreach (var ou in otherUsersList)
            {
                var query = from um in _dbContext.UserMessages
                            where (um.UserTo == idUser || um.UserFrom == idUser) && (um.UserTo == ou || um.UserFrom == ou)
                            join otherUser in _dbContext.AspNetUsers on ou equals otherUser.Id into defOtherUser
                            from contact in defOtherUser.DefaultIfEmpty()
                            orderby um.CreatedDate descending
                            select new Conversation
                            {
                                IdSender = um.UserFrom,
                                IdReceipt = um.UserTo,
                                LastMessage = um.Message,
                                LastMessageDate = um.CreatedDate,
                                Title = contact.Employee.Name + " " + contact.Employee.Surname
                            };

                var conversation = await query.FirstOrDefaultAsync();

                if (conversation != null)
                {
                    result.Add(conversation);
                }
            }

            // final sort by the date
            result = result.OrderByDescending(x => x.LastMessageDate).ToList();

            return result;
        }

        public async Task<List<UserMessageDto>> GetConversationMessagesAsync(int idUser1, int idUser2)
        {
            var query = from um in _dbContext.UserMessages
                        where (um.UserTo == idUser1 && um.UserFrom == idUser2) || (um.UserTo == idUser2 && um.UserFrom == idUser1)
                        orderby um.CreatedDate ascending
                        select DataHelpers.CloneObjectWithIL<UserMessage, UserMessageDto>(um);

            var result = await query.ToListAsync();

            var imageMessages = await (from um in _dbContext.UserMessages
                                       join doc in _dbContext.Documents on um.Id equals doc.ReferenceId
                                       where ((um.UserTo == idUser1 && um.UserFrom == idUser2) || (um.UserTo == idUser2 && um.UserFrom == idUser1))
                                       && doc.DocumentTypeId == DocumentTypeConstants.Message_image
                                       select new
                                       {
                                           IdMessage = um.Id,
                                           IdDocument = doc.Id
                                       })
                                            .AsNoTracking()
                                            .ToListAsync();

            foreach (var imageMessage in imageMessages)
            {
                var msg = result.FirstOrDefault(x => x.Id == imageMessage.IdMessage);
                if (msg != null)
                {
                    msg.ReferenceDocumentId = imageMessage.IdDocument;
                }
            }

            return result;
        }

        public async Task<List<ContactInfo>> GetProposedContactsAsync(int idUser)
        {
            var result = new List<ContactInfo>();

            var usersCompanyId = await (from u in _dbContext.AspNetUsers
                                        where u.Id == idUser
                                        select u.Employee.CompanyId).FirstOrDefaultAsync();

            if (usersCompanyId > 0)
            {
                result = await (from u in _dbContext.AspNetUsers
                                where u.Id != idUser && u.Employee.CompanyId == usersCompanyId &&
                                (u.Employee.JobTypeId == 1 || u.Employee.JobTypeId == 2 || u.Employee.JobTypeId == 4)
                                select new ContactInfo
                                {
                                    Id = u.Id,
                                    FullName = u.Employee.Name + " " + u.Employee.Surname,
                                    Role = u.Employee.JobType.Name
                                }).ToListAsync();
            }
            return result;
        }

        public async Task<List<ContactInfo>> SearchContactsByKeywordAsync(int companyId, string keyword)
        {
            var result = new List<ContactInfo>();

            var query = from u in _dbContext.AspNetUsers
                        where u.Employee.CompanyId == companyId
                        select new
                        {
                            u.Id,
                            u.Employee.Name,
                            u.Employee.MiddleName,
                            u.Employee.Surname,
                            Role = u.Employee.JobType.Name
                        };

            if (!string.IsNullOrEmpty(keyword))
            {
                var words = keyword.Split(" ", StringSplitOptions.None | StringSplitOptions.RemoveEmptyEntries);
                if (words.Any())
                {
                    if (words.Length == 1)
                    {
                        var pattern = $"{words[0]}%";
                        query = query.Where(x =>
                            EF.Functions.Like(x.Name, pattern) ||
                            EF.Functions.Like(x.Surname, pattern));
                    }
                    else if (words.Length == 2)
                    {
                        query = query.Where(x => x.Name == words[0] || x.Surname == words[1]);
                        if (!query.Any())
                        {
                            query = query.Where(x => words.Contains(x.Name) || words.Contains(x.MiddleName) || words.Contains(x.Surname));
                        }
                    }
                    else if (words.Length == 3)
                    {
                        query = query.Where(x => x.Name == words[0] || x.MiddleName == words[1] || x.Surname == words[2]);
                    }
                    else
                    {
                        query = query.Where(x => words.Contains(x.Name) || words.Contains(x.MiddleName) || words.Contains(x.Surname));
                    }
                }
            }

            result = await (from ci in query
                            select new ContactInfo
                            {
                                Id = ci.Id,
                                FullName = ci.Name + " " + ci.Surname,
                                Role = ci.Role
                            }).ToListAsync();

            return result;
        }

        public async Task<List<UserMessageDto>> SearchUserMessagesAsync(SearchUserMessagesParams searchParams)
        {

            var result = new List<UserMessageDto>();
            IQueryable<UserMessage> query = _dbContext.UserMessages;

            if (searchParams.Id.HasValue)
            {
                query = query.Where(x => x.Id == searchParams.Id);
            }
            else
            {
                if (searchParams.FromDate.HasValue)
                {
                    if (searchParams.ToDate.HasValue)
                    {
                        query = query.Where(x => x.CreatedDate >= searchParams.FromDate && x.CreatedDate < searchParams.ToDate.Value.Date.AddDays(1));
                    }
                    else
                    {
                        query = query.Where(x => x.CreatedDate.Date == searchParams.FromDate);
                    }
                }
                else
                {
                    if (searchParams.LastXDays.HasValue)
                    {
                        query = query.Where(x => x.CreatedDate >= DateTime.Now.AddDays(-searchParams.LastXDays.Value));
                    }
                }
                if (searchParams.DispatcherId.HasValue)
                {
                    query = query.Where(x => x.UserFromNavigation.EmployeeId == searchParams.DispatcherId.Value || x.UserToNavigation.EmployeeId == searchParams.DispatcherId.Value);
                }
                if (searchParams.DriverWithId.HasValue)
                {
                    query = query.Where(x => x.UserFromNavigation.EmployeeId == searchParams.DriverWithId.Value || x.UserToNavigation.EmployeeId == searchParams.DriverWithId.Value);
                }

                if (!string.IsNullOrEmpty(searchParams.Keyword))
                {
                    query = query.Where(x => x.Message.ToUpper().Contains(searchParams.Keyword.ToUpper()));
                }
            }

            result = await (from um in query
                            select new UserMessageDto
                            {
                                Id = um.Id,
                                Message = um.Message,
                                CreatedDate = um.CreatedDate,
                                Sender = um.UserFromNavigation.Employee.Name + " " + um.UserFromNavigation.Employee.Surname,
                                Receiver = um.UserToNavigation.Employee.Name + " " + um.UserToNavigation.Employee.Surname,
                                SenderUser = um.UserFromNavigation.UserName,
                                ReceiverUser = um.UserToNavigation.UserName

                            }).ToListAsync();
            foreach (var message in result)
            {
                var files = await (from doc in _dbContext.Documents
                                  where doc.ReferenceId == message.Id && doc.IsDeleted == false && doc.DocumentTypeId == DocumentTypeConstants.Message_image
                                  select new
                                  {
                                      File = doc.DownloadFileName ?? string.Empty
                                  })
                    .ToListAsync();
                if (files.Any())
                {
                    message.File = files.FirstOrDefault()!.File;
                }
            }

            return result;


        }

    }
}