using DTO.Objs;
using Shared;
using SqlKata;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.DAO.Objs
{
    public class PlayerSessionDAO : DAOBase<PlayerSessionDTO>
    {
        public override void ApplyFilterToQuery(List<FilterDto> filter, Query query)
        {
            throw new NotImplementedException();
        }

        public override void BuildQuery(QueryType QT, out Query query, PlayerSessionDTO dto = null)
        {
            query = new Query("account as acc");

            switch (QT)
            {
                case QueryType.SELECT:
                    query.Select("acc.id", "acc.username", "acc.password", "acc.gender", "acc.email", "acc.group_id", "acc.slots", "acc.pincode");
                    break;
            }

        }

        public override PlayerSessionDTO Dyn2Dto(dynamic dyn)
        {
            var dto = new PlayerSessionDTO()
            {
                AccountId = (uint)dyn.id,
                Username = dyn.username,
                Gender = dyn.gender,
                Email = dyn.email,
                CharSlots = dyn.slots,
                Pincode = dyn.pincode
            };

            return dto;
        }

        public override List<PlayerSessionDTO> FilterLDTO(List<FilterDto> filter, int page = 0)
        {
            throw new NotImplementedException();
        }

        public override PlayerSessionDTO GetDto(dynamic dyn)
        {
            BuildQuery(QueryType.SELECT, out var query);

            string username = dyn.Username;
            string password = dyn.Password;

            query.Where("acc.username", username).Where("acc.password", password);

            var o = Factory.FromQuery(query).Get().FirstOrDefault();

            if(o != null)
            {
                return Dyn2Dto(o);
            }

            return null;
        }
    }
}
