using System.ComponentModel.DataAnnotations.Schema;
using WebCiro.Domain.Abstractions;
using WebCiro.Domain.AppEntities.Identity;

namespace WebCiro.Domain.AppEntities
{
    public class UserAndCompanyRelationship : Entity
    {
        public UserAndCompanyRelationship()
        {

        }
        public UserAndCompanyRelationship(string id, string appUserId, string companyId) : base(id)
        {
            AppUserId = appUserId;
            CompanyId = companyId;
        }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("Company")]
        public string CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
