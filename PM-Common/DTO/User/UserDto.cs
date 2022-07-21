using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Common.DTO.User
{
    public class UserRightDto
    {
        public bool CanEditWorkhours { get; set; }
        public bool CanEditParkingRate { get; set;}
        public bool CanEditBlacklist { get; set; }
    }

    public class UserDTO
    {
        public Int64? UserId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }    

        public string? Phone { get; set; }

        public string? Email { get; set; }   
        public string? Username { get; set; }

        public UserRightDto? UserRights { get; set; }
    }
}
