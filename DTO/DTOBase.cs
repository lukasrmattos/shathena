using Newtonsoft.Json;
using System;

namespace DTO
{
    public abstract class DTOBase
    {
        public bool Equals(DTOBase dto)
        {
            var obj1 = JsonConvert.SerializeObject(this);
            var obj2 = JsonConvert.SerializeObject(dto);

            return obj1 == obj2;
        }
    }
}
