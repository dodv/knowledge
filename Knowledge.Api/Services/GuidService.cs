using System;

namespace Knowledge.Api.Services
{
    public class GuidService
    {
        public Guid Create() => RT.Comb.Provider.PostgreSql.Create();

        /*
         *  2022-02-08 From slisboa: Since we currently use ms sql, the recommendation is
         *  to use the mssql provider (https://github.com/richardtallent/RT.Comb#simple-usage).
         *  If there's no diff in the indexing then I would rather leave it as is since we
         *  plan to move to postgres anyways. Even if there is a substantial impact we're still
         *  not at a scale where it would be an issue and by the time it is an issue, we will
         *  already be on postgres and I'd to keep the guids consistent if we can instead of
         *  having a switch over point be evident in the generated guids
         */
    }
}
