using System.Collections.Generic;

namespace MedikTapp.Models
{
    public class ServicesGroup : List<Services>
    {
        public string GroupName { get; set; }

        public ServicesGroup(string groupName, List<Services> services) : base(services)
        {
            GroupName = groupName;
        }
    }
}