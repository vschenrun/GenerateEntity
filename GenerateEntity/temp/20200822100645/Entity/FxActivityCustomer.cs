using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity
{
    ///<summary>
    ///活动客服信息
    ///</summary>
    [SugarTable("fx_activity_customer")]
    public partial class FxActivityCustomer
    {
           public FxActivityCustomer(){


           }
           /// <summary>
           /// Desc:活动客服ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="customer_id")]
           public string CustomerId {get;set;}

           /// <summary>
           /// Desc:活动ID
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="activity_id")]
           public string ActivityId {get;set;}

           /// <summary>
           /// Desc:客服名称
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_name")]
           public string CustomerName {get;set;}

           /// <summary>
           /// Desc:联系方式
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="activity_contact")]
           public string ActivityContact {get;set;}

           /// <summary>
           /// Desc:客服二维码链接
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="customer_url")]
           public string CustomerUrl {get;set;}

           /// <summary>
           /// Desc:操作员id
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="user_id")]
           public string UserId {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="create_time")]
           public DateTime? CreateTime {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="updater_id")]
           public string UpdaterId {get;set;}

           /// <summary>
           /// Desc:修改时间
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="update_time")]
           public DateTime? UpdateTime {get;set;}

           /// <summary>
           /// Desc:商家名称
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="merchant_name")]
           public string MerchantName {get;set;}

           /// <summary>
           /// Desc:品牌logo
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="merchant_logo")]
           public string MerchantLogo {get;set;}

           /// <summary>
           /// Desc:品牌简介
           /// Default:NULL
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="merchant_brand")]
           public string MerchantBrand {get;set;}

    }
}
