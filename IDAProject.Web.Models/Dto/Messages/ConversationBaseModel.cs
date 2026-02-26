namespace IDAProject.Web.Models.Dto.Messages
{
    public class ConversationBaseModel
    {
        public ConversationBaseModel()
        {            
            IdSender = 0;
            IdReceipt = 0;            
        }
        
        public int IdSender { get; set; }

        public int IdReceipt { get; set; }        
    }
}
