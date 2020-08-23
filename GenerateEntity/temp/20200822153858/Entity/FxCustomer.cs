using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity
{
    ///<summary>
    ///客户表
    ///</summary>
    [SugarTable("fx_customer")]
    public partial class FxCustomer
    {
           public FxCustomer(){


           }
           /// <summary>
           /// Desc:客户id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="customer_id")]
           public string customerId {get;set;}

           /// <summary>
           /// Desc:用户序列号MD5值
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_no")]
           public string customerNo {get;set;}

           /// <summary>
           /// Desc:客户openId
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="open_id")]
           public string openId {get;set;}

           /// <summary>
           /// Desc:公众号appId
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="app_id")]
           public string appId {get;set;}

           /// <summary>
           /// Desc:客户unId
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="union_id")]
           public string unionId {get;set;}

           /// <summary>
           /// Desc:支付openId
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="pay_open_id")]
           public string payOpenId {get;set;}

           /// <summary>
           /// Desc:支付appId
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="pay_app_id")]
           public string payAppId {get;set;}

           /// <summary>
           /// Desc:客户昵称
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_name")]
           public string customerName {get;set;}

           /// <summary>
           /// Desc:客户头像地址
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="head_url")]
           public string headUrl {get;set;}

           /// <summary>
           /// Desc:国家
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_country")]
           public string customerCountry {get;set;}

           /// <summary>
           /// Desc:省
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_province")]
           public string customerProvince {get;set;}

           /// <summary>
           /// Desc:城市
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_city")]
           public string customerCity {get;set;}

           /// <summary>
           /// Desc:客户性别
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(ColumnName="customer_sex")]
           public int customerSex {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:current_timestamp()
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="create_time")]
           public DateTime? createTime {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:current_timestamp()
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="update_time")]
           public DateTime? updateTime {get;set;}

    }
}
