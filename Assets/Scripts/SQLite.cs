using UnityEngine;
using System.IO;

public class SQLite : MonoBehaviour
{
    public string m_DatabaseFileName = "SQLiteTestDB.db";
    public string m_TableName = "test";
    private DatabaseAccess m_DatabaseAccess;

    void Start()
    {
        /*string filePath = Path.Combine(Application.streamingAssetsPath, m_DatabaseFileName);
        Debug.Log(filePath);
        m_DatabaseAccess = new DatabaseAccess("data source = " + filePath);
        *//*
                m_DatabaseAccess.CreateTable("TestTable1",
                    new string[] { "name", "age" },
                    new string[] { "text", "int" });

                m_DatabaseAccess.InsertInto("TestTable1", new string[] { "'Coderzedro'", "'47'" });
                m_DatabaseAccess.InsertInto("TestTable1", new string[] { "'JD'", "'17'" });
                m_DatabaseAccess.InsertInto("TestTable1", new string[] { "'Tiger'", "'47'" });
        *//*
        
        var res = m_DatabaseAccess.ExecuteQuery("SELECT * FROM test;");
        while (res.Read())
        {
            //print(res["ID"] + " / " + res["value"]);
        }
        
        m_DatabaseAccess.CloseSqlConnection();*/
    }
}