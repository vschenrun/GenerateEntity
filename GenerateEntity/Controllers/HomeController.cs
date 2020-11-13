using Sealee.Util;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace GenerateEntity.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string dbLink = string.Empty;
            int dbType = 0;
            if (Request.Cookies["MyData"] != null)
            {
                dbLink = Base64Helper.Decode(Request.Cookies["MyData"]["dbLink"]);
                if (Request.Cookies["MyData"]["dbType"] == "Mysql")
                    dbType = 0;
                if (Request.Cookies["MyData"]["dbType"] == "SqlServer")
                    dbType = 1;
            }
            ViewBag.dbLink = dbLink;
            ViewBag.dbType = dbType;
            return View();
        }

        /// <summary>
        /// 链接数据库并查询
        /// </summary>
        /// <param name="dbLink"></param>
        /// <param name="dbType"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public JsonResult LinkServer(string dbLink, DbType dbType, string tableName)
        {
            ResultInfo result = new ResultInfo();
            SqlSugarClient db = GetSugarClient(dbLink, dbType);
            try
            {             
                List<DbTableInfo> list = db.DbMaintenance.GetTableInfoList();
                if (!string.IsNullOrEmpty(tableName))  //模糊查询
                {
                    List<DbTableInfo> tableList = list.Where<DbTableInfo>(t => t.Name.Contains(tableName) || t.Description.Contains(tableName)).ToList();
                    result.info = tableList;
                }
                else
                {
                    result.info = list;
                }

                //获取客户端的Cookie对象
                HttpCookie cok = Request.Cookies["MyData"];

                if (cok != null)
                {
                    //修改Cookie的两种方法
                    cok.Values["dbLink"] = Base64Helper.Encode(dbLink);
                    cok.Values["dbType"] = dbType.ToString();
                    Response.AppendCookie(cok);
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("MyData");//初使化并设置Cookie的名称
                    DateTime dt = DateTime.Now;
                    TimeSpan ts = new TimeSpan(2, 0, 0, 0, 0);//过期时间为1分钟
                    cookie.Expires = dt.Add(ts);//设置过期时间
                    cookie.Values.Add("dbLink", Base64Helper.Encode(dbLink));
                    cookie.Values.Add("dbType", dbType.ToString());
                    Response.AppendCookie(cookie);
                }
                db.Dispose();
                db.Close();
                result.res = true;
                result.msg = "链接成功！";
            }
            catch (Exception ex)
            {
                db.Dispose();
                db.Close();
                result.msg = ex.Message;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 初始化 SqlSugarClient
        /// </summary>
        /// <param name="dbLink"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public SqlSugarClient GetSugarClient(string dbLink, DbType dbType)
        {
            //配置数据库连接
            using (SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = dbLink, //设置数据库类型
                DbType = dbType,
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            }))
                return db;
        }

        //生成实体
        public FilePathResult GenerateEntity(string dbLink, DbType dbType, List<DbTableInfo> TableName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/temp/") + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
            string filename = string.Format("Code{0}.zip", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string path1 = System.Web.HttpContext.Current.Server.MapPath("~/temp/") + filename;
            if (false == System.IO.Directory.Exists(path + "IRepository\\"))
            {
                //创建IRepository文件夹
                System.IO.Directory.CreateDirectory(path + "IRepository\\");
            }
            if (false == System.IO.Directory.Exists(path + "Repository\\"))
            {
                //创建Repository文件夹
                System.IO.Directory.CreateDirectory(path + "Repository\\");
            }
            ResultInfo result = new ResultInfo();
            try
            {
                //配置数据库连接
                SqlSugarClient db = GetSugarClient(dbLink, dbType);
                //表
                foreach (DbTableInfo table in TableName)
                {
                    string table_name = table.Name.ToCase();
                    db.MappingTables.Add(table_name, table.Name);
                    List<DbColumnInfo> dd = db.DbMaintenance.GetColumnInfosByTableName(table.Name);
                    foreach (DbColumnInfo item in dd)
                    {
                        db.MappingColumns.Add(item.DbColumnName.ToInitialCase(), item.DbColumnName, table_name);
                    }
                    db.DbFirst.IsCreateAttribute().Where(table.Name).CreateClassFile(path + "Entity\\", "Entity");

                    GenerateRepository(path + "Repository\\", table);
                    GenerateIRepository(path + "IRepository\\", table);
                }

                ZipHelper.CreateZip(path, path1);

                result.res = true;
                result.msg = "生成成功！";
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
            }
            return File(path1, "application/zip", filename);
        }

        /// <summary>
        /// 生成仓储接口
        /// </summary>
        /// <param name="path"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GenerateIRepository(string path, DbTableInfo param)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($@"
//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 {DateTime.Now.ToString("yyyy/M/d HH:mm:ss")}
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//--------------------------------------------------------------------

using System;
using Entity;
namespace IRepository
{{
	/// <summary>
	/// {param.Description}({param.Name})
	/// </summary>
	public interface I{param.Name.ToCase()}Repository : IBaseRepository<{param.Name.ToCase()}>
	{{
	}}
}}");
            string str = path + $"I{param.Name.ToCase()}Repository.cs";
            using (FileStream fs = new FileStream(str, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.Write(strBuilder);
                    writer.Flush();
                    writer.Close();
                }
                fs.Close();
            }
            return 0;
        }

        /// <summary>
        /// 生成仓储
        /// </summary>
        /// <param name="path"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GenerateRepository(string path, DbTableInfo param)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($@"
//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 {DateTime.Now.ToString("yyyy/M/d HH:mm:ss")}
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//--------------------------------------------------------------------

using System;
using IRepository;
using Entity;
namespace Repository
{{
	/// <summary>
	/// {param.Description}({param.Name})
	/// </summary>
	public class {param.Name.ToCase()}Repository : BaseRepository<{param.Name.ToCase()}>,I{param.Name.ToCase()}Repository
	{{
	}}
}}");
            string str = path + $"{param.Name.ToCase()}Repository.cs";

            using (FileStream fs = new FileStream(str, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    writer.Write(strBuilder);
                    writer.Flush();
                    writer.Close();
                }
                fs.Close();
            }

            return 0;
        }

        //封装返回信息数据
        public class ResultInfo
        {
            public ResultInfo()
            {
                res = false;
                startcode = 449;
                info = "";
            }

            public bool res { get; set; }  //返回状态（true or false）
            public string msg { get; set; }  //返回信息
            public int startcode { get; set; }  //返回http的状态码
            public Object info { get; set; }  //返回的结果（res为true时返回结果集，res为false时返回错误提示）
        }
    }
}