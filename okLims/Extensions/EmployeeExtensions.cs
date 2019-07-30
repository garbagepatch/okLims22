using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkflowEngineTest.Model;

namespace okLims.Extensions
{
    public static class EmployeeExtensions
    {
        public static string GetListRoles(this Employee item)
        {
            return string.Join(",", item.EmployeeRoles.Select(c => c.Role.Name).ToArray());
        }
    }
}