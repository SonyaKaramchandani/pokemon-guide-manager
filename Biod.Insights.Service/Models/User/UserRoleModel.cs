using System.ComponentModel.DataAnnotations;

namespace Biod.Insights.Service.Models.User
{
    public class UserRoleModel
    {
        [Required] public string Id { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }
    }
}