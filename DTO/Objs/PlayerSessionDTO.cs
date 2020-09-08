using System;
using System.Collections.Generic;
using System.Text;

namespace DTO.Objs
{
    public class PlayerSessionDTO : DTOBase
    {
        public uint AccountId { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public short GroupId { get; set; }
        public short CharSlots { get; set; }
        public string Pincode { get; set; }
    }
}
