using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web;
using OhDataManager.Enums;
using WebAppAuth.Models;
using System.Globalization;
using Microsoft.AspNet.Identity.Owin;
using WebAppAuth.Entities;
using WebAppAuth.Services.Data;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Context;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Личный кабинет сотрудника
    /// </summary>
    [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector+", " + AppConstants.RoleWorker)]
    public class EmployeeController : BaseController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        /// <summary>
        /// Главная страница личного кабинета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();

                var employee = context.Employee.Where(x => x.ApplicationUser.Id == userIdentityid);

                if (!employee.Any()) throw new Exception("Сотрудник не найден");

                if (employee.Count() > 1) throw new Exception("К пользователю прикреплен более 1 сорудника");

                return View(employee.Single());
            }
        }


        /// <summary>
        /// Редактирование контактной информации абонента
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="loadImage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(Employee employee, HttpPostedFileBase loadImage)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    if (loadImage != null)
                    {
                        employee.ImageMimeType = loadImage.ContentType;
                        employee.ImageData = new byte[loadImage.ContentLength];
                        loadImage.InputStream.Read(employee.ImageData, 0, loadImage.ContentLength);
                    }

                    if (employee.Id == 0)
                    {
                        context.Employee.Add(employee);
                    }
                    else
                    {
                        var dbEntry = await context.Employee.FindAsync(employee.Id);
                        if (dbEntry != null)
                        {
                            dbEntry.Id = employee.Id;
                            dbEntry.Surname = employee.Surname;
                            dbEntry.Name = employee.Name;
                            dbEntry.Patronymic = employee.Patronymic;
                            dbEntry.DisplayName = employee.DisplayName;
                            dbEntry.PhoneNumber = employee.PhoneNumber;
                            if (loadImage != null)
                            {
                                dbEntry.ImageData = employee.ImageData;
                                dbEntry.ImageMimeType = employee.ImageMimeType;
                            }
                        }
                    }
                    await context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }


        /// <summary>
        /// Получение аватарки сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FileContentResult> GetImage(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employee = await context.Employee.FindAsync(id);
                return employee != null ? File(employee.ImageData, employee.ImageMimeType) : null;
            }
        }

        /// <summary>
        /// Удаление аватарки сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> DelFile(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employee = await context.Employee.FindAsync(id);
                if (employee == null) throw new Exception($"Сотрдуник с Id '{id}' не найден");

                employee.ImageData = null;
                employee.ImageMimeType = null;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> DataImport()
        {
            return View();
        }

        /// <summary>
        /// Выгрузка данных
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> DataExport()
        {
            return View();
        }

        /// <summary>
        /// Отчет абоненты
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> ReportAbonents()
        {
            return View();
        }

        /// <summary>
        /// Отчет Показания ИПУ
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> ReportIPUVals()
        {
            return View();
        }

        /// <summary>
        /// Отчет Показания ОДПУ
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> ReportODPUVals()
        {
            return View();
        }

        /// <summary>
        /// Отчет Оплаты
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> ReportPayments()
        {
            return View();
        }

        /// <summary>
        /// Отчет по заявкам
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> ReportOfClaims()
        {
            return View();
        }

        /// <summary>
        /// Печать наряда-заказа
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> PrintingJobOrder(int claimId)
        {
            ViewData["claimId"] = claimId;
            return View();
        }

        /// <summary>
        /// Лицевые счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult PersonalAccountsInfo(FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                ViewData["CalculationMonth"] =
                    (context.CalculationMonth.Any()
                        ? context.CalculationMonth.Max(x => x.CalculationDate)
                        : DateTime.Now)
                        .ToString("y");

                long houseId;
                long.TryParse(formValue["House"], out houseId);

                ViewData["House"] =
                    context.House
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == houseId
                            })
                        .OrderBy(x => x.Text)
                        .ToList();

                var data = context.PersonalAccount
                    .Where(x => x.HouseId == houseId)
                    .ToList()
                    .OrderBy(x => x.HouseFullAddress)
                    .ThenBy(x => x.ApartmentNumber, new SemiNumericComparer());

                return View(data);
            }
        }

        /// <summary>
        /// Информация по ЛС
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> PersonalAccountDetails(long id)
        {
            using (var context = new ApplicationDbContext())
            {
                var data = context.PersonalAccount.Find(id);
                
                var currentCalculationMonth = context.CalculationMonth
                    .First(x =>x.IsCurrent).CalculationDate;

                var accruals =
                    (await OhConfigurator.Container.Resolve<IAbonentService>()
                        .GetAccruals(id, currentCalculationMonth)).Content.ToList();

                ViewBag.Accruals = accruals;

                ViewBag.CurrentCalculationMonth = currentCalculationMonth.ToString("MMMM yyyy");
                ViewBag.AccruedSumTotal = accruals.Sum(x => x.AccruedTariffNondelivery);
                ViewBag.InsaldoSumTotal = accruals.Sum(x => x.SumInsaldo);
                ViewBag.PaydSumTotal = accruals.Sum(x => x.SumPayd);


                if (data != null)
                    return PartialView(data);
                return HttpNotFound();
            }
        }

        /// <summary>
        /// Новое сообщения
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public int CountNewMessage()
        {
            return 0;
        }

        /// <summary>
        /// Новый опрос
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public int CountNewPoll()
        {
            return 0;
        }

        /// <summary>
        /// Приборы учета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Counters(FormCollection formValue)
        {
            //TODO: вынести в отдельный сервис

            using (var context = new ApplicationDbContext())
            {
                long houseId;
                long.TryParse(formValue["Houses"], out houseId);

                ViewData["Houses"] =
                    context.House
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == houseId
                            })
                        .OrderBy(x => x.Text)
                        .ToList();

                long serviceId;
                long.TryParse(formValue["Services"], out serviceId);

                ViewData["Services"] =
                    context.MeterService
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ServiceName + (x.MeasureName != "-" ? " (" + x.MeasureName + ")" : ""),
                                Value = x.Id.ToString(),
                                Selected = x.Id == serviceId
                            })
                        .OrderBy(x => x.Text)
                        .Distinct()
                        .ToList();


                long personalAccountServiceId;
                long.TryParse(formValue["KvarServices"], out personalAccountServiceId);

                ViewData["KvarServices"] =
                    context.MeterService
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ServiceName + (x.MeasureName != "-" ? " (" + x.MeasureName + ")" : ""),
                                Value = x.Id.ToString(),
                                Selected = x.Id == personalAccountServiceId
                            })
                        .OrderBy(x => x.Text)
                        .Distinct()
                        .ToList();

                long personalAccountId;
                long.TryParse(formValue["Kvars"], out personalAccountId);

                ViewData["Kvars"] =
                    context.PersonalAccount
                        .Where(x => x.HouseId == houseId)
                        .OrderBy(x => x.ApartmentNumber)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ApartmentAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == personalAccountId
                            })
                        .ToList();

                var currentCalculationDate =
                    context.CalculationMonth.Any()
                        ? context.CalculationMonth.First(x => x.IsCurrent)
                            .CalculationDate
                        : DateTime.Now;


                var houseMeterReadings = context.HouseMeterReading
                    .Where(
                        x =>
                            x.HouseId == houseId &&
                            x.CalculationDate == currentCalculationDate);
                if (serviceId != 0)
                    houseMeterReadings = houseMeterReadings.Where(x => x.MeterServiceId == serviceId);

                var personalAccountMeterReading =
                    context.PersonalAccountMeterReading.Where(x =>
                        x.HouseId == houseId &&
                        x.CalculationDate == currentCalculationDate);

                if (personalAccountId != 0)
                    personalAccountMeterReading =
                        personalAccountMeterReading.Where(x => x.PersonalAccountId == personalAccountId);
                if (personalAccountServiceId != 0)

                    personalAccountMeterReading =
                        personalAccountMeterReading.Where(x => x.MeterServiceId == personalAccountServiceId);

                ViewBag.SecondModel = personalAccountMeterReading.ToList();

                return View(houseMeterReadings.ToList());
            }
        }

        /// <summary>
        /// Приборы учета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Deposition(FormCollection formValue)
        {
            //TODO: допилить, вынести в отдельный сервис

            using (var context = new ApplicationDbContext())
            {
                long houseId;
                long.TryParse(formValue["Houses"], out houseId);

                ViewData["Houses"] =
                    context.House
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == houseId
                            })
                        .ToList();

                long serviceId;
                long.TryParse(formValue["Services"], out serviceId);

                ViewData["Services"] =
                    context.Service
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ServiceName,
                                Value = x.Id.ToString(),
                                Selected = x.Id == serviceId
                            })
                        .ToList();


                long personalAccountServiceId;
                long.TryParse(formValue["KvarServices"], out personalAccountServiceId);

                ViewData["KvarServices"] =
                    context.Service
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ServiceName,
                                Value = x.Id.ToString(),
                                Selected = x.Id == personalAccountServiceId
                            })
                        .ToList();



                long personalAccountId;
                long.TryParse(formValue["Kvars"], out personalAccountId);

                ViewData["Kvars"] =
                    context.PersonalAccount
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.ApartmentAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == personalAccountId
                            })
                        .ToList();
                
                var personalAccountMeterReading =
                    context.PersonalAccountMeterReading.Where(x =>
                        x.HouseId == houseId);

                if (personalAccountId != 0)
                    personalAccountMeterReading =
                        personalAccountMeterReading.Where(x => x.PersonalAccountId == personalAccountId);
                if (personalAccountServiceId != 0)

                    personalAccountMeterReading =
                        personalAccountMeterReading.Where(x => x.MeterServiceId == personalAccountServiceId);

                ViewBag.SecondModel = personalAccountMeterReading.ToList();

                return View(personalAccountMeterReading.ToList());
            }
        }

        /// <summary>
        /// Список начислений лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> AbonentAccruals(long id, FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                var calculationDate = Convert.ToDateTime(formValue["CalculationDateSelectList"]);

                var personalAccount = await context.PersonalAccount.FindAsync(id);

                if (personalAccount == null)
                {
                    throw new Exception($"Не найден ЛС с id '{id}'");
                }

                var calculationMonthList = context.CalculationMonth
                    .Where(x => x.Organization.Id == personalAccount.OrganizationId)
                    .OrderByDescending(g => g.CalculationDate).ToList();

                ViewData["CalculationDateSelectList"] =
                    (from month in calculationMonthList
                        select
                            new SelectListItem
                            {
                                Text = month.CalculationDate.ToString("y"),
                                Value = month.CalculationDate.ToString(CultureInfo.CurrentCulture),
                                Selected = (month.CalculationDate == calculationDate)
                            })
                        .ToList();

                var currentCalculationMonth = calculationMonthList.First(x => x.IsCurrent);

                var calculationMonth = calculationDate == default(DateTime)
                    ? currentCalculationMonth.CalculationDate
                    : calculationDate;

                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetAccruals(personalAccount.Id, calculationMonth))
                    .Content
                    .OrderBy(x => x.IndexNumber);

                ViewBag.CalculationMonth = calculationMonth;
                return View(data);
            }
        }

        /// <summary>
        /// Платежи лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult AbonentPayments(long id)
        {
            using (var context = new ApplicationDbContext())
            {
                var data = context.PersonalAccountPayment
                    .Where(x => x.PersonalAccountId == id).ToList();
                return View(data);
            }
        }

        /// <summary>
        /// Приборы учета лицевого счета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> AbonentCounters(long id, FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                var calculationDate = Convert.ToDateTime(formValue["CalculationDateSelectList"]);

                var personalAccount = await context.PersonalAccount.FindAsync(id);

                if (personalAccount == null)
                {
                    throw new Exception($"Не найден ЛС с id '{id}'");
                }

                var calculationMonthList = context.CalculationMonth
                    .Where(x => x.Organization.Id == personalAccount.OrganizationId)
                    .OrderByDescending(g => g.CalculationDate).ToList();

                ViewData["CalculationDateSelectList"] =
                    (from month in calculationMonthList
                        select
                            new SelectListItem
                            {
                                Text = month.CalculationDate.ToString("y"),
                                Value = month.CalculationDate.ToString(CultureInfo.CurrentCulture),
                                Selected = (month.CalculationDate == calculationDate)
                            })
                        .ToList();

                var currentCalculationMonth = calculationMonthList.First(x => x.IsCurrent);

                var calculationMonth = calculationDate == default(DateTime)
                    ? currentCalculationMonth.CalculationDate
                    : calculationDate;

                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetMeterReadings(personalAccount.Id, calculationMonth))
                    .Content
                    .OrderBy(x => x.IndexNumber);

                //добавляем уведомление, если есть ПУ с заканчивающейся датой поверки
                ViewBag.NotificationsList = (from counter in data
                    where
                        (counter.CalculationDate.AddMonths(2) >= counter.VerificationDateNext &&
                         counter.VerificationDateNext != default(DateTime))
                    select
                        "Прибор учёта с заводским номером " + counter.MeterDeviceNumber + " по услуге '" +
                        counter.ServiceName + "'")
                    .ToList();
                ViewBag.CalculationMonth = calculationMonth;
                return View(data);
            }
        }

        /// <summary>
        /// Заявки абонентов
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<ActionResult> AbonentClaims(FormCollection formValue, EClaimStatus? claimStatus)
        {
            //если ввели номер заявки, то сразу переходим в нее
            long claimId;
            long.TryParse(formValue["ClaimId"], out claimId);

            if (claimId != default(long))
            {
                return RedirectToAction("AbonentClaimEdit", "Employee", new {id = claimId});
            }

            using (var context = new ApplicationDbContext())
            {
                long houseId;
                long.TryParse(formValue["Houses"], out houseId);

                ViewData["Houses"] =
                    (await
                        context.House
                            .Select(x =>
                                new SelectListItem
                                {
                                    Text = x.HouseFullAddress,
                                    Value = x.Id.ToString(),
                                    Selected = x.Id == houseId
                                })
                            .OrderBy(x => x.Text)
                            .ToListAsync());

                long personalAccountId;
                long.TryParse(formValue["PersonalAccounts"], out personalAccountId);

                ViewData["PersonalAccounts"] =
                    (await context.PersonalAccount
                        .Where(x => x.HouseId == houseId)
                        .Select(x =>
                            new
                            {
                                x.Id,
                                x.ApartmentOwnerFullName,
                                x.ApartmentAddress,
                                x.ApartmentNumber
                            })
                        .ToListAsync())
                        .OrderBy(x => x.ApartmentNumber, new SemiNumericComparer())
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = $"{x.ApartmentAddress} ({x.ApartmentOwnerFullName})",
                                Value = x.Id.ToString(),
                                Selected = x.Id == personalAccountId
                            });

                //если роль "Сотрудник", то показываем все заявки
                var employeeId = (User.IsInRole(AppConstants.RoleEmployee) ||
                                  User.IsInRole(AppConstants.RoleDirector))
                    ? null
                    : (long?) (await GetCurrentEmployee(context)).Id;

                var data = (await OhConfigurator.Container.Resolve<IAbonentService>()
                    .GetClaims(personalAccountId, employeeId: employeeId,
                        houseId: houseId))
                    .Content
                    //костыль, так как EF не может прикастить enum
                    .WhereIf(claimStatus.HasValue, x => x.ClaimStatus == claimStatus)
                    .OrderByDescending(x => x.CreationTime);

                ViewBag.SelectedStatus = claimStatus;
                ViewBag.SelectedStatusName = claimStatus?.GetDisplayName();

                var userIdentityid = User.Identity.GetUserId<int>();
                ViewBag.CanAddClaim = UserManager.IsInRole(userIdentityid, AppConstants.RoleEmployee);
                return View(data);
            }
        }

        // GET: Employee/AbonentClaimEdit/5
        public async Task<ActionResult> AbonentClaimEdit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ApplicationDbContext())
            {
                var claim = await context.AbonentClaim
                    .Include(x => x.Employee)
                    .Include(x => x.DictClaimKind)
                    .Include(x => x.ImportedFile)
                    .Include(x=>x.ClosedByEmployee)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (claim == null)
                {
                    return RedirectToAction("AbonentClaims", "Employee");
                }

                var positions = await
                    context.PositionClaimKind
                        .Where(x => x.DictClaimKindId == claim.DictClaimKindId)
                        .Select(x => x.PositionId)
                        .ToListAsync();

                ViewData["EmployeeId"] =
                    await 
                    context.Employee
                        .Where(x => positions.Contains(x.PositionId.Value))
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.DisplayName,
                                Value = x.Id.ToString(),
                                Selected = x.Id == claim.EmployeeId
                            })
                            .ToListAsync();

                ViewData["DictClaimKindId"] =
                    context.DictClaimKind.Where(x => x.IsVisible)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString()
                            })
                        .OrderBy(x => x.Text)
                        .ToList();

                //список комментариев к заявке
                var comments = await context.AbonentClaimComment
                    .Where(x => x.AbonentClaimId == claim.Id)
                    .Include(x => x.AbonentClaim)
                    .Include(x => x.AbonentClaim.Creator)
                    .Include(x => x.AbonentClaim.Employee)
                    .Select(x => new AbonentClaimCommentViewModel
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        Creator = x.AbonentClaim.Creator,
                        Employee = x.Employee,
                        InformationSource = x.InformationSource,
                        CreationTime = x.CreationTime
                    })
                    .OrderBy(x => x.Id)
                    .ToListAsync();

                var userIdentityid = User.Identity.GetUserId<int>();

                ViewBag.ClaimCanClosed = claim.ClaimStatus == EClaimStatus.Completed
                                         && !claim.Accepted
                                         && UserManager.IsInRole(userIdentityid, AppConstants.RoleEmployee);

                ViewBag.CurrentUserIsEmployee = UserManager.IsInRole(userIdentityid, AppConstants.RoleEmployee);

                ViewBag.CurrentUserIsAuthor = false;
                if (claim.EmployeeCreatorId.HasValue)
                {
                    var employee = await GetCurrentEmployee(context);
                    if (employee != null)
                        ViewBag.CurrentUserIsAuthor = employee.Id == claim.EmployeeCreatorId;
                }

                return View(new ClaimViewModel
                {
                    Id = claim.Id,
                    ClaimStatus = claim.ClaimStatus,
                    ApartmentFullAddress = claim.ApartmentFullAddress,
                    CreationTime = claim.CreationTime,
                    ChangedTime = claim.ChangedTime,
                    CompleteDate = claim.CompleteDate,
                    ContactPhoneNumber = claim.ContactPhoneNumber,
                    Description = claim.Description,
                    DictClaimKind = claim.DictClaimKind,
                    DictClaimKindId = claim.DictClaimKindId,
                    DictClaimKindName = claim.DictClaimKind?.Name,
                    Employee = claim.Employee,
                    EmployeeId = claim.EmployeeId,
                    ImportedFileId = claim.ImportedFile?.ExchangeFileId,
                    CommentList = comments,
                    Accepted = claim.Accepted,
                    Rating = claim.Rating,
                    ClosedByEmployee = claim.ClosedByEmployee,
                    Cost = claim.Cost
                });
            }
        }

        // POST: Claim/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AbonentClaimEdit(
            //    [Bind(Include = "Id,PersonalAccountId,ApartmentFullAddress,DictClaimKindId,Description,ContactPhoneNumber,ClaimStatus,CompleteDate,EmployeeId,OrganizationId,CreationTime,ChangedTime")]
            ClaimViewModel claimViewModel)
        {
            using (var context = new ApplicationDbContext())
            {
                {
                    var claim =
                        await context.AbonentClaim
                            .FirstOrDefaultAsync(
                                x => x.Id == claimViewModel.Id);

                    if (claim == null)
                    {
                        throw new Exception("Заявка не найдена");
                    }

                    if (claimViewModel.ClaimStatus != EClaimStatus.Undefined &&
                        claim.ClaimStatus != claimViewModel.ClaimStatus)
                    {
                        claim.ChangedTime = DateTime.Now;
                        claim.ClaimStatus = claimViewModel.ClaimStatus;
                        //TODO: внести с историю переходов
                    }


                    if (claimViewModel.EmployeeId != null &&
                        claimViewModel.EmployeeId != 0 &&
                        claim.EmployeeId != claimViewModel.EmployeeId)
                    {
                        claim.ChangedTime = DateTime.Now;
                        claim.EmployeeId = claimViewModel.EmployeeId;
                        //TODO: внести с историю переходов
                    }

                    if (!string.IsNullOrEmpty(claimViewModel.Description) &&
                        claim.Description != claimViewModel.Description)
                    {
                        claim.ChangedTime = DateTime.Now;
                        claim.Description = claimViewModel.Description;
                    }

                    if (claimViewModel.DictClaimKindId != 0 &&
                        claim.DictClaimKindId != claimViewModel.DictClaimKindId)
                    {
                        claim.ChangedTime = DateTime.Now;
                        claim.DictClaimKindId = claimViewModel.DictClaimKindId;
                    }

                    if (claimViewModel.Cost != null &&
                        claimViewModel.Cost != 0 &&
                        claim.Cost != claimViewModel.Cost)
                    {
                        claim.ChangedTime = DateTime.Now;
                        claim.Cost = claimViewModel.Cost;
                    }

                    if (!string.IsNullOrEmpty(claimViewModel.ContactPhoneNumber) &&
                        claim.ContactPhoneNumber != claimViewModel.ContactPhoneNumber)
                    {
                        claim.ChangedTime = DateTime.Now;
                        claim.ContactPhoneNumber = claimViewModel.ContactPhoneNumber;
                    }

                    await context.SaveChangesAsync();

                    return RedirectToAction("AbonentClaimEdit", "Employee", new {id = claim.Id});
                }
            }
        }


        // GET: Employee/AbonentClaimEdit/5
        public async Task<ActionResult> AbonentClaimDelete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ApplicationDbContext())
            {
                var claim = await context.AbonentClaim.FindAsync(id);
                if (claim == null)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                context.AbonentClaim.Remove(claim);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("AbonentClaims", "Employee");
        }

        // POST: Claim/CommentClaim
            [
            HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CommentAbonentClaim(AbonentClaimComment abonentClaimViewModel,
            HttpPostedFileBase inputFile)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var userIdentityid = User.Identity.GetUserId<int>();

                    var employee =
                        await context.Employee.FirstOrDefaultAsync(x => x.ApplicationUser.Id == userIdentityid);

                    if (employee == null)
                    {
                        throw new Exception("Не найден сотрудник");
                    }
                    var claimComment = new AbonentClaimComment
                    {
                        OrganizationId = employee.OrganizationId,
                        AbonentClaimId = abonentClaimViewModel.AbonentClaimId,
                        Comment = abonentClaimViewModel.Comment,
                        EmployeeId = employee.Id,
                        ChangedTime = DateTime.Now,
                        CreationTime = DateTime.Now,
                        InformationSource = EInformationSource.Employee
                    };

                    if (inputFile != null && inputFile.ContentLength > 0)
                    {
                        var fileStorageManager =
                            OhConfigurator.Container.Resolve<IFileStorageManager>();
                        claimComment.ImportedFileId =
                            (await
                                fileStorageManager.SaveImportedFile(inputFile, userIdentityid, EImportType.AbonentFiles))
                                .Id;
                    }

                    context.AbonentClaimComment.Add(claimComment);
                    await context.SaveChangesAsync();
                }

                return RedirectToAction("AbonentClaimEdit", new {id = abonentClaimViewModel.AbonentClaimId});
            }
        }

        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateClaim(FormCollection formValue)
        {
            using (var context = new ApplicationDbContext())
            {
                ViewData["DictClaimKindId"] =
                    context.DictClaimKind.Where(x => x.IsVisible)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString()
                            })
                        .OrderBy(x => x.Text)
                        .ToList();

                ViewData["EmployeeId"] = new SelectList(new List<SelectListItem>());

                long houseId;
                long.TryParse(formValue["HouseId"], out houseId);

                ViewData["HouseId"] =
                    context.House
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == houseId
                            })
                        .OrderBy(x => x.Text)
                        .ToList();

                ViewData["PersonalAccountId"] = new SelectList(new List<SelectListItem>());

                return View();
            }
        }


        // POST: Claim/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateClaim(ClaimViewModel claimViewModel, HttpPostedFileBase inputFile, bool printJobOrderAfterSave = false)
        {
            using (var context = new ApplicationDbContext())
            {
                var userIdentityid = User.Identity.GetUserId<int>();
                var employee = await GetCurrentEmployee(context);

                if (ModelState.IsValid)
                {
                    var personalAccount =
                        context.PersonalAccount.FirstOrDefault(x => x.Id == claimViewModel.PersonalAccountId);
                    if (personalAccount == null)
                        throw new Exception($"Не найден ЛС с идентификатором: {claimViewModel.PersonalAccountId}");


                    var claim = new AbonentClaim
                    {
                        OrganizationId = personalAccount.OrganizationId,
                        ApartmentFullAddress = personalAccount.ApartmentFullAddress,
                        ChangedTime = DateTime.Now,
                        CreationTime = DateTime.Now,
                        ContactPhoneNumber = claimViewModel.ContactPhoneNumber,
                        PersonalAccountId = personalAccount.Id,
                        DictClaimKindId = claimViewModel.DictClaimKindId,
                        Description = claimViewModel.Description,
                        ClaimStatus = EClaimStatus.Created,
                        EmployeeId = claimViewModel.EmployeeId,
                        EmployeeCreatorId = employee.Id,
                        Cost = claimViewModel.Cost
                    };

                    if (inputFile != null && inputFile.ContentLength > 0)
                    {
                        var fileStorageManager =
                            OhConfigurator.Container.Resolve<IFileStorageManager>();
                        claim.ImportedFileId =
                            (await
                                fileStorageManager.SaveImportedFile(inputFile, userIdentityid, EImportType.AbonentFiles))
                                .Id;
                    }

                    context.AbonentClaim.Add(claim);
                    await context.SaveChangesAsync();


                    return printJobOrderAfterSave
                        ? RedirectToAction("PrintingJobOrder", "Employee", new {claimId = claim.Id})
                        : RedirectToAction("AbonentClaims", "Employee");
                }

                ViewData["DictClaimKindId"] =
                    context.DictClaimKind
                        .Where(x => x.IsVisible)
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.Name,
                                Value = x.Id.ToString(),
                                Selected = x.Id == claimViewModel.DictClaimKindId
                            }).ToList();

                ViewData["HouseId"] =
                    context.House
                        .Select(x =>
                            new SelectListItem
                            {
                                Text = x.HouseFullAddress,
                                Value = x.Id.ToString(),
                                Selected = x.Id == claimViewModel.HouseId
                            })
                        .OrderBy(x => x.Text)
                        .ToList();

                ViewData["PersonalAccountId"] = context.PersonalAccount
                    .Where(x => x.HouseId == claimViewModel.HouseId)
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = x.ApartmentFullAddress,
                            Value = x.Id.ToString(),
                            Selected = x.Id == claimViewModel.PersonalAccountId
                        })
                    .ToList()
                    .OrderBy(x => x.Text);


                return View(claimViewModel);
            }
        }


        // POST: Claim/CloseClaim
        public async Task<ActionResult> CloseClaim()
        {
            using (var context = new ApplicationDbContext())
            {
                long claimId;
                long.TryParse(Request["AbonentClaimId"], out claimId);


                int rating;
                int.TryParse(Request["Rating"], out rating);

                var claim = await context.AbonentClaim.FindAsync(claimId);
                if (claim == null)
                    return HttpNotFound();
                
                var employee = await GetCurrentEmployee(context);

                claim.Rating = rating;
                claim.Accepted = true;
                claim.CompleteDate = DateTime.Now;
                claim.ClaimStatus = EClaimStatus.Closed;
                claim.ClosedByEmployeeId = employee.Id;

                await context.SaveChangesAsync();
                
                return RedirectToAction("AbonentClaimEdit", new { id = claimId });
            }
        }
    }
}