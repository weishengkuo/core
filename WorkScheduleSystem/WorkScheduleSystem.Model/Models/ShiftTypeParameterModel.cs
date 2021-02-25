using System;
using System.Collections.Generic;
using System.Text;
using WorkScheduleSystem.BaseModels.Models;
using WorkScheduleSystem.Common.AttributeExtend;

namespace WorkScheduleSystem.Model.Models
{
    [Mapping("ShiftTypeParameter")]
    public class ShiftTypeParameterModel : BaseModel
    {
      public  string name             {get;set;}
      public string shiftTypeCategory { get; set; }
      public DateTime? createDatetime   {get;set;}
      public DateTime? updateDatetime   {get;set;}
            
    }
}
