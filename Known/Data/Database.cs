﻿using System;
using System.Collections.Generic;
using System.Data;
using Known.Mapping;

namespace Known.Data
{
    public class Database : IDisposable
    {
        private IDbProvider provider;

        public Database() : this("Default") { }

        public Database(string name)
        {
            Name = name;
            provider = new DbProvider(name);
        }

        public string Name { get; }
        public string ConnectionString
        {
            get { return provider.ConnectionString; }
        }

        public string UserName { get; internal set; }

        public void BeginTrans()
        {
            provider.BeginTrans();
        }

        public void Commit()
        {
            provider.Commit();
        }

        public void Rollback()
        {
            provider.Rollback();
        }

        public Result Transaction(Action<Database> action)
        {
            var db = new Database(Name);

            try
            {
                db.BeginTrans();
                action(db);
                db.Commit();
                return Result.Success("");
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw ex;
            }
            finally
            {
                db.Dispose();
            }
        }

        public void Execute(string sql, object param = null)
        {
            var command = CommandHelper.GetCommand(sql, param);
            provider.Execute(command);
        }

        public T Scalar<T>(string sql, object param = null)
        {
            var command = CommandHelper.GetCommand(sql, param);
            return (T)provider.Scalar(command);
        }

        public T QueryById<T>(string id) where T : BaseEntity
        {
            var sql = CommandHelper.GetQueryByIdSql<T>();
            return Query<T>(sql, new { id });
        }

        public T Query<T>(string sql, object param = null) where T : BaseEntity
        {
            var row = QueryRow(sql, param);
            if (row == null)
                return default(T);

            return AutoMapper.GetBaseEntity<T>(row);
        }

        public List<T> QueryList<T>() where T : BaseEntity
        {
            var sql = CommandHelper.GetQueryListSql<T>();
            var data = QueryTable(sql);
            return AutoMapper.GetBaseEntities<T>(data);
        }

        public List<T> QueryList<T>(string sql, object param = null) where T : BaseEntity
        {
            var data = QueryTable(sql, param);
            return AutoMapper.GetBaseEntities<T>(data);
        }

        public List<T> QueryListById<T>(string[] ids) where T : BaseEntity
        {
            var sql = CommandHelper.GetQueryListByIdSql<T>(ids);
            var data = QueryTable(sql);
            return AutoMapper.GetBaseEntities<T>(data);
        }

        public PagingResult<T> QueryPage<T>(string sql, PagingCriteria criteria)
        {
            var result = QueryPageTable(sql, criteria);
            if (result == null)
                return null;

            var pageData = AutoMapper.GetEntities<T>(result.PageData);
            return new PagingResult<T>(result.TotalCount, pageData);
        }

        public void Save<T>(T entity) where T : BaseEntity
        {
            if (entity.IsNew)
            {
                entity.CreateBy = UserName;
                entity.CreateTime = DateTime.Now;
            }
            else
            {
                entity.ModifyBy = UserName;
                entity.ModifyTime = DateTime.Now;
            }

            var command = CommandHelper.GetSaveCommand(entity);
            provider.Execute(command);
        }

        public void Save<T>(List<T> entities) where T : BaseEntity
        {
            foreach (var entity in entities)
            {
                Save(entity);
            }
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            var command = CommandHelper.GetDeleteCommand(entity);
            provider.Execute(command);
        }

        public void Delete<T>(List<T> entities) where T : BaseEntity
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public void WriteTable(DataTable table)
        {
            provider.WriteTable(table);
        }

        public DataTable QueryTable(string sql, object param = null)
        {
            var command = CommandHelper.GetCommand(sql, param);
            return provider.Query(command);
        }

        public PagingResult QueryPageTable(string sql, PagingCriteria criteria)
        {
            var cmd = CommandHelper.GetQueryCommand(sql, criteria.Parameter);
            if (cmd == null)
                return null;

            var sqlCount = CommandHelper.GetCountSql(cmd.Text);
            var cmdCount = new Command(sqlCount, cmd.Parameters);
            var totalCount = (int)provider.Scalar(cmdCount);

            var sqlPage = CommandHelper.GetPagingSql(cmd.Text, criteria);
            var cmdData = new Command(sqlPage, cmd.Parameters);
            var pageData = provider.Query(cmdData);
            return new PagingResult(totalCount, pageData);
        }

        public DataRow QueryRow(string sql, object param = null)
        {
            var command = CommandHelper.GetCommand(sql, param);
            var data = provider.Query(command);
            if (data == null || data.Rows.Count == 0)
                return null;

            return data.Rows[0];
        }

        public void Dispose()
        {
            if (provider != null)
            {
                provider.Dispose();
                provider = null;
            }
        }
    }
}
