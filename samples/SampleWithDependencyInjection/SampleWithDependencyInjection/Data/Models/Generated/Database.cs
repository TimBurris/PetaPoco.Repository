﻿
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `SampleDBConnection`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Code\PetaPoco.Repository\samples\SampleWithDependencyInjection\SampleWithDependencyInjection\Data\localdb\SampleDB.mdf;Integrated Security=True;`
//     Schema:                 ``
//     Include Views:          `False`

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace SampleWithDependencyInjection.Data.Models
{
	

    
	[TableName("dbo.SalesOrder")]
	[PrimaryKey("SalesOrderId")]
	[ExplicitColumns]
    public partial class SalesOrder  
    {
		[Column] public int SalesOrderId { get; set; }
		[Column] public decimal Amount { get; set; }
		[Column] public string SoldBy { get; set; }
	}
    
	[TableName("dbo.SalesOrderDetailId")]
	[PrimaryKey("SalesOrderDetailId")]
	[ExplicitColumns]
    public partial class SalesOrderDetailId  
    {
		[Column("SalesOrderDetailId")] public int _SalesOrderDetailId { get; set; }
		[Column] public int SalesOrderId { get; set; }
		[Column] public string PartNumber { get; set; }
		[Column] public int Quantity { get; set; }
	}
}
