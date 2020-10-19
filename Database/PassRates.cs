using Line_Production.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production.Database
{
    public class PassRates
    {
        public PassRate Update(PassRate o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("update PassRate set ProductionID = @ProductionID, ProductPlan = @ProductPlan,Actual = @Actual,Time = @Time,Line = @Line,IDCount = @IDCount, IDCountBox = @IDCountBox, BoxCurrent = @BoxCurrent,TimeCycleActual = @TimeCycleActual, TimeValue = @TimeValue where Id = '" + o.Id + "'", DataProvider.Instance.DB))
                {

                    cmd.Parameters.Add("@Id", SqlDbType.Int);
                    cmd.Parameters["@Id"].Value = o.Id;
                    cmd.Parameters.Add("@ProductionID", SqlDbType.NVarChar);
                    cmd.Parameters["@ProductionID"].Value = o.ProductionID;
                    cmd.Parameters.Add("@ProductPlan", SqlDbType.Int);
                    cmd.Parameters["@ProductPlan"].Value = o.ProductPlan;
                    cmd.Parameters.Add("@Actual", SqlDbType.Int);
                    cmd.Parameters["@Actual"].Value = o.Actual;
                    cmd.Parameters.Add("@Time", SqlDbType.NVarChar);
                    cmd.Parameters["@Time"].Value = o.Time;
                    cmd.Parameters.Add("@Line", SqlDbType.NVarChar);
                    cmd.Parameters["@Line"].Value = o.Line;
                    cmd.Parameters.Add("@IDCount", SqlDbType.Int);
                    cmd.Parameters["@IDCount"].Value = o.IDCount;
                    cmd.Parameters.Add("@IDCountBox", SqlDbType.Int);
                    cmd.Parameters["@IDCountBox"].Value = o.IDCountBox;
                    cmd.Parameters.Add("@BoxCurrent", SqlDbType.NVarChar);
                    cmd.Parameters["@BoxCurrent"].Value = o.BoxCurrent;

                    cmd.Parameters.Add("@TimeCycleActual", SqlDbType.Float);
                    cmd.Parameters["@TimeCycleActual"].Value = o.TimeCycleActual;
                    StringBuilder timeValue = new StringBuilder();
                    for (int i = 0; i < 10; i++)
                    {
                        timeValue.Append(o.TimeValue[i]);
                        timeValue.Append(",");
                    }
                    cmd.Parameters.Add("@TimeValue", SqlDbType.NVarChar);
                    cmd.Parameters["@TimeValue"].Value = timeValue.ToString();
                    cmd.ExecuteNonQuery();
                    return o;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public PassRate Insert(PassRate o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("insert into PassRate(ProductionID,ProductPlan,Actual,Time,Line,IDCount,IDCountBox,BoxCurrent,TimeCycleActual,TimeValue) values(@ProductionID,@ProductPlan,@Actual,@Time,@Line,@IDCount,@IDCountBox,@BoxCurrent,@TimeCycleActual,@TimeValue); SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@ProductionID", o.ProductionID);
                    cmd.Parameters.AddWithValue("@ProductPlan", o.ProductPlan);
                    cmd.Parameters.AddWithValue("@Actual", o.Actual);
                    cmd.Parameters.AddWithValue("@Time", o.Time);
                    cmd.Parameters.AddWithValue("@Line", o.Line);
                    cmd.Parameters.AddWithValue("@IDCount", o.IDCount);
                    cmd.Parameters.AddWithValue("@IDCountBox", o.IDCountBox);
                    cmd.Parameters.AddWithValue("@BoxCurrent", o.BoxCurrent);
                    cmd.Parameters.AddWithValue("@TimeCycleActual", o.TimeCycleActual);
                    StringBuilder timeValue = new StringBuilder();
                    for (int i = 0; i < 10; i++)
                    {
                        timeValue.Append(o.TimeValue[i]);
                        timeValue.Append(",");
                    }
                    cmd.Parameters.AddWithValue("@TimeValue", timeValue.ToString());
                    o.Id = (int)cmd.ExecuteScalar();
                    return o;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }

        public PassRate GetPassRate(string line, string modelId, string time)
        {
            try
            {
                string sql = "select * from PassRate where ProductionID = '" + modelId + "' and Line = '" + line + "' and Time = '" + time + "'";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            PassRate passRate = new PassRate();
                            passRate.Id = reader.GetInt32(reader.GetOrdinal(PassRateString.Id));
                            passRate.ProductionID = reader[reader.GetOrdinal(PassRateString.ProductionID)] as string;
                            passRate.Line = reader[reader.GetOrdinal(PassRateString.Line)] as string;
                            passRate.Time = reader[reader.GetOrdinal(PassRateString.Time)] as string;
                            passRate.ProductPlan = reader.GetInt32(reader.GetOrdinal(PassRateString.ProductPlan));
                            passRate.Actual = reader.GetInt32(reader.GetOrdinal(PassRateString.Actual));
                            passRate.IDCount = reader.GetInt32(reader.GetOrdinal(PassRateString.IDCount));
                            passRate.IDCountBox = reader.GetInt32(reader.GetOrdinal(PassRateString.IDCountBox));
                            passRate.BoxCurrent = reader[reader.GetOrdinal(PassRateString.BoxCurrent)] as string;
                            passRate.TimeCycleActual = reader.GetDouble(reader.GetOrdinal(PassRateString.TimeCycleActual));
                            string timeValue = reader[reader.GetOrdinal(PassRateString.TimeValue)] as string;
                            passRate.TimeValue = timeValue.Split(',');
                            return passRate;
                        }

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }


}
