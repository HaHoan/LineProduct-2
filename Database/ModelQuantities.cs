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
    public class ModelQuantities
    {
        public object Insert(object o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("insert into ModelQuantity(Model,PersonPerLine,CycleTime,WarnQuantity,MinQuantity,CharModel,UseBarcode) values(@Model, @PersonPerLine, @CycleTime, @WarnQuantity, @MinQuantity, @CharModel,@UseBarcode); SELECT CAST(scope_identity() AS int)", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@Model", (o as Model).ModelID);
                    cmd.Parameters.AddWithValue("@PersonPerLine", (o as Model).PersonInLine);
                    cmd.Parameters.AddWithValue("@CycleTime", (o as Model).Cycle);
                    cmd.Parameters.AddWithValue("@WarnQuantity", (o as Model).WarnQuantity);
                    cmd.Parameters.AddWithValue("@MinQuantity", (o as Model).MinQuantity);
                    cmd.Parameters.AddWithValue("@CharModel", (o as Model).CharModel);
                    cmd.Parameters.AddWithValue("@UseBarcode", (o as Model).UseBarcode);
                    
                    (o as Model).Id = (int)cmd.ExecuteScalar();
                    return o;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public object Update(Model o)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("update ModelQuantity set Model = @Model, PersonPerLine = @PersonPerLine, CycleTime = @CycleTime, WarnQuantity = @WarnQuantity, MinQuantity = @MinQuantity, CharModel = @CharModel,UseBarcode = @UseBarcode where Id = '"+o.Id+"'", DataProvider.Instance.DB))
                {
                    cmd.Parameters.AddWithValue("@Model", (o as Model).ModelID);
                    cmd.Parameters.AddWithValue("@PersonPerLine", (o as Model).PersonInLine);
                    cmd.Parameters.AddWithValue("@CycleTime", (o as Model).Cycle);
                    cmd.Parameters.AddWithValue("@WarnQuantity", (o as Model).WarnQuantity);
                    cmd.Parameters.AddWithValue("@MinQuantity", (o as Model).MinQuantity);
                    cmd.Parameters.AddWithValue("@CharModel", (o as Model).CharModel);
                    cmd.Parameters.AddWithValue("@UseBarcode", (o as Model).UseBarcode);

                    (o as Model).Id = (int)cmd.ExecuteScalar();
                    return o;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return null;
            }
        }
        public List<Model> Select()
        {
            var list = new List<Model>();
            try
            {
                string sql = "select * from ModelQuantity";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Model model = new Model();
                            model.Id = reader.GetInt32(reader.GetOrdinal(ModelString.Id));
                            model.ModelID = reader[reader.GetOrdinal(ModelString.ModelID)] as string;
                            model.Cycle = reader.GetDouble(reader.GetOrdinal(ModelString.CycleTime));
                            model.UseBarcode = reader.GetInt32(reader.GetOrdinal(ModelString.UseBarcode)) == 1 ? true : false;
                            model.WarnQuantity = reader.GetDouble(reader.GetOrdinal(ModelString.WarnQuantity));
                            model.MinQuantity = reader.GetDouble(reader.GetOrdinal(ModelString.MinQuantity));
                            model.CharModel = reader[reader.GetOrdinal(ModelString.CharModel)] as string;
                            model.PersonInLine = reader.GetInt32(reader.GetOrdinal(ModelString.PersonPerLine));
                            list.Add(model);
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
        public Model Select(string ModelID)
        {
            try
            {
                string sql = "select * from ModelQuantity where Model = '" + ModelID + "'";
                SqlCommand command = new SqlCommand(sql, DataProvider.Instance.DB);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Model model = new Model();
                            model.Id = reader.GetInt32(reader.GetOrdinal(ModelString.Id));
                            model.ModelID = reader[reader.GetOrdinal(ModelString.ModelID)] as string;
                            model.Cycle = reader.GetDouble(reader.GetOrdinal(ModelString.CycleTime));
                            model.UseBarcode = reader.GetInt32(reader.GetOrdinal(ModelString.UseBarcode)) == 1 ? true : false;
                            model.WarnQuantity = reader.GetDouble(reader.GetOrdinal(ModelString.WarnQuantity));
                            model.MinQuantity = reader.GetDouble(reader.GetOrdinal(ModelString.MinQuantity));
                            model.CharModel = reader[reader.GetOrdinal(ModelString.CharModel)] as string;
                            model.PersonInLine = reader.GetInt32(reader.GetOrdinal(ModelString.PersonPerLine));
                            return model;
                        }

                    }

                }
                return null;
            }
            catch (Exception e)
            {
                Console.Write(e.Message.ToString());
                return null;
            }
        }

        public int Delete(string Model)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("delete from ModelQuantity where Model = @Model", DataProvider.Instance.DB))
                {
                    cmd.Parameters.Add("@Model", SqlDbType.NVarChar);
                    cmd.Parameters["@Model"].Value = Model;

                    int rowCount = cmd.ExecuteNonQuery();
                    return rowCount;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return 0;
            }
        }
    }
}
