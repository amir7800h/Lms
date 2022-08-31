using Lms.Context;
using Lms.Models.Authentication;
using Lms.Models.Entities;
using Lms.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net;

namespace Lms.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataBaseContext context;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager
            , DataBaseContext context)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region مدیریت اساتید
        public IActionResult AddMaster(bool? isSuccess)
        {
            if (isSuccess != null)
            {
                ViewBag.Result = "ثبت استاد با موفقیت صورت گرفت";
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddMaster(MasterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = _userManager.FindByNameAsync(model.UserName).Result;
                if (user != null)
                {
                    ModelState.AddModelError("", "این نام کاربری قبلا ثبت شده");
                    return View(model);
                }
                User newUser = new User()
                {
                    UserName = model.UserName,
                    FullName = model.FullName,
                    College = model.Collage
                };

                var result = _userManager.CreateAsync(newUser, model.Password).Result;
                if (result.Succeeded)
                {
                    user = _userManager.FindByNameAsync(model.UserName).Result;
                    var roleSuccess = _userManager.AddToRoleAsync(user, "MASTER").Result;
                    if (roleSuccess.Succeeded)
                    {
                        return Redirect("~/admin/addmaster?isSuccess=true");
                    }
                    foreach (var item in roleSuccess.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View(model);
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult ManageMasters()
        {
            try
            {
                List<MasterViewModel> masters = _userManager.GetUsersInRoleAsync("MASTER").Result
                    .Select(x => new MasterViewModel()
                    {
                        Collage = x.College,
                        FullName = x.FullName,
                        Id = x.Id,
                        UserName = x.UserName,
                    }).ToList();

                return View(masters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IActionResult EditMasters(string Id, bool? isSuccess)
        {
            try
            {
                User master = _userManager.FindByIdAsync(Id).Result;
                MasterViewModel model = new MasterViewModel()
                {
                    Collage = master.College,
                    FullName = master.FullName,
                    Id = master.Id,
                    Password = "",
                    UserName = master.UserName,
                };
                if (isSuccess != null)
                {
                    ViewBag.Result = "ویرایش استاد با موفقیت صورت گرفت";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult EditMasters(MasterViewModel model)
        {
            try
            {

                User master = _userManager.FindByIdAsync(model.Id).Result;

                if (!String.IsNullOrWhiteSpace(model.FullName))
                    master.FullName = model.FullName;

                if (!String.IsNullOrWhiteSpace(model.Collage))
                    master.College = model.Collage;


                if (!String.IsNullOrWhiteSpace(model.UserName))
                    master.UserName = model.UserName;


                if (!String.IsNullOrWhiteSpace(model.Password))
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(master).Result;
                    var result = _userManager.ResetPasswordAsync(master, token, model.Password).Result;
                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View();
                    }
                }

                var resultUpdate = _userManager.UpdateAsync(master).Result;
                if (resultUpdate.Succeeded)
                {
                    return Redirect($"~/admin/EditMasters?isSuccess=true&id={model.Id}");
                }
                else
                {
                    foreach (var item in resultUpdate.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<JsonResult> MasterDelete(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return new JsonResult(new { isSuccess = true });

                }
                else
                {
                    return new JsonResult(new { isSuccess = false });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region مدیریت دانشجویان
        public IActionResult AddStudent(bool? isSuccess)
        {
            if (isSuccess != null)
            {
                ViewBag.Result = "ثبت دانشجو با موفقیت صورت گرفت";
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddStudent(MasterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = _userManager.FindByNameAsync(model.UserName).Result;
                if (user != null)
                {
                    ModelState.AddModelError("", "این نام کاربری قبلا ثبت شده");
                    return View(model);
                }
                User newUser = new User()
                {
                    UserName = model.UserName,
                    FullName = model.FullName,
                    College = model.Collage
                };

                var result = _userManager.CreateAsync(newUser, model.Password).Result;
                if (result.Succeeded)
                {
                    user = _userManager.FindByNameAsync(model.UserName).Result;
                    var roleSuccess = _userManager.AddToRoleAsync(user, "STUDENT").Result;
                    if (roleSuccess.Succeeded)
                    {
                        return Redirect("~/admin/addstudent?isSuccess=true");
                    }
                    foreach (var item in roleSuccess.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View(model);
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult ManageStudent()
        {
            try
            {
                List<MasterViewModel> students = _userManager.GetUsersInRoleAsync("STUDENT").Result
                    .Select(x => new MasterViewModel()
                    {
                        Collage = x.College,
                        FullName = x.FullName,
                        Id = x.Id,
                        UserName = x.UserName,
                    }).ToList();

                return View(students);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IActionResult EditStudent(string Id, bool? isSuccess)
        {
            try
            {
                User master = _userManager.FindByIdAsync(Id).Result;
                MasterViewModel model = new MasterViewModel()
                {
                    Collage = master.College,
                    FullName = master.FullName,
                    Id = master.Id,
                    Password = "",
                    UserName = master.UserName,
                };
                if (isSuccess != null)
                {
                    ViewBag.Result = "ویرایش دانشجو با موفقیت صورت گرفت";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult EditStudent(MasterViewModel model)
        {
            try
            {

                User master = _userManager.FindByIdAsync(model.Id).Result;

                if (!String.IsNullOrWhiteSpace(model.FullName))
                    master.FullName = model.FullName;

                if (!String.IsNullOrWhiteSpace(model.Collage))
                    master.College = model.Collage;


                if (!String.IsNullOrWhiteSpace(model.UserName))
                    master.UserName = model.UserName;


                if (!String.IsNullOrWhiteSpace(model.Password))
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(master).Result;
                    var result = _userManager.ResetPasswordAsync(master, token, model.Password).Result;
                    if (!result.Succeeded)
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View();
                    }
                }

                var resultUpdate = _userManager.UpdateAsync(master).Result;
                if (resultUpdate.Succeeded)
                {
                    return Redirect($"~/admin/Editstudent?isSuccess=true&id={model.Id}");
                }
                else
                {
                    foreach (var item in resultUpdate.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<JsonResult> StudentDelete(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return new JsonResult(new { isSuccess = true });

                }
                else
                {
                    return new JsonResult(new { isSuccess = false });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region مدیریت دروس
        public IActionResult AddCource(bool? isSuccess)
        {
            if (isSuccess != null)
            {
                ViewBag.ResultMessage = "ثبت درس با موفقیت صورت گرفت";
            }
            var masters = _userManager.GetUsersInRoleAsync("MASTER").Result
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.FullName,
                }).ToList();
            ViewBag.Masters = new SelectList(masters, "Id", "Name");

            return View();
        }
        [HttpPost]
        public IActionResult AddCource(Course model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                Course course = new Course()
                {
                    College = model.College,
                    MasterId = model.MasterId,
                    Name = model.Name
                };

                context.Courses.Add(course);
                context.SaveChanges();

                return Redirect("~/admin/AddCource?isSuccess=true");


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult ManageCource()
        {
            try
            {
                List<CourseViewModel> courses = context.Courses
                    .Select(x => new CourseViewModel
                    {
                        College = x.College,
                        Name = x.Name,
                        MasterId = x.MasterId,
                        Id = x.Id,
                    }).ToList();

                foreach (var item in courses)
                {
                    item.MasterName = "استاد " + _userManager.FindByIdAsync(item.MasterId.ToString()).Result.FullName;
                }

                return View(courses);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IActionResult EditCource(int Id, bool? isSuccess)
        {
            try
            {

                var course = context.Courses.SingleOrDefault(x => x.Id == Id);
                var masters = _userManager.GetUsersInRoleAsync("MASTER").Result
                    .Select(x => new Masters
                    {
                        Id = x.Id,
                        Name = x.FullName,
                    }).ToList();
                var model = new CourseEditViewModel()
                {
                    Id = course.Id,
                    College = course.College,
                    MasterId = course.MasterId,
                    Name = course.Name,
                    Masters = masters
                };

                if (isSuccess != null)
                {
                    ViewBag.ResultMessageEditCourse = "ویرایش درس با موفقیت صورت گرفت";
                }

                ViewBag.Masters = masters;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public IActionResult EditCource(CourseEditViewModel model)
        {
            try
            {

                var course = context.Courses.SingleOrDefault(x => x.Id == model.Id);

                if (!String.IsNullOrWhiteSpace(model.Name))
                    course.Name = model.Name;

                if (!String.IsNullOrWhiteSpace(model.College))
                    course.College = model.College;

                if (!String.IsNullOrWhiteSpace(model.MasterId.ToString()))
                    course.MasterId = model.MasterId;

                context.SaveChanges();

                return Redirect($"~/admin/EditCource?isSuccess=true&id={model.Id}");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        public async Task<JsonResult> DeleteCource(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return new JsonResult(new { isSuccess = true });

                }
                else
                {
                    return new JsonResult(new { isSuccess = false });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
