using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkScheduleSystem.BaseModels.Models;
using WorkScheduleSystem.Common;
using WorkScheduleSystem.Model.Models;
using WorkScheduleSystem.Models;

namespace WorkScheduleSystem.Controllers
{
    public class ShiftMaintainService
    {
        List<DepartmentModel> department = SimpleFactory.CreateInstance().FindAll<DepartmentModel>().AsQueryable().ToList();

        List<DepShiftTypeParameterModel> depshiftTypeParameter = SimpleFactory.CreateInstance().FindAll<DepShiftTypeParameterModel>().AsQueryable().ToList();

        List<ShiftTypeParameterModel> shiftTypeParameter = SimpleFactory.CreateInstance().FindAll<ShiftTypeParameterModel>().AsQueryable().ToList();

        DateTime date = DateTime.Now;

        public bool ValidatedToDepShiftType(string depID,string shiftTypeID)
        {
            List<ShiftMaintainIndexModel> depShiftType = GetDepShiftModel().Where(c => c.depID == depID && c.shiftTypeCategoryID == shiftTypeID).ToList();

            if(depShiftType.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int InsertDataToShiftMaintain(ShiftMaintainViewModel shiftMaintain)
        {                        
            DepShiftTypeParameterModel depshiftTypeParametModelData = new DepShiftTypeParameterModel()
            {
                depID = shiftMaintain.depID,
                shiftTypeCategory = shiftMaintain.shiftTypeID,
                startTime = shiftMaintain.startTime,
                endTime = shiftMaintain.endTime,
                status = shiftMaintain.status,
                workhour = shiftMaintain.workhour,
                resthour = shiftMaintain.resthour,
                createDatetime = Convert.ToDateTime(date.ToString("yyy/MM/dd")),
                updateDatetime = Convert.ToDateTime(date.ToString("yyy/MM/dd"))
            };

            int result = SimpleFactory.CreateInstance().Add<DepShiftTypeParameterModel>(depshiftTypeParametModelData);
           
            return result;
        }

        public List<ShiftMaintainIndexModel> GetDepShiftModel()
        {           
            var result = (from departmentData in department
                          join depShiftTypeData in depshiftTypeParameter
                          on departmentData.depID equals depShiftTypeData.depID
                          join shiftTypeParam in shiftTypeParameter
                          on depShiftTypeData.shiftTypeCategory equals shiftTypeParam.shiftTypeCategory
                          select new ShiftMaintainIndexModel()
                          {
                              id = depShiftTypeData.Id,
                              depID = depShiftTypeData.depID,
                              depName = departmentData.name,                             
                              shiftTypeCategoryID = depShiftTypeData.shiftTypeCategory,
                              shiftTypeName = shiftTypeParam.name,
                              status = depShiftTypeData.status == true ? "已啟用" : "未啟用",
                              startTime = depShiftTypeData.startTime,
                              endTime = depShiftTypeData.endTime,
                              workhour = depShiftTypeData.workhour,
                              resthour = depShiftTypeData.resthour
                          }).ToList();

            return result;
        }

        public ShiftMaintainIndexModel GetDepShift(int id)
        {
            ShiftMaintainIndexModel result = GetDepShiftModel().FirstOrDefault(c => c.id == id);

            return result;
        }

        public bool UpdateDataToDepShift(ShiftMaintainIndexModel data)
        {
            bool result = false;

            var depshiftTypeParameter = SimpleFactory.CreateInstance().FindAll<DepShiftTypeParameterModel>().AsQueryable().Where(x =>
            x.Id == data.id).FirstOrDefault();
           
            if (depshiftTypeParameter != null)
            {
                depshiftTypeParameter.depID = data.depID;
                depshiftTypeParameter.shiftTypeCategory = data.shiftTypeCategoryID;
                depshiftTypeParameter.createDatetime = Convert.ToDateTime(date.ToString("yyy/MM/dd"));
                depshiftTypeParameter.updateDatetime = Convert.ToDateTime(date.ToString("yyy/MM/dd"));
                depshiftTypeParameter.startTime = data.startTime;
                depshiftTypeParameter.endTime = data.endTime;
                depshiftTypeParameter.status = data.status == "已啟用" ? true : false;
                depshiftTypeParameter.workhour = data.workhour;
                depshiftTypeParameter.resthour = data.resthour;

                result = SimpleFactory.CreateInstance().Update<DepShiftTypeParameterModel>(depshiftTypeParameter);
               
            }

            return result;
        }
        public bool DelDepShiftTypeParameter(int id)
        {
            bool result = false;

            DepShiftTypeParameterModel depshift = depshiftTypeParameter.FirstOrDefault(c => c.Id == id);

            result = SimpleFactory.CreateInstance().Delete<DepShiftTypeParameterModel>(depshift);

            return result;
        }
        public List<SelectListItem> GetDepartmentForDropdown()
        {
            List<SelectListItem> result = SimpleFactory.CreateInstance().FindAll<DepartmentModel>().AsQueryable()
                .Select(c => new SelectListItem()
                {                    
                    Value = c.depID,
                    Text = c.name
                }).ToList();

            return result;
        }
        public List<SelectListItem> GetShiftTypeParameterForDropdown()
        {
            List<SelectListItem> result = SimpleFactory.CreateInstance().FindAll<ShiftTypeParameterModel>().AsQueryable()
                .Select(c => new SelectListItem()
                {                    
                    Value = c.shiftTypeCategory.ToString(),
                    Text = c.name
                }).ToList();
                
            return result;
        }

        public double GetWorkHours(string starttime,string endtime)
        {
            DateTime startTime = Convert.ToDateTime(starttime);
            DateTime endTime = Convert.ToDateTime(endtime);

            var spentTime = endTime - startTime;

            double spenEndTime = Math.Round(Convert.ToDouble(spentTime.Minutes)/60, 2, MidpointRounding.AwayFromZero);

            double workhours = spentTime.Hours + spenEndTime;

            return workhours;
        }
    }
}