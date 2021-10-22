using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ApiIntegrasiSIAP.Libs
{
    public class DbUtils
    {
        private DbConnection _db;

        public DbUtils (DbContext db)
        {
            _db = db.Database.GetDbConnection();
        }

        public DataSet GetDataSet(String sql, Boolean isStoredProcedure=false, Dictionary<String, Object> parameters=null)
        {
            DbCommand cmd = _db.CreateCommand();
            cmd.CommandText = sql;

            if(isStoredProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            if(parameters != null && parameters.Count > 0)
            {
                foreach(KeyValuePair<String, Object> item in parameters)
                {
                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = item.Key;
                    param.Value = item.Value;
                    cmd.Parameters.Add(param);
                }
            }

            return GetDataSet(cmd);
        }

        public DataSet GetDataSet(DbCommand cmd)
        {
            DataSet ds = new DataSet();

            _db.Open();

            DbDataAdapter da = DbProviderFactories.GetFactory(_db).CreateDataAdapter();
            da.SelectCommand = cmd;
            da.Fill(ds);

            _db.Close();

            return ds;
        }

        public DataTable GetDataTable(String sql, Boolean isStoredProcedure=false, Dictionary<String, Object> parameters = null)
        {
            DbCommand cmd = _db.CreateCommand();
            cmd.CommandText = sql;

            if (isStoredProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<String, Object> item in parameters)
                {
                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = item.Key;
                    param.Value = item.Value;
                    cmd.Parameters.Add(param);
                }
            }

            return GetDataTable(cmd);
        }

        public DataTable GetDataTable(DbCommand cmd)
        {
            DataTable dt = new DataTable();

            _db.Open();

            //cmd.Prepare();
            DbDataReader sda = cmd.ExecuteReader();
            dt.Load(sda);

            _db.Close();

            return dt;
        }

        public bool ExecuteNonQuery(String sql, Boolean isStoredProcedure = false, Dictionary<String, Object> parameters = null)
        {
            DbCommand cmd = _db.CreateCommand();
            cmd.CommandText = sql;

            if (isStoredProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<String, Object> item in parameters)
                {
                    DbParameter param = cmd.CreateParameter();
                    param.ParameterName = item.Key;
                    param.Value = item.Value;
                    cmd.Parameters.Add(param);
                }
            }

            return ExecuteNonQuery(cmd);
        }

        public bool ExecuteNonQuery(DbCommand cmd)
        {
            _db.Open();

            //cmd.Prepare();
            int result = cmd.ExecuteNonQuery();

            _db.Close();

            return result > 0;
        }
    }
}
