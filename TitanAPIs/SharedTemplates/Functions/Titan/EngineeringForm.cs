using Titan.Data;

namespace Titan.Functions
{
    public class EngineeringForm
    {
        public static int SaveEnquiry(string TicketType, string TicketTitle, int TicketPriority, string TicketDetails, int TicketStatus, string TicketCreatedBy, string TicketRasiedBy, string TicketAssignedDept, string EngineeringType)
        {
            var TitanData = new TitanEntities();
            var ticket = new TicketHeader
            {
                TicketType = TicketType,
                TicketTitle = TicketTitle,
                TicketPriority = TicketPriority,
                TicketDetails = TicketDetails,
                EngineeringType = EngineeringType,

                TicketStatus = TicketStatus,
                TicketCreatedBy = TicketCreatedBy,
                TicketRasiedDept = TicketRasiedBy,
                TicketAssignedDept = TicketAssignedDept,
                TicketCreatedDate = System.DateTime.Now
            };

            TitanData.TicketHeaders.Add(ticket);
            TitanData.SaveChanges();

            ticket.TicketId = ticket.TicketId;

            return ticket.TicketId;
        }
    }
}