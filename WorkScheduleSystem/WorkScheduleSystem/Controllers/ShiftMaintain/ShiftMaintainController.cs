using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkScheduleSystem.Controllers.Shift.Service;
using WorkScheduleSystem.Model.Models;
using WorkScheduleSystem.Models;
using WorkScheduleSystem.Utilities.CookiesHelper;

namespace WorkScheduleSystem.Controllers
{
    public class ShiftMaintainController : Controller
    {
        private ShiftMaintainService _shiftMaintainService;
        private ShiftMaintainService shiftMaintainService
        {
            get
            {
                if (_shiftMaintainService == null)
                { _shiftMaintainService = new ShiftMaintainService(); }
                return _shiftMaintainService;
            }
        }

        private ShiftService _shiftService;
        private ShiftService shiftService
        {
            get
            {
                if (_shiftService == null)
                { _shiftService = new ShiftService(); }
                return _shiftService;
            }
        }

        UsersModel userInfo = new UsersModel();
        int uId = AuthCookies.GetAuthUserUid();
        public ActionResult Index(ShiftMaintainViewModel shiftMaintainData)
        {
            userInfo = shiftService.GetUserInfoById(uId);
            ViewBag.userName = userInfo.name;
            ViewBag.userTypeID = userInfo.userTypeId;

            ViewData["Department"] = shiftMaintainService.GetDepartmentForDropdown();
            ViewData["ShiftType"] = shiftMaintainService.GetShiftTypeParameterForDropdown();
             
            if(shiftMaintainService.ValidatedToDepShiftType(shiftMaintainData.depID, shiftMaintainData.shiftTypeID) == true)
            {
                TempData["DepShiftData"] = "部門和班別不可重複";
            }
            else 
            {
                int shiftMaintain = shiftMaintainService.InsertDataToShiftMaintain(shiftMaintainData);

                if (shiftMaintain > 1)
                {
                    TempData["ResultMessage"] = "新增成功";
                }               
            }
            return View();
        }

        public ActionResult Query(ShiftMaintainIndexModel shiftMaintainModel, int? page)
        {
            userInfo = shiftService.GetUserInfoById(uId);
            ViewBag.userName = userInfo.name;
            ViewBag.userTypeID = userInfo.userTypeId;

            ViewData["Department"] = shiftMaintainService.GetDepartmentForDropdown();
            ViewData["ShiftType"] = shiftMaintainService.GetShiftTypeParameterForDropdown();

            shiftMaintainModel.shiftMaintainModelList = shiftMaintainService.GetDepShiftModel();

            if (!string.IsNullOrEmpty(shiftMaintainModel.SearchModel.depId))
            {
                shiftMaintainModel.shiftMaintainModelList = shiftMaintainModel.shiftMaintainModelList.Where(s => s.depID == shiftMaintainModel.SearchModel.depId).ToList();
            }

            if (!string.IsNullOrEmpty(shiftMaintainModel.SearchModel.shiftTypeCategoryId))
            {
                shiftMaintainModel.shiftMaintainModelList = shiftMaintainModel.shiftMaintainModelList.Where(s => s.shiftTypeCategoryID == shiftMaintainModel.SearchModel.shiftTypeCategoryId).ToList();
            }

            return View(shiftMaintainModel.shiftMaintainModelList.ToList().ToPagedList(page ?? 1, 3));           
        }

        public ActionResult Edit(ShiftMaintainIndexModel shiftMaintain)
        {
            userInfo = shiftService.GetUserInfoById(uId);
            ViewBag.userName = userInfo.name;
            ViewBag.userTypeID = userInfo.userTypeId;

            ViewData["Department"] = shiftMaintainService.GetDepartmentForDropdown();
            ViewData["ShiftType"] = shiftMaintainService.GetShiftTypeParameterForDropdown();

            ShiftMaintainIndexModel result = shiftMaintainService.GetDepShift(shiftMaintain.id);

            bool depData = shiftMaintainService.UpdateDataToDepShift(shiftMaintain);

            if (depData == true)
            {
                TempData["ResultMessage"] = "更新成功";
            }

            return View(result);
        }

        [HttpPost]
        public JsonResult WorkHours(string startTime,string endTime)
        {

            double workhours = shiftMaintainService.GetWorkHours(startTime, endTime);

            return Json(workhours, JsonRequestBehavior.AllowGet);
        }
    }
}