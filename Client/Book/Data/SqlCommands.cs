using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data
{
    public static class SqlCommands
    {
        internal static string CountForPages = @"
	        SELECT count(*)
            FROM dbo.tBook tor
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
                    Name as 'Название',
                    Writer as 'Автор'
	            FROM dbo.tBook
	            WHERE 1 = 1
	            )
            SELECT *
            FROM OrderedRecords
            WHERE RowNumber BETWEEN @RowStart
		            AND @RowEnd order by RowNumber";

        internal static string DeleteCard = @"delete from  dbo.tBook where ID=@DelID";
        internal static string SelectByID = @"select Name, Writer from dbo.tBook where ID=@ID";
        internal static string Update = @"update dbo.tBook set Name=@Name, Writer=@Writer where ID=@ID";
        internal static string SaveNew = @"insert into dbo.tBook values (@Name, @Writer); select @@identity";
    }
}