using Line_Production.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production.Database
{
    public class TimeLines
    {
        public const string TIME = "LINE_TIME";
        public const string TIMELINE = "LINE_TIMELINE";

        public TimeLine Insert(TimeLine o)
        {
            try
            {
                if (IsExist(o.IdTimeLine, o.TimeIndex) > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("update " + TIMELINE + " set IdTimeLine = @IdTimeLine,TimeIndex = @TimeIndex,TimeFrom = @TimeFrom,TimeTo = @TimeTo where IdTimeLine = " + o.IdTimeLine + " and TimeIndex = " + o.TimeIndex, DataProvider.Instance.DB))
                    {
                        cmd.Parameters.AddWithValue("@IdTimeLine", (o as TimeLine).IdTimeLine);
                        cmd.Parameters.AddWithValue("@TimeIndex", (o as TimeLine).TimeIndex);
                        cmd.Parameters.AddWithValue("@TimeFrom", (o as TimeLine).TimeFrom);
                        cmd.Parameters.AddWithValue("@TimeTo", (o as TimeLine).TimeTo);
                        (o as TimeLine).Id = (int)cmd.ExecuteScalar();
                        return o;
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand("insert into " + TIMELINE + "(IdTimeLine,TimeIndex,TimeFrom,TimeTo) values(@IdTimeLine,@TimeIndex,@TimeFrom,@TimeTo);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                    {
                        cmd.Parameters.AddWithValue("@IdTimeLine", (o as TimeLine).IdTimeLine);
                        cmd.Parameters.AddWithValue("@TimeIndex", (o as TimeLine).TimeIndex);
                        cmd.Parameters.AddWithValue("@TimeFrom", (o as TimeLine).TimeFrom);
                        cmd.Parameters.AddWithValue("@TimeTo", (o as TimeLine).TimeTo);
                        (o as TimeLine).Id = (int)cmd.ExecuteScalar();
                        return o;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public bool InsertLine(string line)
        {
            try
            {
                int rsDay = 0, rsNight = 0;
                if (SelectIdLine(CaSX.DAY, line) == 0)
                {
                    using (SqlCommand cmd = new SqlCommand("insert into " + TIME + "(Line,Type) values(@Line,@Type);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                    {
                        cmd.Parameters.AddWithValue("@Line", line);
                        cmd.Parameters.AddWithValue("@Type", CaSX.DAY);
                        rsDay = (int)cmd.ExecuteScalar();
                    }
                }
                else rsDay = 1;
                if (SelectIdLine(CaSX.NIGHT, line) == 0)
                {
                    using (SqlCommand cmd = new SqlCommand("insert into " + TIME + "(Line,Type) values(@Line,@Type);SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                    {
                        cmd.Parameters.AddWithValue("@Line", line);
                        cmd.Parameters.AddWithValue("@Type", CaSX.NIGHT);
                        rsNight = (int)cmd.ExecuteScalar();
                    }
                }
                else rsNight = 1;
                return (rsDay > 0 && rsNight > 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return false;
            }
        }
        public int SelectIdLine(int caSX, string line)
        {
            try
            {
                string sql = "select * from " + TIME + " where Type = '" + caSX + "' and Line = '" + line + "'";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int IdLine = reader.GetInt32(reader.GetOrdinal(TimeLineString.Id));
                            return IdLine;
                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
            return 0;
        }
        public int IsExist(int IdTimeLine, int TimeIndex)
        {
            try
            {
                string sql = "select count(IdTimeLine) total from " + TIMELINE + " where IdTimeLine = " + IdTimeLine + " and TimeIndex = " + TimeIndex;
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int count = reader.GetInt32(reader.GetOrdinal(TimeLineString.total));
                            return count;
                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
            }
            return 0;
        }
        public List<string> SelectLine(int type)
        {
            var list = new List<string>();
            try
            {
                string sql = "select * from " + TIME + " where Type = '" + type + "'";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            string line = reader[reader.GetOrdinal(TimeLineString.Line)] as string;
                            list.Add(line);
                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
                return null;
            }
            return list;
        }
        public List<TimeLine> Select(string line, int type)
        {
            var list = new List<TimeLine>();
            try
            {
                string sql = "select * from " + TIMELINE + "  where IdTimeLine  in (select Id from " + TIME + " where Line = '" + line + "' and Type = " + type + ")";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            TimeLine timeLine = new TimeLine();
                            timeLine.Id = reader.GetInt32(reader.GetOrdinal(TimeLineString.Id));
                            timeLine.IdTimeLine = reader.GetInt32(reader.GetOrdinal(TimeLineString.IdTimeLine));
                            timeLine.TimeIndex = reader.GetInt32(reader.GetOrdinal(TimeLineString.TimeIndex));
                            timeLine.TimeFrom = reader[reader.GetOrdinal(TimeLineString.TimeFrom)] as string;
                            timeLine.TimeTo = reader[reader.GetOrdinal(TimeLineString.TimeTo)] as string;
                            list.Add(timeLine);
                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
                return null;
            }
            return list;
        }
    }
}
