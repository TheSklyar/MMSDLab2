using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleBooks.Data
{
    public static class SqlCommands
    {
        internal static string CountForPages = @"
	        SELECT count(*)
            FROM dbo.tPeopleBooks zak
inner join dbo.tbook b on b.ID=zak.BookID
inner join dbo.tPeople p on p.ID=zak.PeopleID
	        WHERE 1 = 1";

        internal static string GridPart1 = @"
            WITH OrderedRecords
            AS (
	            SELECT 
		            row_number() OVER (
			            ORDER BY ";

        internal static string GridByPagePart2 = @"
			            ) AS RowNumber,
		            zak.ID as 'ID',
					b.[Name] as 'Книга',
                    p.Family as 'Фамилия читателя'
	            FROM dbo.tPeopleBooks zak
inner join dbo.tbook b on b.ID=zak.BookID
inner join dbo.tPeople p on p.ID=zak.PeopleID
	            WHERE 1 = 1
	            )
            SELECT *
            FROM OrderedRecords
            WHERE RowNumber BETWEEN @RowStart
		            AND @RowEnd order by RowNumber";

        internal static string DeleteCard = @"delete from  dbo.tPeopleBooks where ID=@DelID";
        internal static string SaveNew = @"insert into dbo.tPeopleBooks values (@BookID, @PeopleID); select @@identity";
        
        internal static string GetAllDataByID = @"select [BookID],[PeopleID]
  from dbo.tPeopleBooks  where ID=@ID";

        internal static string SaveItem = @"
update dbo.tPeopleBooks set BookID=@BookID, PeopleID=@PeopleID where ID=@ID
";


    }
}
