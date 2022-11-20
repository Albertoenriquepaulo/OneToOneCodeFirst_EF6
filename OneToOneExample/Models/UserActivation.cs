using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OneToOneExample.Models
{
    public class UserActivation
    {
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public bool Active { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
