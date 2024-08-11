using Roc.Models;
using SqlSugar;
using System;

namespace Single.Entities.Entity;
/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/6/11 14:40:47
/// 版本：1.6
/// </summary>
[SugarTable("sys_role")]
public class SysRole : RocSupplyEntity
{

	///<summary>
	///roleid
	///</summary>
	public string? RoleId  { set; get;} 
	///<summary>
	///rolename
	///</summary>
	public string? RoleName  { set; get;} 
	///<summary>
	///menubuttonid
	///</summary>
	public string? MenubuttonId  { set; get;} 
}

