using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanningAPI.Model
{
    public static class ListExtensions
    {
        public static string ToValidationString(this IEnumerable<string> validationErrors)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var error in validationErrors)
            {
                stringBuilder.AppendLine(error);
            }

            return stringBuilder.ToString();
        }
    }
}
