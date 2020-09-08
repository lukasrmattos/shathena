using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using Shared;
using SqlKata;

namespace Database.DAO
{
    public abstract class DAOBase<DTO> : IDisposable
    {
        public MySqlRo Sql;
        public QueryFactory Factory;

        public abstract void BuildQuery(QueryType QT, out Query query, DTO dto = default);
        public abstract DTO GetDto(dynamic dyn);
        public abstract List<DTO> FilterLDTO(List<FilterDto> filter, int page = 0);
        public abstract void ApplyFilterToQuery(List<FilterDto> filter, Query query);
        public abstract DTO Dyn2Dto(dynamic dyn);

        public DAOBase()
        {
            Sql = DatabaseRo.Sql();
            Factory = Sql.NewQuery();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool v)
        {
            if (v)
            {
                if (Sql.Con != null) { Sql.Dispose(); Sql.Con = null; }
                if (Factory != null) { Factory.Dispose(); Factory = null; }
            }
        }

        ~DAOBase()
        {
            Dispose(false);
        }

    }
}
