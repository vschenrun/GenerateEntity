using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Entity
{
    ///<summary>
    ///群id变更
    ///</summary>
    [SugarTable("tk_change_group")]
    public partial class TkChangeGroup
    {
           public TkChangeGroup(){


           }
           /// <summary>
           /// Desc:新群id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="new_group_id")]
           public string newGroupId {get;set;}

           /// <summary>
           /// Desc:状态 (0-待修改,1-修改成功)
           /// Default:0
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="change_status")]
           public int? changeStatus {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="create_time")]
           public DateTime? createTime {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="update_time")]
           public DateTime? updateTime {get;set;}

           /// <summary>
           /// Desc:主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,ColumnName="change_group_id")]
           public string changeGroupId {get;set;}

           /// <summary>
           /// Desc:旧群id
           /// Default:
           /// Nullable:True
           /// </summary>           
           [SugarColumn(ColumnName="old_group_id")]
           public string oldGroupId {get;set;}

    }
}
