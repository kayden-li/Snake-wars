using UnityEngine;
using System;
using System.Collections;
using Mono.Data.Sqlite;

public class DbAccess
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader reader;

    public DbAccess(string connectionString)
    {
        OpenDB(connectionString);
    }
    public DbAccess() { }
    /// <summary>
    /// 打开数据库
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    public void OpenDB(string connectionString)
    {
        try
        {
            dbConnection = new SqliteConnection(connectionString);

            dbConnection.Open();

            Debug.Log("Connected to db");
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseSqlConnection()
    {
        if(dbCommand != null)
        {
            dbCommand.Dispose();
        }

        dbCommand = null;

        if(reader != null)
        {
            reader.Dispose();
        }

        reader = null;

        if(dbConnection != null)
        {
            dbConnection.Clone();
        }

        dbConnection = null;


        Debug.Log("Disconnected from db");
    }
    /// <summary>
    /// 执行sql语句
    /// </summary>
    /// <param name="sqlQuery">sql语句</param>
    /// <returns></returns>
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();

        dbCommand.CommandText = sqlQuery;

        reader = dbCommand.ExecuteReader();

        return reader;
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader ReaderFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="values">待插入的值</param>
    /// <returns></returns>
    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES( " + values[0];
        for(int i = 1; i < values.Length; ++i)
        {
            query += "," + values[i];
        }

        query += ")";

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 更新数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">列名集合</param>
    /// <param name="colsvalues">待更新的值</param>
    /// <param name="selectkey">选择的列名</param>
    /// <param name="selectvalue">选择的列值</param>
    /// <returns></returns>
    public SqliteDataReader UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "update " + tableName + " set " + cols[0] + " = " + colsvalues[0];

        for(int i = 1; i <colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " = " + selectvalue[i];
        }

        query += " where " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">待删除列名</param>
    /// <param name="colsvalues">待删除列值</param>
    /// <returns></returns>
    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];

        }

        Debug.Log(query);

        return ExecuteQuery(query);
    }
    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="cols">待添加的列名</param>
    /// <param name="values">待添加的列值</param>
    /// <returns></returns>
    public SqliteDataReader InsertIntoSpecific(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }
        
        string query = "INSERT INTO " + tableName + "(" + cols[0];
        
        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }
        
        query += ") VALUES (" + values[0];
        
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }

        query += ")";
        
        return ExecuteQuery(query);
    }
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public SqliteDataReader DeleteContents(string tableName)
    {
        string query = "DELETE FROM " + tableName;
        
        return ExecuteQuery(query);
    }
    /// <summary>
    /// 创建表
    /// </summary>
    /// <param name="name">表名</param>
    /// <param name="col">列名</param>
    /// <param name="colType">列类型</param>
    /// <returns></returns>
    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }
        
        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
        
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }

        query += ")";
        
        return ExecuteQuery(query);
    }
    /// <summary>
    /// 按条件查找
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="items">返回结果的列</param>
    /// <param name="col">被查找的列名</param>
    /// <param name="operation">查找的符号</param>
    /// <param name="values">被查找的值</param>
    /// <returns></returns>
    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }
        
        string query = "SELECT " + items[0];
        
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }
        
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";

        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col[i] + operation[i] + "'" + values[0] + "' ";
        }
        
        return ExecuteQuery(query);
    }
}