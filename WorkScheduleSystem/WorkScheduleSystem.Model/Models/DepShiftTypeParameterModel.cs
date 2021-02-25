using System;
using System.Collections.Generic;
using System.Text;
using WorkScheduleSystem.BaseModels.Models;
using WorkScheduleSystem.Common.AttributeExtend;

namespace WorkScheduleSystem.Model.Models
{
    [Mapping("DepShiftTypeParameter")]
    public class DepShiftTypeParameterModel : BaseModel
    {     
      public string depID { get; set; }
      public string shiftTypeCategory { get; set; }
      public string startTime { get; set; }
      public string endTime { get; set; }
      public double workhour { get; set; }
      public double resthour { get; set; }
      public bool status { get; set; }
      public DateTime createDatetime   {get;set;}
      public DateTime updateDatetime   {get;set;}
                 
    }
}
