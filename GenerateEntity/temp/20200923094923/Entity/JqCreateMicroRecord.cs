using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity
{
    ///<summary>
    ///建群记录表
    ///</summary>
    [SugarTable("jq_create_micro_record")]
    public partial class JqCreateMicroRecord
    {
           public JqCreateMicroRecord(){


           }
           /// <summary>
           /// Desc:业务单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="business_no")]
           public string businessNo {get;set;}

           /// <summary>
           /// Desc:记录建群单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="create_micro_no")]
           public string createMicroNo {get;set;}

           /// <summary>
           /// Desc:建群备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="create_micro_note")]
           public string createMicroNote {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="create_time")]
           public DateTime? createTime {get;set;}

           /// <summary>
           /// Desc:操作编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="operation_no")]
           public string operationNo {get;set;}

           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="record_id")]
           public string recordId {get;set;}

           /// <summary>
           /// Desc:记录类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="record_type")]
           public int? recordType {get;set;}

    }
}
