using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Data
{
    public static class SqlCommands
    {
        internal static string CountForPages = @"
	        SELECT count(*)
            FROM dbo.tPeople tor
	        WHERE 1 = 1";
        internal static string GridPart1 = @"
            WITH OrderedRecords
            AS (
	            SELECT 
		            row_number() OVER (
			            ORDER BY ";

        internal static string GridByPagePart2 = @"
			            ) AS RowNumber,
		            ID as 'ID',
                    Family as 'Фамилия',
                    Name as 'Имя'
	            FROM dbo.tPeople
	            WHERE 1 = 1
	            )
            SELECT *
            FROM OrderedRecords
            WHERE RowNumber BETWEEN @RowStart
		            AND @RowEnd order by RowNumber";

        internal static string DeleteCard = @"delete from  dbo.tPeople where ID=@DelID";
        internal static string SelectByID = @"select Family, Name from dbo.tPeople where ID=@ID";
        internal static string Update = @"update dbo.tPeople set Family=@Family, Name=@Name where ID=@ID";
        internal static string SaveNew = @"insert into dbo.tPeople values (@Family, @Name); select @@identity";
    }
}
