using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class SupportTicket
    {
        public int ModuleId { get; set; }
        public int FunctionalityId { get; set; }

        public int TicketId { get; set; }

        public string Description { get; set; }

        public string Attachment { get; set; }
    }
}