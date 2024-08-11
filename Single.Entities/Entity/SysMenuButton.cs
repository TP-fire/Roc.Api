using Roc.Models;
using SqlSugar;
using System;

namespace Single.Entities.Entity;
/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/6/6 10:37:11
/// 版本：1.5
/// </summary>
[SugarTable("sys_menu_button")]
public class SysMenuButton : RocSupplyEntity
{

	///<summary>
	///parentid
	///</summary>
	public string? ParentId  { set; get;} 
	///<summary>
	///category
	///</summary>
	public int? Category  { set; get;} 
	///<summary>
	///name
	///</summary>
	public string? Name  { set; get;} 
	///<summary>
	///title
	///</summary>
	public string? Title  { set; get;} 
	///<summary>
	///path
	///</summary>
	public string? Path  { set; get;} 
	///<summary>
	///component
	///</summary>
	public string? Component  { set; get;} 
	///<summary>
	///icon
	///</summary>
	public string? Icon  { set; get;} 
	///<summary>
	///sortcode
	///</summary>
	public int? SortCode  { set; get;} 
}

