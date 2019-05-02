#region UsingRegion

using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

#endregion

namespace Krouzky.ORM.Database.DAO {
    #region UsingRegion

    #endregion

    public abstract class Table<T> {
        protected readonly string _sqlDeleteId;
        protected readonly string _sqlInsert;
        protected readonly string _sqlSelect;
        protected readonly string _sqlSelectId;
        protected readonly string _sqlUpdate;
        protected readonly string _tableName;
        protected readonly string sqlSelectOneParamName_;

        protected Table(string tableName, string sqlSelect, string sqlSelectId, string sqlInsert, string sqlUpdate,
            string sqlDeleteId, string sqlSelectOneParamName) {
            this._tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            this._sqlSelect = sqlSelect ?? throw new ArgumentNullException(nameof(sqlSelect));
            this._sqlSelectId = sqlSelectId ?? throw new ArgumentNullException(nameof(sqlSelectId));
            this._sqlInsert = sqlInsert ?? throw new ArgumentNullException(nameof(sqlInsert));
            this._sqlUpdate = sqlUpdate ?? throw new ArgumentNullException(nameof(sqlUpdate));
            this._sqlDeleteId = sqlDeleteId ?? throw new ArgumentNullException(nameof(sqlDeleteId));
            this.sqlSelectOneParamName_ = sqlSelectOneParamName ?? throw new ArgumentNullException(nameof(sqlDeleteId));
        }

        public int Insert(T dbObject, Database pDb = null) {
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            }
            else {
                db = pDb;
            }
            db.BeginTransaction();
            SqlCommand command = db.CreateCommand(this._sqlInsert);
            if (command == null) return -1;

            this.PrepareCommand(command, dbObject);
            int ret = db.ExecuteNonQuery(command);

            //ret = db.ExecuteNonQuery(db.CreateCommand("SELECT SCOPE_IDENTITY()"));
            //ret = id;

            //SqlDataReader reader = db.CreateCommand("SELECT SCOPE_IDENTITY() as id").ExecuteReader();
            SqlDataReader reader = db.CreateCommand("select @@identity as id").ExecuteReader();

            while (reader.Read()) {
                    Console.WriteLine(reader.GetDecimal(0));
                    ret = (int) reader.GetDecimal(0);
            }

            reader.Close();
            db.EndTransaction();
            if (pDb == null) db.Close();


            return ret;
        }

        public int Update(T dbObject, Database pDb = null) {
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            }
            else {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(this._sqlUpdate);
            this.PrepareCommand(command, dbObject);
            int ret = db.ExecuteNonQuery(command);
            if (pDb == null) db.Close();

            return ret;
        }

        public Collection<T> Select(Database pDb = null) {
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            }
            else {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(this._sqlSelect);
            if (command == null) return new Collection<T>();

            //command.Parameters.AddWithValue(parameterName, idDbObject);
            SqlDataReader reader = db.Select(command);
            Collection<T> result = this.Read(reader);
            reader.Close();
            if (pDb == null) db.Close();

            return result;
        }

        public Collection<T> SelectWhere<TU>(TU idDbObject, string parameterName, Database pDb = null) {
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            }
            else {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(this._sqlSelectId);
            if (command == null) return new Collection<T>();

            command.Parameters.AddWithValue(parameterName, idDbObject);

            SqlDataReader reader = db.Select(command);
            Collection<T> result = this.Read(reader);
            reader.Close();
            if (pDb == null) db.Close();

            return result;
        }

        public bool SelectOne(int idDbObject, out T result, Database pDb = null) {
            return SelectOne(idDbObject, sqlSelectOneParamName_, out result, pDb);
        }

        public bool SelectOne(int idDbObject, string parameterName, out T result, Database pDb = null) {
            bool res = false;
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            }
            else {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(this._sqlSelectId);
            command.Parameters.AddWithValue(parameterName, idDbObject);
            SqlDataReader reader = db.Select(command);

            Collection<T> results = this.Read(reader);
            //T result;
            if (results.Count == 1) {
                result = results[0];
                res = true;
            }
            else {
                result = default(T); //(T) new object();
            }

            reader.Close();
            if (pDb == null) db.Close();

            //Hlavne pro debug
            if (!res) throw new Exception("Objekt v tabulce neexistuje");
            return res;
        }

        public int Delete(int idDbObject, string parameterName, Database pDb = null) {
            Database db;
            if (pDb == null) {
                db = new Database();
                db.Connect();
            }
            else {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(this._sqlDeleteId);
            if (command == null) return -1;

            command.Parameters.AddWithValue(parameterName, idDbObject);
            int ret = db.ExecuteNonQuery(command);
            if (pDb == null) db.Close();

            return ret;
        }

        protected abstract Collection<T> Read(SqlDataReader reader);
        protected abstract void PrepareCommand(SqlCommand command, T dbObject);
    }
}